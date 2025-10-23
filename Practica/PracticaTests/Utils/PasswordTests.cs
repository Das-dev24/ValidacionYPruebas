using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Practica.Utils.Tests {
    [TestClass]
    public class PasswordTests {
        #region CheckPassword Tests

        [TestMethod]
        public void CheckPassword_WhenPasswordIsValid_ReturnsTrue() {
            // Arrange
            string validPassword = "ValidPassword123!";
            // Act
            bool result = Password.CheckPassword(validPassword);
            // Assert
            Assert.IsTrue(result, "La contraseña debería ser válida.");
        }

        public static IEnumerable<object[]> GetInvalidPasswordTestData() {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "passwords_invalidas.csv");
            return File.ReadAllLines(filePath)
                       .Select(line => line.Split(';'))
                       .Select(fields => new object[] { fields[0], fields[1] });
        }

        [TestMethod]
        [DynamicData(nameof(GetInvalidPasswordTestData))]
        public void CheckPassword_WhenPasswordIsInvalid_DataDriven_ReturnsFalse(string invalidPassword, string message) {
            // Act
            bool result = Password.CheckPassword(invalidPassword);
            // Assert
            Assert.IsFalse(result, $"La contraseña '{invalidPassword}' debería ser inválida. Razón: {message}");
        }

        [TestMethod]
        public void CheckPassword_WhenPasswordIsNull_ReturnsFalse() {
            // Act
            bool result = Password.CheckPassword(null);
            // Assert
            Assert.IsFalse(result, "Una contraseña nula debería ser inválida.");
        }

        #endregion

        // --- RESTO DE TESTS SIN CAMBIOS ---

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