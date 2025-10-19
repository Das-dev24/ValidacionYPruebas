using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Model;
using System;

namespace Practica.Model.Tests {
    [TestClass]
    public class UserTests {
        // Usamos una contraseña válida que cumpla las políticas para las pruebas.
        private const string InitialPassword = "ValidPassword123!";
        private User _user;

        // Este método se ejecuta antes de cada test para tener un objeto limpio.
        [TestInitialize]
        public void Setup() {
            _user = new User("John", "Doe", "john.doe@example.com", InitialPassword);
        }

        #region Constructor Tests

        [TestMethod]
        public void Constructor_WhenCalled_InitializesPropertiesCorrectly() {
            // Assert: Verificamos que todas las propiedades se inicializan correctamente.
            Assert.AreEqual("John", _user.Name);
            Assert.AreEqual("Doe", _user.LastName);
            Assert.AreEqual("john.doe@example.com", _user.Email);
            // Comprobamos que la contraseña fue encriptada correctamente.
            Assert.IsTrue(Utils.Password.VerifyPassword(InitialPassword, _user.Password));
            Assert.IsFalse(_user.Is_Subscription);
            Assert.IsFalse(_user.Is_superuser);
            Assert.AreEqual(UserState.Unactive, _user.State);
            Assert.IsNotNull(_user.Activities);
            Assert.AreEqual(0, _user.Activities.Count);
            // Verificamos que la fecha de último login es reciente.
            Assert.IsTrue((DateTime.Now - _user.Last_login).TotalSeconds < 5);
        }

        #endregion

        #region ChangePassword Tests

        [TestMethod]
        public void ChangePassword_WithValidInputs_ShouldUpdatePasswordSuccessfully() {
            // Arrange
            string newPassword = "NewSecurePassword456$";

            // Act
            _user.ChangePassword(InitialPassword, newPassword);

            // Assert
            Assert.IsTrue(Utils.Password.VerifyPassword(newPassword, _user.Password), "La nueva contraseña debería estar hasheada y guardada.");
            Assert.IsFalse(Utils.Password.VerifyPassword(InitialPassword, _user.Password), "La contraseña antigua ya no debería ser válida.");
        }

        [TestMethod]
        public void ChangePassword_WhenNewPasswordIsSameAsOld_ShouldSucceed() {
            // Act
            _user.ChangePassword(InitialPassword, InitialPassword);

            // Assert
            Assert.IsTrue(Utils.Password.VerifyPassword(InitialPassword, _user.Password), "La contraseña debería seguir siendo la misma y válida.");
        }

        [TestMethod]
        public void ChangePassword_WithIncorrectCurrentPassword_ShouldThrowInvalidOperationException() {
            // Arrange
            string wrongPassword = "this-is-not-the-password";
            string newPassword = "NewSecurePassword456$";

            // Act & Assert
            var ex = Assert.ThrowsException<InvalidOperationException>(
                () => _user.ChangePassword(wrongPassword, newPassword)
            );
            Assert.AreEqual("La contraseña actual no es correcta.", ex.Message);
        }

        [DataTestMethod]
        [DataRow(null, "NewSecurePassword456$", "La contraseña actual no puede ser nula.")]
        [DataRow(InitialPassword, null, "La nueva contraseña no puede ser nula.")]
        [DataRow("", "NewSecurePassword456$", "La contraseña actual no puede estar vacía.")]
        [DataRow(InitialPassword, "", "La nueva contraseña no puede estar vacía.")]
        public void ChangePassword_WithNullOrEmptyInputs_ShouldThrowArgumentException(string oldPass, string newPass, string message) {
            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(
                () => _user.ChangePassword(oldPass, newPass),
                message
            );
            Assert.AreEqual("Las contraseñas no pueden estar vacías.", ex.Message);
        }

        [DataTestMethod]
        [DataRow("short", "La contraseña es demasiado corta.")]
        [DataRow("nouppercase123!", "La contraseña no tiene mayúsculas.")]
        [DataRow("NOLOWERCASE123!", "La contraseña no tiene minúsculas.")]
        [DataRow("NoDigitsHere!", "La contraseña no tiene números.")]
        [DataRow("NoSpecialChar123", "La contraseña no tiene caracteres especiales.")]
        public void ChangePassword_WhenNewPasswordFailsPolicy_ShouldThrowArgumentException(string invalidNewPassword, string reason) {
            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(
                () => _user.ChangePassword(InitialPassword, invalidNewPassword),
                $"Debería fallar porque: {reason}"
            );
            Assert.AreEqual("La nueva contraseña no cumple los requisitos de seguridad.", ex.Message);
        }

