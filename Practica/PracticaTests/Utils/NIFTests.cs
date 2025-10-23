using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Utils; 

namespace Practica.Utils.Tests {
    [TestClass]
    public class NIFTests {
        #region NIFs Válidos
        [TestMethod]
        [DataRow("12345678Z", "NIF estándar con letra mayúscula")]
        [DataRow("83655302R", "Otro NIF estándar válido")]
        [DataRow("00000000T", "NIF con todos los números en cero")]
        [DataRow("12345678z", "NIF válido con letra minúscula para probar la conversión")]
        public void ValidarNIF_ConNIFValidos_DebeDevolverTrue(string nif, string descripcion) {
            // Act
            bool resultado = NIF.validar_NIF(nif);

            // Assert
            Assert.IsTrue(resultado, $"El NIF '{nif}' debería ser válido. Caso: {descripcion}");
        }

        #endregion

        #region NIFs con Formato Inválido

        [TestMethod]
        [DataRow(null, "NIF nulo")]
        [DataRow("", "NIF vacío")]
        [DataRow("        ", "NIF con solo espacios")]
        [DataRow("1234567", "NIF demasiado corto")]
        [DataRow("123456789A", "NIF demasiado largo")]
        [DataRow("1234567A8", "Letra en la parte numérica")]
        [DataRow("ABCDEFGHZ", "Toda la parte numérica son letras")]
        public void ValidarNIF_ConFormatoInvalido_DebeDevolverFalse(string nif, string descripcion) {
            // Act
            bool resultado = NIF.validar_NIF(nif);

            // Assert
            Assert.IsFalse(resultado, $"El NIF '{nif}' debería ser inválido. Caso: {descripcion}");
        }

        #endregion

        #region NIFs con Letra de Control Incorrecta

        [TestMethod]
        [DataRow("12345678A", "La letra correcta es Z")]
        [DataRow("87654321K", "La letra correcta es J")]
        [DataRow("00000000B", "La letra correcta es T")]
        public void ValidarNIF_ConLetraIncorrecta_DebeDevolverFalse(string nif, string descripcion) {
            // Act
            bool resultado = NIF.validar_NIF(nif);

            // Assert
            Assert.IsFalse(resultado, $"El NIF '{nif}' debería ser inválido. Caso: {descripcion}");
        }

        #endregion
    }
}