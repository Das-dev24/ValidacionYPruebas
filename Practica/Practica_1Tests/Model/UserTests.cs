using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Model;
using Practica.Utils;
using System;
using System.Collections.Generic;

namespace Practica.Model.Tests {
    [TestClass()]
    public class UserTests {
        [TestMethod()]
        public void UserTest_Constructor_Parameters() {
            // Arrange
            string name = "Juan";
            string lastName = "Perez";
            string email = "juan@example.com";
            string password = "password123";

            // Act
            User user = new User(name, lastName, email, password);

            // Assert
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(lastName, user.LastName);
            Assert.AreEqual(email, user.Email);
            Assert.IsFalse(user.Is_Subscription, "La suscripción debería ser falsa por defecto.");
            Assert.IsFalse(user.Is_superuser, "Is_superuser debería ser falsa por defecto.");
            Assert.IsFalse(user.Is_active, "Is_active debería ser falsa por defecto.");
            Assert.AreEqual(UserState.Unactive, user.State, "El estado del usuario debería ser 'Unactive' por defecto.");
            Assert.IsTrue(user.Last_login <= DateTime.Now, "La última fecha de login debería ser igual o anterior a la fecha actual.");
        }

        [TestMethod()]
        public void UserTest_Constructor_NoParameters() {
            // Arrange & Act
            User user = new User();

            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Admin", user.Name);
            Assert.AreEqual("Admin", user.LastName);
            Assert.AreEqual("example@example.com", user.Email);
            Assert.IsFalse(user.Is_Subscription, "La suscripción debería ser falsa por defecto.");
            Assert.IsTrue(user.Is_superuser, "Is_superuser debería ser true por defecto.");
            Assert.IsFalse(user.Is_active, "Is_active debería ser falsa por defecto.");
            Assert.AreEqual(UserState.Unactive, user.State, "El estado del usuario 'Admin' debería ser 'Unactive' por defecto.");
            Assert.IsTrue(user.Last_login <= DateTime.Now, "La última fecha de login debería ser igual o anterior a la fecha actual.");
        }

        [TestMethod]
        public void RegisterTest_Success() {
            // Arrange
            User user = new User();
            string newName = "Nuevo";
            string newLastName = "Usuario";
            string newEmail = "nuevo@example.com";
            string newPassword = "Password_123";

            // Act
            user.Register(newName, newLastName, newEmail, newPassword);

            // Assert
            Assert.AreEqual(newName, user.Name, "El nombre del usuario debería ser actualizado.");
            Assert.AreEqual(newLastName, user.LastName, "El apellido del usuario debería ser actualizado.");
            Assert.AreEqual(newEmail, user.Email, "El email del usuario debería ser actualizado.");
        }

        [DataTestMethod]
        [DataRow("", "Usuario", "email@test.com", "Password_123", typeof(ArgumentNullException), "El registro debería fallar si el nombre es nulo o vacío.")]
        [DataRow("Nuevo", "", "email@test.com", "Password_123", typeof(ArgumentNullException), "El registro debería fallar si el apellido es nulo o vacío.")]
        [DataRow("Nuevo", "Usuario", "", "Password_123", typeof(ArgumentNullException), "El registro debería fallar si el email es nulo o vacío.")]
        [DataRow("Nuevo", "Usuario", "email@test.com", "", typeof(ArgumentNullException), "El registro debería fallar si la contraseña es nula o vacía.")]
        [DataRow("Nuevo", "Usuario", "email@test.com", "pass", typeof(ArgumentException), "El registro debería fallar con una contraseña inválida.")]
        public void RegisterTest_Failure(string name, string lastName, string email, string password, Type exceptionType, string message) {
            // Arrange
            User user = new User("admin", "admin", "email@test.com", "pass_ok");

            // Act & Assert
            AssertThrowsOfType(() => user.Register(name, lastName, email, password), exceptionType, message);
        }

