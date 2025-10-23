using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Utils;

namespace Practica.Utils.Tests {
    [TestClass]
    public class IBANTests {
        #region IBANs Válidos

        [TestMethod]
        [DataRow("ES9121000418450200051332", "IBAN válido sin espacios")]
        [DataRow("ES91 2100 0418 4502 0005 1332", "IBAN válido con espacios")]
        [DataRow("es9121000418450200051332", "IBAN válido con 'es' en minúsculas")]
        [DataRow("ES6000491500051234567892", "Otro IBAN válido de ejemplo")]
        public void ValidarIBAN_ConIBANValidos_DebeDevolverTrue(string iban, string descripcion) {
            // Act
            bool resultado = IBAN.validar_IBAN(iban);

            // Assert
            Assert.IsTrue(resultado, $"El IBAN '{iban}' debería ser válido. Caso: {descripcion}");
        }

        #endregion

        #region IBANs con Formato Inválido

        [TestMethod]
        [DataRow(null, "IBAN nulo")]
        [DataRow("", "IBAN vacío")]
        [DataRow("    ", "IBAN con solo espacios")]
        [DataRow("ES123456", "IBAN demasiado corto")]
        [DataRow("ES12345678901234567890123", "IBAN demasiado largo")]
        [DataRow("DE9121000418450200051332", "IBAN con prefijo de otro país (DE)")]
        [DataRow("ES9121000418A50200051332", "IBAN con letra en la parte numérica")]
        public void ValidarIBAN_ConFormatoInvalido_DebeDevolverFalse(string iban, string descripcion) {
            // Act
            bool resultado = IBAN.validar_IBAN(iban);

            // Assert
            Assert.IsFalse(resultado, $"El IBAN '{iban}' debería ser inválido. Caso: {descripcion}");
        }

        #endregion

        #region IBANs con Dígito de Control Inválido

        [TestMethod]
        [DataRow("ES9021000418450200051332", "Dígito de control '90' es incorrecto, debería ser '91'")]
        [DataRow("ES9121000418450200051333", "Último dígito cambiado, cálculo incorrecto")]
        [DataRow("ES6100491500051234567892", "Dígito de control '61' es incorrecto, debería ser '60'")]
        public void ValidarIBAN_ConDigitoDeControlIncorrecto_DebeDevolverFalse(string iban, string descripcion) {
            // Act
            bool resultado = IBAN.validar_IBAN(iban);

            // Assert
            Assert.IsFalse(resultado, $"El IBAN '{iban}' debería ser inválido. Caso: {descripcion}");
        }

        #endregion
    }
}