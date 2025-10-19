using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Utils;

namespace Practica.Utils.Tests {
    [TestClass()]
    public class EmailTests {
        #region Valid Email Formats

        [DataTestMethod]
        [DataRow("test@example.com")]
        [DataRow("john.doe@example.co.uk")]
        [DataRow("user+alias@subdomain.example.com")]
        [DataRow("user123@example-one.com")]
        [DataRow("email@machine.museum")]
        public void IsValidFormat_WhenEmailIsValid_ShouldReturnTrue(string validEmail) {
            // Act
            bool result = Email.IsValidFormat(validEmail);
            // Assert
            Assert.IsTrue(result, $"El email '{validEmail}' debería ser válido.");
        }

        #endregion

        #region Invalid Formats (Caught by initial checks)

        [DataTestMethod]
        [DataRow(null, "Un email nulo debe devolver false.")]
        [DataRow("", "Un email vacío debe devolver false.")]
        [DataRow("    ", "Un email con solo espacios debe devolver false.")]
        [DataRow("username @ domain.com", "Un email con espacios intermedios debe ser inválido.")]
        [DataRow(" test@test.com", "Un email con espacios al inicio debe ser inválido.")]
        public void IsValidFormat_WhenEmailIsInvalidByWhitespace_ShouldReturnFalse(string email, string message) {
            // Act
            bool result = Email.IsValidFormat(email);
            // Assert
            Assert.IsFalse(result, message);
        }

        #endregion

        #region Invalid Formats (Caught by try-catch and custom logic)

        [DataTestMethod]
        // Casos que DEBEN lanzar FormatException y ser atrapados por el CATCH
        [DataRow("plainaddress")]             // No tiene '@'
        [DataRow("@missingusername.com")]     // Falta la parte local
        [DataRow("username@.com")]            // Dominio inválido
        [DataRow("test@domain.com.")]         // Punto al final del dominio

        // Casos que NO lanzan excepción pero fallan la LÓGICA PERSONALIZADA
        [DataRow("username@domain..com")]     // Falla la comprobación de '..'
        [DataRow("username@domain.c")]        // Falla la comprobación de longitud del TLD
        public void IsValidFormat_WhenEmailIsStructurallyInvalid_ShouldReturnFalse(string invalidEmail) {
            // Act
            bool result = Email.IsValidFormat(invalidEmail);
            // Assert
            Assert.IsFalse(result, $"El email '{invalidEmail}' debería ser inválido.");
        }

        #endregion
    }
}