        [TestMethod]
        public void LoginTest_Success() {
            // Arrange
            string userEmail = "test@example.com";
            string userPassword = "test_password";
            User user = new User("Test", "User", userEmail, userPassword);

            // Act
            user.Login(userEmail, userPassword);

            // Assert
            Assert.AreEqual(UserState.Active, user.State, "El estado del usuario debería ser 'Active' después del login.");
        }

        [DataTestMethod]
        [DataRow("test@example.com", "wrong_password", typeof(InvalidOperationException), "Login debería fallar con contraseña incorrecta.")]
        [DataRow("wrong@example.com", "test_password", typeof(InvalidOperationException), "Login debería fallar con email incorrecto.")]
        [DataRow("test@example.com", null, typeof(ArgumentNullException), "Login debería fallar con contraseña nula.")]
        [DataRow(null, "test_password", typeof(ArgumentNullException), "Login debería fallar con email nulo.")] 
        public void LoginTest_Failure(string loginEmail, string loginPassword, Type exceptionType, string message) {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "test_password");

            // Act & Assert
            AssertThrowsOfType(() => user.Login(loginEmail, loginPassword), exceptionType, message);
        }

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ChangePasswordTest_Success() {
            string email = "change@example.com";
            string initialPassword = "oldPassword123";
            string newPassword = "new_Password456";
            User user = new User("Change", "Test", email, initialPassword);

            user.ChangePassword(initialPassword, newPassword);

            string encrypted = Utils.Password.EncriptPassword(newPassword);

            Assert.IsTrue(Utils.Password.VerifyPassword("new_Password456", user.Password), "La nueva contraseña debería estar guardada.");
        }

        [DataTestMethod]
        [DataRow("wrongPassword", "newPassword456", typeof(InvalidOperationException), "El cambio debería fallar con la contraseña actual incorrecta.")]
        [DataRow("", "newPassword456", typeof(ArgumentNullException), "El cambio debería fallar con contraseña actual en blanco.")]
        [DataRow("oldPassword123", "new", typeof(ArgumentException), "El cambio debería fallar si la nueva contraseña no es válida.")]
        public void ChangePasswordTest_Failure(string oldPassword, string newPassword, Type exceptionType, string message) {
            // Arrange
            string initialPassword = "oldPassword123";
            User user = new User("Change", "Test", "change@example.com", initialPassword);

            // Act & Assert
            AssertThrowsOfType(() => user.ChangePassword(oldPassword, newPassword), exceptionType, message);
        }

        [TestMethod()]
        public void LogoutTest_Success() {
            // Arrange
            User user = new User("test", "user", "test@user.com", "password");
            user.State = UserState.Active;

            // Act
            user.Logout();

            // Assert
            Assert.AreEqual(UserState.Unactive, user.State, "El estado del usuario debe cambiar a 'Unactive' después del logout.");
        }

        [TestMethod()]
        public void LogoutTest_Failure() {
            // Arrange
            User user = new User("test", "user", "test@user.com", "password");
            user.State = UserState.Unactive;

            // Act & Assert
            AssertThrowsOfType(() => user.Logout(), typeof(InvalidOperationException), "El logout debería fallar si el usuario ya no está activo.");
        }

        [TestMethod]
        public void ChangeDetailsTest_Success() {
            // Arrange
            User user = new User("Original", "Original", "original@example.com", "password123");
            string newName = "Nuevo";
            string newLastName = "Nombre";
            string newEmail = "nuevo.email@example.com";

            // Act
            user.ChangeDetails(newName, newLastName, newEmail);

            // Assert
            Assert.AreEqual(newName, user.Name, "El nombre no se actualizó correctamente.");
            Assert.AreEqual(newLastName, user.LastName, "El apellido no se actualizó correctamente.");
            Assert.AreEqual(newEmail, user.Email, "El email no se actualizó correctamente.");
        }

