using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_1.Model.Tests
{
    [TestClass()]
    public class UserTests {
        [TestMethod()]
        public void UserTest_Constructor_Parameters() {
            // Arrange
            int id = 1;
            string name = "Juan";
            string lastName = "Perez";
            string email = "juan@example.com";
            string password = "password123";
            bool subscription = false;
            bool is_superuser = false;
            bool is_active = false;
            DateTime last_login = DateTime.Now;

            // Act
            User user = new User(name, lastName, email, password);

            // Assert
            Assert.AreEqual(id, user.Id);
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(lastName, user.LastName);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(subscription, user.Subscription);
            Assert.AreEqual(is_superuser, user.Is_superuser);
            Assert.AreEqual(is_active, user.Is_active);
            Assert.AreEqual(last_login, user.Last_login);
        }

        [TestMethod()]
        public void UserTest_Constructor_NoParameters() {
            // Arrange & Act
            User user = new User();

            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Pedro", user.Name);
            Assert.AreEqual("Gonzalez", user.LastName);
            Assert.AreEqual("example@example.com", user.Email);
            Assert.AreEqual(true, user.Subscription);
            Assert.AreEqual(true, user.Is_superuser);
            Assert.AreEqual(false, user.Is_active);
            Assert.IsTrue(user.Last_login <= DateTime.Now);
        }

        [TestMethod()]
        public void LoginTest() {
            // Arrange
            string email = "test@example.com";
            string password = "test_password";
            User user = new User("Test", "User", email, "test_password");

            // Act
            bool loginSuccess = user.Login(email, password);
            bool loginFail_IncorrectPassword = user.Login(email, "wrong_password");
            bool loginFail_IncorrectEmail = user.Login("wrong@example.com", password);
            bool loginFail_NullPassword = user.Login(email, null);
            bool loginFail_NullEmail = user.Login(null, password);

            // Assert
            Assert.IsTrue(loginSuccess, "El login debería ser exitoso con credenciales correctas.");
            Assert.IsFalse(loginFail_IncorrectPassword, "El login debería fallar con una contraseña incorrecta.");
            Assert.IsFalse(loginFail_IncorrectEmail, "El login debería fallar con un email incorrecto.");
            Assert.IsFalse(loginFail_NullPassword, "El login debería fallar con una contraseña nula.");
            Assert.IsFalse(loginFail_NullEmail, "El login debería fallar con un email nulo.");
        }

        [TestMethod()]
        public void ChangePasswordTest() {
            // Arrange
            string email = "change@example.com";
            string initialPassword = "oldPassword123";
            string newPassword = "newPassword456";
            User user = new User("Change", "Test", email, "oldPassword123");

            // Act
            bool changeSuccess = user.ChangePassword(initialPassword, newPassword);
            bool changeFail = user.ChangePassword("wrongPassword", newPassword);
            bool changeFail2 = user.ChangePassword("", newPassword);

            // Assert
            Assert.IsTrue(changeSuccess, "El cambio de contraseña debería ser exitoso.");
            Assert.IsTrue(user.Login(email, newPassword), "El usuario debería poder iniciar sesión con la nueva contraseña.");
            Assert.IsFalse(changeFail, "El cambio de contraseña debería fallar con la contraseña actual incorrecta.");
            Assert.IsFalse(changeFail2, "El cambio de contraseña debería fallar si no se introduce contraseña o se introduce en blanco.");
            Assert.IsFalse(user.Login(email, initialPassword), "El usuario no debería poder iniciar sesión con la contraseña anterior.");
        }

        [TestMethod()]
        public void EqualsTest() {
            // Arrange
            User user1 = new User("Ana", "Gomez", "ana@test.com", "pass1");
            User user2 = new User("Ana", "Gomez", "ana@test.com", "pass1");
            User user3 = new User("Luis", "Hernandez", "luis@test.com", "pass2");

            // Assert
            Assert.IsTrue(user1.Equals(user2), "Dos objetos con las mismas propiedades deberían ser iguales.");
            Assert.IsFalse(user1.Equals(user3), "Dos objetos con diferentes propiedades no deberían ser iguales.");
            Assert.IsFalse(user1.Equals(null), "Un objeto no debería ser igual a null.");
        }

        [TestMethod()]
        public void GetHashCodeTest() {
            // Arrange
            User user1 = new User("Ana", "Gomez", "ana@test.com", "pass1");
            User user2 = new User("Ana", "Gomez", "ana@test.com", "pass1");

            // Assert
            Assert.AreEqual(user1.GetHashCode(), user2.GetHashCode(), "Dos objetos iguales deben tener el mismo código hash.");
        }
    }
}