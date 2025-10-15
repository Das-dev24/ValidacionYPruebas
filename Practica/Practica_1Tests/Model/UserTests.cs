using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Model;
using Practica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Practica.Utils.Tests
{
    [TestClass()]
    public class PasswordTests
    {
        #region CheckPassword - Casos Válidos

        [TestMethod()]
        public void CheckPassword_PasswordValida_DebeRetornarTrue()
        {
            // Arrange - Contraseña con todos los requisitos: 12+ chars, mayúscula, minúscula, número, carácter especial
            string password = "Password123!";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordCon12Caracteres_DebeRetornarTrue()
        {
            // Arrange - Exactamente 12 caracteres (longitud mínima)
            string password = "Password12!@";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordLarga_DebeRetornarTrue()
        {
            // Arrange - Password muy larga con todos los requisitos
            string password = "ThisIsAVeryLongPassword123!@#$%";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordConTodosCaracteresEspeciales_DebeRetornarTrue()
        {
            // Arrange - Testing con diferentes caracteres especiales permitidos
            string[] passwordsValidas = new string[]
            {
                "Password123!",
                "Password123@",
                "Password123#",
                "Password123$",
                "Password123%",
                "Password123^",
                "Password123&",
                "Password123*",
                "Password123(",
                "Password123)",
                "Password123_",
                "Password123[",
                "Password123]",
                "Password123{",
                "Password123}",
                "Password123;",
                "Password123:",
                "Password123'",
                "Password123\"",
                "Password123,",
                "Password123.",
                "Password123<",
                "Password123>",
                "Password123/",
                "Password123?",
                "Password123`",
                "Password123~"
            };

            // Act & Assert
            foreach (var pwd in passwordsValidas)
            {
                bool result = Password.CheckPassword(pwd);
                Assert.IsTrue(result, $"La password '{pwd}' debería ser válida");
            }
        }

        [TestMethod()]
        public void CheckPassword_PasswordConMultiplesCaracteresEspeciales_DebeRetornarTrue()
        {
            // Arrange
            string password = "Pass!@#Word123$%^";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordConVariasMayusculas_DebeRetornarTrue()
        {
            // Arrange
            string password = "PASS word 123!";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordConVariosNumeros_DebeRetornarTrue()
        {
            // Arrange
            string password = "Password123456!";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region CheckPassword - Casos Inválidos

        [TestMethod()]
        public void CheckPassword_PasswordNull_DebeRetornarFalse()
        {
            // Arrange
            string password = null;

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordVacia_DebeRetornarFalse()
        {
            // Arrange
            string password = "";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordMenorA12Caracteres_DebeRetornarFalse()
        {
            // Arrange - 11 caracteres (1 menos del mínimo)
            string password = "Pass123!abc";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordSinMayuscula_DebeRetornarFalse()
        {
            // Arrange
            string password = "password123!";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordSinMinuscula_DebeRetornarFalse()
        {
            // Arrange
            string password = "PASSWORD123!";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordSinNumero_DebeRetornarFalse()
        {
            // Arrange
            string password = "PasswordOnly!";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordSinCaracterEspecial_DebeRetornarFalse()
        {
            // Arrange
            string password = "Password1234";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordCon11Caracteres_DebeRetornarFalse()
        {
            // Arrange - Exactamente 11 caracteres (justo debajo del límite)
            string password = "Pass1234!ab";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordSoloEspacios_DebeRetornarFalse()
        {
            // Arrange
            string password = "            ";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckPassword_PasswordConCaracterNoEspecial_DebeRetornarFalse()
        {
            // Arrange - Tiene todo excepto caracter especial válido
            string password = "Password123ñ";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region CheckPassword - Casos Borde

        [TestMethod()]
        public void CheckPassword_PasswordExactamente12CaracteresValida_DebeRetornarTrue()
        {
            // Arrange - Caso límite: exactamente 12 caracteres
            string password = "Abcdefgh12!@";

            // Act
            bool result = Password.CheckPassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckPassword_MultiplesValidaciones_CubreTodosLosCasos()
        {
            // Arrange - Array de passwords para cubrir todas las condiciones
            var testCases = new[]
            {
                new { Password = "Pass123!", Expected = false },          // Muy corta
                new { Password = "password123!@#", Expected = false },    // Sin mayúscula
                new { Password = "PASSWORD123!@#", Expected = false },    // Sin minúscula
                new { Password = "PasswordOnly!@", Expected = false },    // Sin número
                new { Password = "Password12345", Expected = false },     // Sin especial
                new { Password = "Password123!", Expected = true },       // Válida
                new { Password = "", Expected = false }                   // Vacía
            };

            // Act & Assert
            foreach (var testCase in testCases)
            {
                bool result = Password.CheckPassword(testCase.Password);
                Assert.AreEqual(testCase.Expected, result,
                    $"Password '{testCase.Password}' debería retornar {testCase.Expected}");
            }
        }

        #endregion

        #region EncriptPassword Tests

        [TestMethod()]
        public void EncriptPassword_PasswordValida_DebeRetornarHashBase64()
        {
            // Arrange
            string password = "MySecurePassword123!";

            // Act
            string hash = Password.EncriptPassword(password);

            // Assert
            Assert.IsNotNull(hash);
            Assert.IsTrue(hash.Length > 0);
            Assert.AreNotEqual(password, hash, "El hash no debe ser igual a la password original");
        }

        [TestMethod()]
        public void EncriptPassword_MismaPassword_DebeGenerarMismoHash()
        {
            // Arrange
            string password = "ConsistentPassword123!";

            // Act
            string hash1 = Password.EncriptPassword(password);
            string hash2 = Password.EncriptPassword(password);

            // Assert
            Assert.AreEqual(hash1, hash2, "El mismo password debe generar el mismo hash");
        }

        [TestMethod()]
        public void EncriptPassword_PasswordsDiferentes_DebeGenerarHashesDiferentes()
        {
            // Arrange
            string password1 = "Password123!";
            string password2 = "Password456!";

            // Act
            string hash1 = Password.EncriptPassword(password1);
            string hash2 = Password.EncriptPassword(password2);

            // Assert
            Assert.AreNotEqual(hash1, hash2, "Passwords diferentes deben generar hashes diferentes");
        }

        [TestMethod()]
        public void EncriptPassword_PasswordVacia_DebeGenerarHash()
        {
            // Arrange
            string password = "";

            // Act
            string hash = Password.EncriptPassword(password);

            // Assert
            Assert.IsNotNull(hash);
            Assert.IsTrue(hash.Length > 0);
        }

        [TestMethod()]
        public void EncriptPassword_PasswordConCaracteresEspeciales_DebeGenerarHash()
        {
            // Arrange
            string password = "P@ssw0rd!#$%^&*()";

            // Act
            string hash = Password.EncriptPassword(password);

            // Assert
            Assert.IsNotNull(hash);
            Assert.IsTrue(hash.Length > 0);
        }

        [TestMethod()]
        public void EncriptPassword_PasswordConUnicode_DebeGenerarHash()
        {
            // Arrange
            string password = "Contraseña123!ñáéíóú";

            // Act
            string hash = Password.EncriptPassword(password);

            // Assert
            Assert.IsNotNull(hash);
            Assert.IsTrue(hash.Length > 0);
        }

        [TestMethod()]
        public void EncriptPassword_HashEsBase64Valido_DebePoderConvertirse()
        {
            // Arrange
            string password = "TestPassword123!";

            // Act
            string hash = Password.EncriptPassword(password);

            // Assert - Verificar que es Base64 válido intentando convertirlo de vuelta
            try
            {
                byte[] bytes = Convert.FromBase64String(hash);
                Assert.IsTrue(bytes.Length > 0, "El hash debe ser Base64 válido");
            }
            catch (FormatException)
            {
                Assert.Fail("El hash no es un string Base64 válido");
            }
        }

        [TestMethod()]
        public void EncriptPassword_SHA256Genera32Bytes_DebeTener44CaracteresBase64()
        {
            // Arrange
            string password = "AnyPassword123!";

            // Act
            string hash = Password.EncriptPassword(password);
            byte[] hashBytes = Convert.FromBase64String(hash);

            // Assert
            // SHA256 genera 32 bytes, que en Base64 son aproximadamente 44 caracteres
            Assert.AreEqual(32, hashBytes.Length, "SHA256 debe generar exactamente 32 bytes");
        }

        #endregion

        #region VerifyPassword Tests

        [TestMethod()]
        public void VerifyPassword_PasswordCorrectaYHashValido_DebeRetornarTrue()
        {
            // Arrange
            string password = "MyPassword123!";
            string storedHash = Password.EncriptPassword(password);

            // Act
            bool result = Password.VerifyPassword(password, storedHash);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void VerifyPassword_PasswordIncorrectaYHashValido_DebeRetornarFalse()
        {
            // Arrange
            string correctPassword = "CorrectPassword123!";
            string wrongPassword = "WrongPassword456!";
            string storedHash = Password.EncriptPassword(correctPassword);

            // Act
            bool result = Password.VerifyPassword(wrongPassword, storedHash);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void VerifyPassword_PasswordVaciaConHashDePasswordVacia_DebeRetornarTrue()
        {
            // Arrange
            string password = "";
            string storedHash = Password.EncriptPassword(password);

            // Act
            bool result = Password.VerifyPassword(password, storedHash);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void VerifyPassword_PasswordConEspacios_DebeDistinguirDePasswordSinEspacios()
        {
            // Arrange
            string password1 = "Password 123!";
            string password2 = "Password123!";
            string storedHash = Password.EncriptPassword(password1);

            // Act
            bool result1 = Password.VerifyPassword(password1, storedHash);
            bool result2 = Password.VerifyPassword(password2, storedHash);

            // Assert
            Assert.IsTrue(result1, "Password con espacios debe verificar correctamente");
            Assert.IsFalse(result2, "Password sin espacios no debe coincidir");
        }

        [TestMethod()]
        public void VerifyPassword_CaseSensitive_DebeDistinguirMayusculasDeMinusculas()
        {
            // Arrange
            string password = "Password123!";
            string passwordUpperCase = "PASSWORD123!";
            string storedHash = Password.EncriptPassword(password);

            // Act
            bool resultCorrect = Password.VerifyPassword(password, storedHash);
            bool resultWrong = Password.VerifyPassword(passwordUpperCase, storedHash);

            // Assert
            Assert.IsTrue(resultCorrect);
            Assert.IsFalse(resultWrong, "La verificación debe ser case-sensitive");
        }

        [TestMethod()]
        public void VerifyPassword_PasswordsConUnCaracterDeDiferencia_DebeRetornarFalse()
        {
            // Arrange
            string password = "Password123!";
            string similarPassword = "Password123@"; // Solo cambia el último caracter
            string storedHash = Password.EncriptPassword(password);

            // Act
            bool result = Password.VerifyPassword(similarPassword, storedHash);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void VerifyPassword_HashInvalido_DebeRetornarFalse()
        {
            // Arrange
            string password = "Password123!";
            string invalidHash = "InvalidHashString";

            // Act
            bool result = Password.VerifyPassword(password, invalidHash);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void VerifyPassword_MultiplesPasswordsConMismoHash_SoloUnaDebeValidar()
        {
            // Arrange
            string correctPassword = "CorrectOne123!";
            string storedHash = Password.EncriptPassword(correctPassword);
            string[] wrongPasswords = new string[]
            {
                "WrongOne123!",
                "CorrectOne123",
                "correctone123!",
                "CorrectOne1234!",
                "CorrectOne12!",
                ""
            };

            // Act
            bool correctResult = Password.VerifyPassword(correctPassword, storedHash);

            // Assert
            Assert.IsTrue(correctResult, "La password correcta debe validar");

            foreach (var wrongPwd in wrongPasswords)
            {
                bool wrongResult = Password.VerifyPassword(wrongPwd, storedHash);
                Assert.IsFalse(wrongResult, $"Password '{wrongPwd}' no debería validar");
            }
        }

        #endregion

        #region Integration Tests

        [TestMethod()]
        public void IntegrationTest_FlujoCompletoCheckEncriptVerify()
        {
            // Arrange
            string password = "SecurePassword123!";

            // Act - Paso 1: Verificar que cumple requisitos
            bool isValid = Password.CheckPassword(password);

            // Act - Paso 2: Encriptar
            string hash = Password.EncriptPassword(password);

            // Act - Paso 3: Verificar con password correcta
            bool verifyCorrect = Password.VerifyPassword(password, hash);

            // Act - Paso 4: Verificar con password incorrecta
            bool verifyIncorrect = Password.VerifyPassword("WrongPassword!", hash);

            // Assert
            Assert.IsTrue(isValid, "La password debe cumplir los requisitos");
            Assert.IsNotNull(hash, "Debe generar un hash");
            Assert.AreNotEqual(password, hash, "El hash no debe ser igual a la password");
            Assert.IsTrue(verifyCorrect, "Debe verificar correctamente con la password correcta");
            Assert.IsFalse(verifyIncorrect, "No debe verificar con password incorrecta");
        }

        #endregion

        #region Email Property Tests

        [TestMethod]
        public void Email_SetValidEmail_StoresCorrectly()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            string newEmail = "newemail@example.com";

            // Act
            user.Email = newEmail;

            // Assert
            Assert.AreEqual(newEmail, user.Email);
        }

        [TestMethod]
        public void Email_Constructor_StoresEmailCorrectly()
        {
            // Arrange
            string email = "constructor@example.com";

            // Act
            User user = new User("Test", "User", email, "Password123!");

            // Assert
            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        public void Email_SetDifferentValidFormats_AllAccepted()
        {
            // Arrange
            User user = new User("Test", "User", "initial@example.com", "Password123!");
            string[] validEmails = new string[]
            {
        "simple@example.com",
        "user.name@example.com",
        "user+tag@example.com",
        "user_name@example.com",
        "user123@example456.com",
        "user@sub.example.com",
        "user@example.co.uk",
        "test-user@example.com"
            };

            // Act & Assert
            foreach (var email in validEmails)
            {
                user.Email = email;
                Assert.AreEqual(email, user.Email, $"Email '{email}' debería almacenarse correctamente");
            }
        }

        [TestMethod]
        public void Email_CaseSensitive_StoresExactCase()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            string mixedCaseEmail = "Test.User@Example.COM";

            // Act
            user.Email = mixedCaseEmail;

            // Assert
            Assert.AreEqual(mixedCaseEmail, user.Email, "El email debe mantener mayúsculas y minúsculas");
        }

        [TestMethod]
        public void Email_SetEmpty_StoresEmpty()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Email = "";

            // Assert
            Assert.AreEqual("", user.Email);
        }

        [TestMethod]
        public void Email_SetNull_StoresNull()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Email = null;

            // Assert
            Assert.IsNull(user.Email);
        }

        [TestMethod]
        public void Email_MultipleChanges_StoresLatestValue()
        {
            // Arrange
            User user = new User("Test", "User", "initial@example.com", "Password123!");

            // Act
            user.Email = "first@example.com";
            user.Email = "second@example.com";
            user.Email = "final@example.com";

            // Assert
            Assert.AreEqual("final@example.com", user.Email);
        }

        [TestMethod]
        public void Email_WithSpaces_StoresWithSpaces()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            string emailWithSpaces = " user@example.com ";

            // Act
            user.Email = emailWithSpaces;

            // Assert
            Assert.AreEqual(emailWithSpaces, user.Email, "El email debe almacenarse tal como se proporciona, incluyendo espacios");
        }

        [TestMethod]
        public void Email_VeryLongEmail_StoresCorrectly()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            string longEmail = "very.long.email.address.with.many.dots@subdomain.example.com";

            // Act
            user.Email = longEmail;

            // Assert
            Assert.AreEqual(longEmail, user.Email);
        }

        [TestMethod]
        public void Email_SpecialCharacters_StoresCorrectly()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act & Assert - Caracteres válidos en emails
            user.Email = "user+filter@example.com";
            Assert.AreEqual("user+filter@example.com", user.Email);

            user.Email = "user_name@example.com";
            Assert.AreEqual("user_name@example.com", user.Email);

            user.Email = "user.name@example.com";
            Assert.AreEqual("user.name@example.com", user.Email);

            user.Email = "user-name@example.com";
            Assert.AreEqual("user-name@example.com", user.Email);
        }

        [TestMethod]
        public void Email_SetInvalidFormat_StillStores()
        {
            // Arrange
            User user = new User("Test", "User", "valid@example.com", "Password123!");
            string invalidEmail = "not-an-email";

            // Act
            user.Email = invalidEmail;

            // Assert
            // La propiedad Email no valida el formato, solo almacena
            Assert.AreEqual(invalidEmail, user.Email);
        }

        [TestMethod]
        public void Email_GetAfterSet_ReturnsConsistentValue()
        {
            // Arrange
            User user = new User("Test", "User", "initial@example.com", "Password123!");
            string testEmail = "consistent@example.com";

            // Act
            user.Email = testEmail;
            string firstGet = user.Email;
            string secondGet = user.Email;
            string thirdGet = user.Email;

            // Assert
            Assert.AreEqual(testEmail, firstGet);
            Assert.AreEqual(testEmail, secondGet);
            Assert.AreEqual(testEmail, thirdGet);
            Assert.AreEqual(firstGet, secondGet);
            Assert.AreEqual(secondGet, thirdGet);
        }

        [TestMethod]
        public void Email_ChangeDetails_UpdatesEmailCorrectly()
        {
            // Arrange
            User user = new User("Original", "User", "original@example.com", "Password123!");
            string newEmail = "updated@example.com";

            // Act
            user.ChangeDetails("New", "Name", newEmail);

            // Assert
            Assert.AreEqual(newEmail, user.Email);
        }

        [TestMethod]
        public void Email_Login_UsesEmailForAuthentication()
        {
            // Arrange
            string email = "login@example.com";
            string password = "TestPassword123!";
            User user = new User("Test", "User", email, password);

            // Act
            user.Login(email, password);

            // Assert
            Assert.AreEqual(UserState.Active, user.State, "Login debe funcionar con el email correcto");
        }

        [TestMethod]
        public void Email_TwoUsersWithSameEmail_AreIndependent()
        {
            // Arrange
            string sharedEmail = "shared@example.com";
            User user1 = new User("User", "One", sharedEmail, "Password123!");
            User user2 = new User("User", "Two", sharedEmail, "Password456!");

            // Act
            user1.Email = "changed@example.com";

            // Assert
            Assert.AreEqual("changed@example.com", user1.Email);
            Assert.AreEqual(sharedEmail, user2.Email, "El email de user2 no debe cambiar");
        }

        [TestMethod]
        public void Email_Equals_ComparesEmailCorrectly()
        {
            // Arrange
            User user1 = new User("Test", "User", "test@example.com", "Password123!") { Id = 1 };
            User user2 = new User("Test", "User", "test@example.com", "Password123!") { Id = 1 };
            User user3 = new User("Test", "User", "different@example.com", "Password123!") { Id = 1 };

            // Act & Assert
            Assert.IsTrue(user1.Equals(user2), "Usuarios con el mismo email deben ser iguales");
            Assert.IsFalse(user1.Equals(user3), "Usuarios con diferente email no deben ser iguales");
        }

        [TestMethod]
        public void Email_GetHashCode_ConsidersEmail()
        {
            // Arrange
            User user1 = new User("Test", "User", "test@example.com", "Password123!") { Id = 1 };
            User user2 = new User("Test", "User", "test@example.com", "Password123!") { Id = 1 };
            User user3 = new User("Test", "User", "different@example.com", "Password123!") { Id = 1 };

            // Act
            int hash1 = user1.GetHashCode();
            int hash2 = user2.GetHashCode();
            int hash3 = user3.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2, "Usuarios iguales deben tener el mismo hash");
            Assert.AreNotEqual(hash1, hash3, "Usuarios diferentes deben tener hash diferente");
        }

        [TestMethod]
        public void Email_AfterMultipleOperations_MaintainsIntegrity()
        {
            // Arrange
            string initialEmail = "initial@example.com";
            User user = new User("Test", "User", initialEmail, "Password123!");

            // Act - Realizar múltiples operaciones
            user.Login(initialEmail, "Password123!");
            user.Subscribe();
            user.ChangePassword("Password123!", "NewPassword456!");
            string currentEmail = user.Email;
            user.Logout();
            string finalEmail = user.Email;

            // Assert
            Assert.AreEqual(initialEmail, currentEmail, "El email no debe cambiar durante otras operaciones");
            Assert.AreEqual(initialEmail, finalEmail, "El email debe mantenerse después de logout");
        }

        #endregion

        #region Id Property Tests

        [TestMethod]
        public void Id_SetValue_StoresCorrectly()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Id = 100;

            // Assert
            Assert.AreEqual(100, user.Id);
        }

        [TestMethod]
        public void Id_MultipleUsers_CanHaveDifferentIds()
        {
            // Arrange
            User user1 = new User("User", "One", "user1@example.com", "Password123!");
            User user2 = new User("User", "Two", "user2@example.com", "Password123!");

            // Act
            user1.Id = 1;
            user2.Id = 2;

            // Assert
            Assert.AreEqual(1, user1.Id);
            Assert.AreEqual(2, user2.Id);
        }

        [TestMethod]
        public void Id_SetNegativeValue_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Id = -1;

            // Assert
            Assert.AreEqual(-1, user.Id);
        }

        [TestMethod]
        public void Id_SetZero_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Id = 0;

            // Assert
            Assert.AreEqual(0, user.Id);
        }

        #endregion

        #region Name Property Tests

        [TestMethod]
        public void Name_SetValue_StoresCorrectly()
        {
            // Arrange
            User user = new User("Initial", "User", "test@example.com", "Password123!");

            // Act
            user.Name = "Updated";

            // Assert
            Assert.AreEqual("Updated", user.Name);
        }

        [TestMethod]
        public void Name_Constructor_StoresCorrectly()
        {
            // Arrange & Act
            User user = new User("TestName", "LastName", "test@example.com", "Password123!");

            // Assert
            Assert.AreEqual("TestName", user.Name);
        }

        [TestMethod]
        public void Name_SetEmpty_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Name = "";

            // Assert
            Assert.AreEqual("", user.Name);
        }

        [TestMethod]
        public void Name_SetNull_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Name = null;

            // Assert
            Assert.IsNull(user.Name);
        }

        [TestMethod]
        public void Name_SetWithSpaces_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Name = "John Paul";

            // Assert
            Assert.AreEqual("John Paul", user.Name);
        }

        [TestMethod]
        public void Name_SetWithSpecialCharacters_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Name = "José María";

            // Assert
            Assert.AreEqual("José María", user.Name);
        }

        [TestMethod]
        public void Name_ChangeDetails_UpdatesCorrectly()
        {
            // Arrange
            User user = new User("Original", "User", "test@example.com", "Password123!");

            // Act
            user.ChangeDetails("NewName", "NewLastName", "new@example.com");

            // Assert
            Assert.AreEqual("NewName", user.Name);
        }

        #endregion

        #region LastName Property Tests

        [TestMethod]
        public void LastName_SetValue_StoresCorrectly()
        {
            // Arrange
            User user = new User("Test", "Initial", "test@example.com", "Password123!");

            // Act
            user.LastName = "Updated";

            // Assert
            Assert.AreEqual("Updated", user.LastName);
        }

        [TestMethod]
        public void LastName_Constructor_StoresCorrectly()
        {
            // Arrange & Act
            User user = new User("FirstName", "TestLastName", "test@example.com", "Password123!");

            // Assert
            Assert.AreEqual("TestLastName", user.LastName);
        }

        [TestMethod]
        public void LastName_SetEmpty_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.LastName = "";

            // Assert
            Assert.AreEqual("", user.LastName);
        }

        [TestMethod]
        public void LastName_SetNull_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.LastName = null;

            // Assert
            Assert.IsNull(user.LastName);
        }

        [TestMethod]
        public void LastName_SetWithSpaces_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.LastName = "De La Cruz";

            // Assert
            Assert.AreEqual("De La Cruz", user.LastName);
        }

        [TestMethod]
        public void LastName_ChangeDetails_UpdatesCorrectly()
        {
            // Arrange
            User user = new User("Test", "Original", "test@example.com", "Password123!");

            // Act
            user.ChangeDetails("NewName", "NewLastName", "new@example.com");

            // Assert
            Assert.AreEqual("NewLastName", user.LastName);
        }

        #endregion

        #region Password Property Tests

        [TestMethod]
        public void Password_Constructor_EncryptsPassword()
        {
            // Arrange
            string plainPassword = "PlainPassword123!";

            // Act
            User user = new User("Test", "User", "test@example.com", plainPassword);

            // Assert
            Assert.AreNotEqual(plainPassword, user.Password, "La contraseña debe estar encriptada");
            Assert.IsNotNull(user.Password);
            Assert.IsTrue(user.Password.Length > 0);
        }

        [TestMethod]
        public void Password_Setter_EncryptsPassword()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Initial123!");
            string newPassword = "NewPassword456!";

            // Act
            user.Password = newPassword;

            // Assert
            Assert.AreNotEqual(newPassword, user.Password, "La contraseña debe estar encriptada");
        }

        [TestMethod]
        public void Password_TwoIdenticalPasswords_SameHash()
        {
            // Arrange
            string password = "SamePassword123!";

            // Act
            User user1 = new User("User", "One", "user1@example.com", password);
            User user2 = new User("User", "Two", "user2@example.com", password);

            // Assert
            Assert.AreEqual(user1.Password, user2.Password, "Misma contraseña debe generar el mismo hash");
        }

        [TestMethod]
        public void Password_DifferentPasswords_DifferentHashes()
        {
            // Arrange
            User user1 = new User("User", "One", "user1@example.com", "Password123!");
            User user2 = new User("User", "Two", "user2@example.com", "Different456!");

            // Act & Assert
            Assert.AreNotEqual(user1.Password, user2.Password, "Diferentes contraseñas deben generar diferentes hashes");
        }

        [TestMethod]
        public void Password_ChangePassword_UpdatesHash()
        {
            // Arrange
            string initialPassword = "Initial123!";
            User user = new User("Test", "User", "test@example.com", initialPassword);
            string initialHash = user.Password;

            // Act
            user.ChangePassword(initialPassword, "NewPassword456!");

            // Assert
            Assert.AreNotEqual(initialHash, user.Password, "El hash debe cambiar al cambiar la contraseña");
        }

        [TestMethod]
        public void Password_EmptyString_Encrypts()
        {
            // Arrange & Act
            User user = new User("Test", "User", "test@example.com", "");

            // Assert
            Assert.IsNotNull(user.Password);
            Assert.IsTrue(user.Password.Length > 0, "Incluso una contraseña vacía debe encriptarse");
        }

        #endregion

        #region Is_Subscription Property Tests

        [TestMethod]
        public void Is_Subscription_Constructor_DefaultsFalse()
        {
            // Arrange & Act
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Assert
            Assert.IsFalse(user.Is_Subscription);
        }

        [TestMethod]
        public void Is_Subscription_SetTrue_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Is_Subscription = true;

            // Assert
            Assert.IsTrue(user.Is_Subscription);
        }

        [TestMethod]
        public void Is_Subscription_SetFalse_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.Is_Subscription = true;

            // Act
            user.Is_Subscription = false;

            // Assert
            Assert.IsFalse(user.Is_Subscription);
        }

        [TestMethod]
        public void Is_Subscription_Subscribe_SetsTrue()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Subscribe();

            // Assert
            Assert.IsTrue(user.Is_Subscription);
        }

        [TestMethod]
        public void Is_Subscription_Unsubscribe_SetsFalse()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.Subscribe();

            // Act
            user.Unsubscribe();

            // Assert
            Assert.IsFalse(user.Is_Subscription);
        }

        [TestMethod]
        public void Is_Subscription_MultipleToggles_Works()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act & Assert
            user.Subscribe();
            Assert.IsTrue(user.Is_Subscription);

            user.Unsubscribe();
            Assert.IsFalse(user.Is_Subscription);

            user.Subscribe();
            Assert.IsTrue(user.Is_Subscription);

            user.Unsubscribe();
            Assert.IsFalse(user.Is_Subscription);
        }

        #endregion

        #region Is_superuser Property Tests

        [TestMethod]
        public void Is_superuser_Constructor_DefaultsFalse()
        {
            // Arrange & Act
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Assert
            Assert.IsFalse(user.Is_superuser);
        }

        [TestMethod]
        public void Is_superuser_SetTrue_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Is_superuser = true;

            // Assert
            Assert.IsTrue(user.Is_superuser);
        }

        #endregion

        #region State Property Tests

        [TestMethod]
        public void State_Constructor_DefaultsUnactive()
        {
            // Arrange & Act
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Assert
            Assert.AreEqual(UserState.Unactive, user.State);
        }

        [TestMethod]
        public void State_Login_SetsActive()
        {
            // Arrange
            string email = "test@example.com";
            string password = "Password123!";
            User user = new User("Test", "User", email, password);

            // Act
            user.Login(email, password);

            // Assert
            Assert.AreEqual(UserState.Active, user.State);
        }

        [TestMethod]
        public void State_Logout_SetsUnactive()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.State = UserState.Active;

            // Act
            user.Logout();

            // Assert
            Assert.AreEqual(UserState.Unactive, user.State);
        }

        [TestMethod]
        public void State_Block_SetsBlocked()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.Block();

            // Assert
            Assert.AreEqual(UserState.Blocked, user.State);
        }

        [TestMethod]
        public void State_Unblock_SetsUnactive()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.Block();

            // Act
            user.Unblock();

            // Assert
            Assert.AreEqual(UserState.Unactive, user.State);
        }

        [TestMethod]
        public void State_SetDirectly_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act & Assert
            user.State = UserState.Active;
            Assert.AreEqual(UserState.Active, user.State);

            user.State = UserState.Unactive;
            Assert.AreEqual(UserState.Unactive, user.State);

            user.State = UserState.Blocked;
            Assert.AreEqual(UserState.Blocked, user.State);
        }

        [TestMethod]
        public void State_AllPossibleValues_CanBeSet()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act & Assert - Probar todos los valores del enum
            foreach (UserState state in Enum.GetValues(typeof(UserState)))
            {
                user.State = state;
                Assert.AreEqual(state, user.State);
            }
        }

        #endregion

        #region Last_login Property Tests

        [TestMethod]
        public void Last_login_Constructor_SetsCurrentTime()
        {
            // Arrange
            DateTime before = DateTime.Now.AddSeconds(-1);

            // Act
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Assert
            DateTime after = DateTime.Now.AddSeconds(1);
            Assert.IsTrue(user.Last_login >= before && user.Last_login <= after);
        }

        [TestMethod]
        public void Last_login_Login_UpdatesTime()
        {
            // Arrange
            string email = "test@example.com";
            string password = "Password123!";
            User user = new User("Test", "User", email, password);
            DateTime initialLogin = user.Last_login;
            System.Threading.Thread.Sleep(100); // Esperar un poco

            // Act
            user.Login(email, password);

            // Assert
            Assert.IsTrue(user.Last_login > initialLogin, "Last_login debe actualizarse al hacer login");
        }

        [TestMethod]
        public void Last_login_SetDirectly_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            DateTime customDate = new DateTime(2024, 1, 1, 12, 0, 0);

            // Act
            user.Last_login = customDate;

            // Assert
            Assert.AreEqual(customDate, user.Last_login);
        }

        [TestMethod]
        public void Last_login_FutureDate_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            DateTime futureDate = DateTime.Now.AddDays(30);

            // Act
            user.Last_login = futureDate;

            // Assert
            Assert.AreEqual(futureDate, user.Last_login);
        }

        [TestMethod]
        public void Last_login_PastDate_Stores()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            DateTime pastDate = new DateTime(2000, 1, 1);

            // Act
            user.Last_login = pastDate;

            // Assert
            Assert.AreEqual(pastDate, user.Last_login);
        }

        #endregion

        #region Activities Property Tests

        [TestMethod]
        public void Activities_Constructor_InitializesEmptyList()
        {
            // Arrange & Act
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Assert
            Assert.IsNotNull(user.Activities);
            Assert.AreEqual(0, user.Activities.Count);
        }

        [TestMethod]
        public void Activities_IsReadOnly_CannotReassign()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act & Assert
            // La propiedad Activities solo tiene getter, no se puede reasignar
            // Este test verifica que la lista existe y es utilizable
            Assert.IsNotNull(user.Activities);
            Assert.IsInstanceOfType(user.Activities, typeof(List<Activity>));
        }


        #endregion

        #region Block Tests - Additional Coverage

        [TestMethod]
        public void Block_FromActiveState_SetsBlocked()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.State = UserState.Active;

            // Act
            user.Block();

            // Assert
            Assert.AreEqual(UserState.Blocked, user.State);
        }

        [TestMethod]
        public void Block_FromUnactiveState_SetsBlocked()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.State = UserState.Unactive;

            // Act
            user.Block();

            // Assert
            Assert.AreEqual(UserState.Blocked, user.State);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Block_AlreadyBlocked_ThrowsException()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.Block();

            // Act
            user.Block(); // Intentar bloquear de nuevo
        }

        #endregion

        #region Unblock Tests - Additional Coverage

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Unblock_NotBlocked_ThrowsException()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.State = UserState.Unactive;

            // Act
            user.Unblock();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Unblock_ActiveState_ThrowsException()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            user.State = UserState.Active;

            // Act
            user.Unblock();
        }

        #endregion

        #region ChangeDetails Tests - Additional Coverage

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeDetails_InvalidEmailFormat_ThrowsException()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            user.ChangeDetails("New", "Name", "invalid-email-format");
        }

        [TestMethod]
        public void ChangeDetails_ValidData_UpdatesAllProperties()
        {
            // Arrange
            User user = new User("Original", "User", "original@example.com", "Password123!");
            string newName = "Updated";
            string newLastName = "NewLastName";
            string newEmail = "updated@example.com";

            // Act
            user.ChangeDetails(newName, newLastName, newEmail);

            // Assert
            Assert.AreEqual(newName, user.Name);
            Assert.AreEqual(newLastName, user.LastName);
            Assert.AreEqual(newEmail, user.Email);
        }

        #endregion

        #region Integration and Edge Case Tests

        [TestMethod]
        public void User_CompleteLifecycle_AllOperationsWork()
        {
            // Arrange
            string email = "lifecycle@example.com";
            string password = "Password123!";
            User user = new User("Test", "User", email, password);

            // Act & Assert - Ciclo completo de vida del usuario

            // 1. Login
            user.Login(email, password);
            Assert.AreEqual(UserState.Active, user.State);

            // 2. Subscribe
            user.Subscribe();
            Assert.IsTrue(user.Is_Subscription);

            // 3. Change password
            user.ChangePassword(password, "NewPassword456!");

            // 4. Change details
            user.ChangeDetails("NewName", "NewLastName", "newemail@example.com");
            Assert.AreEqual("NewName", user.Name);

            // 5. Unsubscribe
            user.Unsubscribe();
            Assert.IsFalse(user.Is_Subscription);

            // 6. Logout
            user.Logout();
            Assert.AreEqual(UserState.Unactive, user.State);

            // 7. Block
            user.Block();
            Assert.AreEqual(UserState.Blocked, user.State);

            // 8. Unblock
            user.Unblock();
            Assert.AreEqual(UserState.Unactive, user.State);
        }

        [TestMethod]
        public void User_TwoInstancesWithSameData_AreEqual()
        {
            // Arrange
            User user1 = new User("John", "Doe", "john@example.com", "Password123!") { Id = 5 };
            User user2 = new User("John", "Doe", "john@example.com", "Password123!") { Id = 5 };

            // Act
            bool areEqual = user1.Equals(user2);

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void User_DifferentInstances_AreNotEqual()
        {
            // Arrange
            User user1 = new User("John", "Doe", "john@example.com", "Password123!") { Id = 1 };
            User user2 = new User("Jane", "Doe", "jane@example.com", "Password123!") { Id = 2 };

            // Act
            bool areEqual = user1.Equals(user2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void User_CompareWithNull_ReturnsFalse()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            bool areEqual = user.Equals(null);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void User_CompareWithNonUserObject_ReturnsFalse()
        {
            // Arrange
            User user = new User("Test", "User", "test@example.com", "Password123!");
            string notAUser = "Not a user";

            // Act
            bool areEqual = user.Equals(notAUser);

            // Assert
            Assert.IsFalse(areEqual);
        }

        #endregion


    }
}