        #endregion

        #region ChangeDetails Tests

        [TestMethod]
        public void ChangeDetails_WithValidData_UpdatesProperties() {
            _user.ChangeDetails("Jane", "Smith", "jane.smith@example.org");

            Assert.AreEqual("Jane", _user.Name);
            Assert.AreEqual("Smith", _user.LastName);
            Assert.AreEqual("jane.smith@example.org", _user.Email);
        }

        [TestMethod]
        public void ChangeDetails_WithWhitespaceAsName_UpdatesProperties() {
            // El código actual permite nombres y apellidos con solo espacios en blanco,
            // ya que string.IsNullOrEmpty() devuelve false. Este test lo verifica.
            string whitespaceName = "   ";
            _user.ChangeDetails(whitespaceName, "Smith", "jane.smith@example.org");

            Assert.AreEqual(whitespaceName, _user.Name);
            Assert.AreEqual("Smith", _user.LastName);
        }

        [DataTestMethod]
        [DataRow("plainaddress", "Email sin @")]
        [DataRow("user@domain.c", "Email con TLD muy corto")]
        [DataRow("user @ domain.com", "Email con espacios")]
        [DataRow("@domain.com", "Email sin parte local")]
        public void ChangeDetails_WhenEmailFormatIsInvalid_ThrowsArgumentException(string invalidEmail, string reason) {
            var ex = Assert.ThrowsException<ArgumentException>(
                () => _user.ChangeDetails("Jane", "Smith", invalidEmail),
                $"Debería fallar porque: {reason}"
            );

            Assert.AreEqual("El formato del email no es válido.", ex.Message);
        }

        [DataTestMethod]
        [DataRow(null, "Smith", "e@e.com")]
        [DataRow("Jane", null, "e@e.com")]
        [DataRow("Jane", "Smith", null)]
        [DataRow("", "Smith", "e@e.com")]
        [DataRow("Jane", "", "e@e.com")]
        [DataRow("Jane", "Smith", "")]
        public void ChangeDetails_WhenAnyInputIsNullOrEmpty_ThrowsArgumentException(string name, string lastName, string email) {
            var ex = Assert.ThrowsException<ArgumentException>(
                () => _user.ChangeDetails(name, lastName, email)
            );

            Assert.AreEqual("El nombre, apellidos y email no pueden estar vacíos.", ex.Message);
        }

        #endregion

        #region Equals and GetHashCode Tests

        [TestMethod]
        public void Equals_WithIdenticalUser_ReturnsTrueAndSameHashCode() {
            // Arrange: Creamos dos usuarios idénticos.
            // Es crucial que la contraseña en texto plano sea la misma para que el hash coincida.
            var user1 = new User("Test", "User", "test@test.com", "Password123!") { Id = 1 };
            var user2 = new User("Test", "User", "test@test.com", "Password123!") { Id = 1 };

            // Assert
            Assert.IsTrue(user1.Equals(user2));
            Assert.AreEqual(user1.GetHashCode(), user2.GetHashCode());
        }

        [TestMethod]
        public void Equals_WithDifferentUser_ReturnsFalse() {
            // Arrange: Creamos dos usuarios que difieren en el Id.
            var user1 = new User("Test", "User", "test@test.com", "Password123!") { Id = 1 };
            var user2 = new User("Test", "User", "test@test.com", "Password123!") { Id = 2 };

            // Assert
            Assert.IsFalse(user1.Equals(user2));
        }

        [TestMethod]
        public void Equals_WithNullObject_ReturnsFalse() {
            // Assert
            Assert.IsFalse(_user.Equals(null));
        }

        [TestMethod]
        public void Equals_WithDifferentTypeObject_ReturnsFalse() {
            // Assert
            Assert.IsFalse(_user.Equals(new object()));
        }

        #endregion
    }
}