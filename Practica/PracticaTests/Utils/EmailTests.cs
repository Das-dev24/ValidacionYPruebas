using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Nodes;

namespace Practica.Utils.Tests {
    [TestClass]
    public class EmailTests {
        #region Correos con Formato Válido (desde JSON)

        public static IEnumerable<object[]> GetValidEmailData() {
            return LoadTestData("emails_validos.json");
        }

        [TestMethod]
        [DynamicData(nameof(GetValidEmailData))]
        public void IsValidFormat_WhenEmailIsValid_ShouldReturnTrue(string validEmail, string description) {
            // Act
            bool result = Email.IsValidFormat(validEmail);

            // Assert
            Assert.IsTrue(result, $"El email '{validEmail}' debería ser válido. Caso: {description}");
        }

        #endregion


        #region Correos con Formato Inválido por Espacios (desde JSON)

        public static IEnumerable<object[]> GetInvalidWhitespaceEmailData() {
            return LoadTestData("emails_invalidos_espacios.json");
        }

        [TestMethod]
        [DynamicData(nameof(GetInvalidWhitespaceEmailData))]
        public void IsValidFormat_WhenEmailContainsWhitespace_ShouldReturnFalse(string invalidEmail, string description) {
            // Act
            bool result = Email.IsValidFormat(invalidEmail);

            // Assert
            Assert.IsFalse(result, $"El email '{invalidEmail}' debería ser inválido. Caso: {description}");
        }

        #endregion


        #region Correos con Formato Inválido por Estructura (desde JSON)

        public static IEnumerable<object[]> GetInvalidStructuralEmailData() {
            return LoadTestData("emails_invalidos_estructura.json");
        }

        [TestMethod]
        [DynamicData(nameof(GetInvalidStructuralEmailData))]
        public void IsValidFormat_WhenEmailIsStructurallyInvalid_ShouldReturnFalse(string invalidEmail, string description) {
            // Act
            bool result = Email.IsValidFormat(invalidEmail);

            // Assert
            Assert.IsFalse(result, $"El email '{invalidEmail}' debería ser inválido. Caso: {description}");
        }

        #endregion


        #region Método Auxiliar de Carga de Datos

        private static IEnumerable<object[]> LoadTestData(string fileName) {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", fileName);
            string json = File.ReadAllText(filePath);

            JsonArray testCases = JsonNode.Parse(json).AsArray();

            foreach (var testCase in testCases) {
                string email = testCase["email"]?.GetValue<string>();
                string descripcion = testCase["descripcion"]?.GetValue<string>() ?? "Sin descripción";

                yield return new object[] { email, descripcion };
            }
        }

        #endregion
    }
}