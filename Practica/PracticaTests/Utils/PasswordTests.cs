using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Utils.Tests {
    [TestClass()]
    public class PasswordTests {
        #region CheckPassword Tests

        [TestMethod]
        public void CheckPassword_WhenPasswordIsValid_ReturnsTrue() {
            // Arrange: Una contraseña que cumple todos los requisitos
            string validPassword = "ValidPassword123!";

            // Act
            bool result = Password.CheckPassword(validPassword);

            // Assert
            Assert.IsTrue(result, "La contraseña debería ser válida.");
        }

        [DataTestMethod]
        [DataRow(null, "Contraseña nula")]
        [DataRow("", "Contraseña vacía")]
        [DataRow("Short1!", "Menos de 12 caracteres")]
        [DataRow("nouppercase123!", "Sin mayúsculas")]
        [DataRow("NOLOWERCASE123!", "Sin minúsculas")]
        [DataRow("NoDigitsHere!", "Sin dígitos")]
        [DataRow("NoSpecialChar123", "Sin caracteres especiales")]
        public void CheckPassword_WhenPasswordIsInvalid_ReturnsFalse(string invalidPassword, string message) {
            // Act
            bool result = Password.CheckPassword(invalidPassword);

            // Assert
            Assert.IsFalse(result, $"La contraseña '{invalidPassword}' debería ser inválida. Razón: {message}");
        }

        #endregion

        #region EncriptPassword Tests

        [TestMethod]
        public void EncriptPassword_WithSameInput_ReturnsSameHash() {
            // Arrange
            string password = "mySecretPassword";

            // Act
            string hash1 = Password.EncriptPassword(password);
            string hash2 = Password.EncriptPassword(password);

            // Assert
            Assert.IsNotNull(hash1);
            Assert.AreEqual(hash1, hash2, "El hash debe ser determinista (mismo input, mismo output).");
        }

        [TestMethod]
        public void EncriptPassword_WithDifferentInput_ReturnsDifferentHash() {
            // Arrange
            string passwordA = "mySecretPassword1";
            string passwordB = "mySecretPassword2";

            // Act
            string hashA = Password.EncriptPassword(passwordA);
            string hashB = Password.EncriptPassword(passwordB);

            // Assert
            Assert.AreNotEqual(hashA, hashB, "Hashes de contraseñas diferentes no deberían ser iguales.");
        }

        [TestMethod]
        public void EncriptPassword_WithEmptyString_ReturnsValidHash() {
            // Act
            string hash = Password.EncriptPassword("");

            // Assert
            // El hash de una cadena vacía para SHA256 en Base64 es siempre el mismo
            Assert.AreEqual("47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU=", hash);
        }

        #endregion

        #region VerifyPassword Tests

        [TestMethod]
        public void VerifyPassword_WhenPasswordMatchesHash_ReturnsTrue() {
            // Arrange
            string rawPassword = "mySecurePassword123!";
            string storedHash = Password.EncriptPassword(rawPassword);

            // Act
            bool result = Password.VerifyPassword(rawPassword, storedHash);

            // Assert
            Assert.IsTrue(result, "La verificación debería ser exitosa si la contraseña es correcta.");
        }

        [TestMethod]
        public void VerifyPassword_WhenPasswordDoesNotMatchHash_ReturnsFalse() {
            // Arrange
            string correctPassword = "mySecurePassword123!";
            string incorrectPassword = "wrongPassword";
            string storedHash = Password.EncriptPassword(correctPassword);

            // Act
            bool result = Password.VerifyPassword(incorrectPassword, storedHash);

            // Assert
            Assert.IsFalse(result, "La verificación debería fallar si la contraseña es incorrecta.");
        }

        #endregion
    }
}