        [DataTestMethod]
        [DataRow("", "Nuevo", "nuevo.email@example.com", typeof(ArgumentNullException), "El cambio de detalles debería fallar con nombre nulo.")]
        [DataRow("Nuevo", "", "nuevo.email@example.com", typeof(ArgumentNullException), "El cambio de detalles debería fallar con apellido nulo.")]
        [DataRow("Nuevo", "Nombre", "", typeof(ArgumentNullException), "El cambio de detalles debería fallar con email nulo.")]
        public void ChangeDetailsTest_Failure(string newName, string newLastName, string newEmail, Type exceptionType, string message) {
            // Arrange
            User user = new User("Original", "Original", "original@example.com", "password123");
            string originalName = user.Name;
            string originalLastName = user.LastName;
            string originalEmail = user.Email;

            // Act & Assert
            AssertThrowsOfType(() => user.ChangeDetails(newName, newLastName, newEmail), exceptionType, message);

            // Assert - datos originales inalterados
            Assert.AreEqual(originalName, user.Name, "Los datos no deberían haber cambiado después de una excepción.");
            Assert.AreEqual(originalLastName, user.LastName, "Los datos no deberían haber cambiado después de una excepción.");
            Assert.AreEqual(originalEmail, user.Email, "Los datos no deberían haber cambiado después de una excepción.");
        }

        [TestMethod()]
        public void SubscribeTest_Success() {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "password");

            // Act
            user.Subscribe();

            // Assert
            Assert.IsTrue(user.Is_Subscription, "La suscripción debería ser true después de suscribirse.");
        }

        [TestMethod]
        public void SubscribeTest_Failure() {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "password");
            user.Subscribe(); // Suscribe por primera vez para forzar el fallo en el siguiente intento.

            // Act & Assert
            AssertThrowsOfType(() => user.Subscribe(), typeof(InvalidOperationException), "La suscripción debería fallar si el usuario ya está suscrito.");
        }

        [TestMethod()]
        public void UnsubscribeTest() {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "password");
            user.Is_Subscription = true;

            // Act
            user.Unsubscribe();

            // Assert
            Assert.IsFalse(user.Is_Subscription, "La suscripción debería ser false después de desuscribirse.");
        }

        [TestMethod]
        public void EqualsTest() {
            // Arrange
            User user1 = new User("Ana", "García", "ana@example.com", "password123") { Id = 1 };
            User user2 = new User("Ana", "García", "ana@example.com", "password123") { Id = 1 };
            User user3 = new User("Pedro", "López", "pedro@example.com", "password456") { Id = 2 };
            User user4 = new User("Pedro", "López", "pedro@example.com", "password456") { Id = 1 }; // mismo Id, diferente info

            // Act & Assert
            Assert.IsTrue(user1.Equals(user2), "Usuarios con los mismos datos deberían ser iguales.");
            Assert.IsFalse(user1.Equals(user3), "Usuarios con datos distintos no deberían ser iguales.");
            Assert.IsFalse(user1.Equals(user4), "Usuarios con el mismo Id pero diferente información no deberían ser iguales.");
            Assert.IsFalse(user1.Equals(null), "Un usuario no debería ser igual a null.");
        }

        [TestMethod]
        public void GetHashCodeTest() {
            // Arrange
            User user1 = new User("Ana", "García", "ana@example.com", "Password_123") { Id = 1 };
            User user2 = new User("Ana", "García", "ana@example.com", "Password_123") { Id = 1 };
            User user3 = new User("Pedro", "López", "pedro@example.com", "Password_123") { Id = 1 }; // mismo Id, diferente info

            // Act & Assert
            Assert.AreEqual(user1.GetHashCode(), user2.GetHashCode(), "Usuarios con los mismos datos deben tener el mismo hash.");
            Assert.AreNotEqual(user1.GetHashCode(), user3.GetHashCode(), "Usuarios con el mismo Id pero diferente información no deben tener el mismo hash.");
        }

        // 🔹 Helper unificado para todos los tests de fallo
        private void AssertThrowsOfType(Action action, Type expectedType, string message = null) {
            try {
                action();
                Assert.Fail(message ?? $"Se esperaba una excepción de tipo {expectedType.Name}.");
            } catch (Exception ex) {
                Assert.IsInstanceOfType(ex, expectedType, message);
            }
        }
    }
}
