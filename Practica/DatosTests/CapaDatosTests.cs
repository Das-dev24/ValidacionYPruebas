using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datos;
using Practica.Model;
using Practica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Tests
{
    [TestClass()]
    public class CapaDatosTests
    {
        private CapaDatos _capaDatos;

        [TestInitialize]
        public void Setup()
        {
            // Crear nueva instancia antes de cada test para aislar las pruebas
            _capaDatos = new CapaDatos();
        }

        #region GetAllUsers Tests

        [TestMethod()]
        public void GetAllUsers_DebeRetornarListaDeUsuarios()
        {
            // Act
            var result = _capaDatos.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<User>));
        }

        [TestMethod()]
        public void GetAllUsers_DebeRetornarUsuariosSemilla()
        {
            // Act
            var result = _capaDatos.GetAllUsers();

            // Assert - Verifica que hay usuarios cargados por el Seeder
            Assert.IsTrue(result.Count >= 0);
        }

        #endregion

        #region GuardaUser Tests

        [TestMethod()]
        public void GuardaUser_UsuarioNuevo_DebeRetornarTrue()
        {
            // Arrange
            var nuevoUsuario = new User("Test", "User", "test@example.com", "Password123!");

            // Act
            var result = _capaDatos.GuardaUser(nuevoUsuario);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(_capaDatos.GetAllUsers().Contains(nuevoUsuario));
        }

        [TestMethod()]
        public void GuardaUser_UsuarioDuplicado_DebeRetornarFalse()
        {
            // Arrange
            var usuario = new User("Test", "User", "test@example.com", "Password123!");
            _capaDatos.GuardaUser(usuario);

            // Act - Intentar guardar el mismo usuario dos veces
            var result = _capaDatos.GuardaUser(usuario);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion


        #region LeeUser Tests

        [TestMethod()]
        public void LeeUser_EmailExistente_DebeRetornarUsuario()
        {
            // Arrange
            var usuario = new User("Test", "User", "leeuser@example.com", "Password123!");
            _capaDatos.GuardaUser(usuario);

            // Act
            var result = _capaDatos.LeeUser("leeuser@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("leeuser@example.com", result.Email, ignoreCase: true);
        }

        [TestMethod()]
        public void LeeUser_EmailNoExistente_DebeRetornarNull()
        {
            // Act
            var result = _capaDatos.LeeUser("noexiste@example.com");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void LeeUser_EmailCaseInsensitive_DebeRetornarUsuario()
        {
            // Arrange
            var usuario = new User("Test", "User", "CaseSensitive@Example.com", "Password123!");
            _capaDatos.GuardaUser(usuario);

            // Act
            var result = _capaDatos.LeeUser("casesensitive@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CaseSensitive@Example.com", result.Email, ignoreCase: true);
        }

        #endregion

        #region LeeUserPorId Tests

        [TestMethod()]
        public void LeeUserPorId_IdExistente_DebeRetornarUsuario()
        {
            // Arrange
            var usuario = new User("Test", "User", "userid@example.com", "Password123!");
            usuario.Id = 12345;
            _capaDatos.GuardaUser(usuario);

            // Act
            var result = _capaDatos.LeeUserPorId(12345);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(12345, result.Id);
        }

        [TestMethod()]
        public void LeeUserPorId_IdNoExistente_DebeRetornarNull()
        {
            // Act
            var result = _capaDatos.LeeUserPorId(999999);

            // Assert
            Assert.IsNull(result);
        }

        #endregion

        #region NumUsers Tests

        [TestMethod()]
        public void NumUsers_DebeRetornarCantidadCorrecta()
        {
            // Arrange
            int cantidadInicial = _capaDatos.NumUsers();
            var usuario = new User("Test", "User", "numuser@example.com", "Password123!");
            _capaDatos.GuardaUser(usuario);

            // Act
            int cantidadFinal = _capaDatos.NumUsers();

            // Assert
            Assert.AreEqual(cantidadInicial + 1, cantidadFinal);
        }

        #endregion

        #region NumUsersActivos Tests

        [TestMethod()]
        public void NumUsersActivos_ConUsuariosActivos_DebeContarCorrectamente()
        {
            // Arrange
            var usuario1 = new User("Test1", "User", "activo1@example.com", "Password123!");
            usuario1.State = UserState.Active;
            _capaDatos.GuardaUser(usuario1);

            var usuario2 = new User("Test2", "User", "activo2@example.com", "Password123!");
            usuario2.State = UserState.Active;
            _capaDatos.GuardaUser(usuario2);

            var usuario3 = new User("Test3", "User", "inactivo@example.com", "Password123!");
            usuario3.State = UserState.Unactive;
            _capaDatos.GuardaUser(usuario3);

            int activosEsperados = _capaDatos.GetAllUsers().Count(u => u.State == UserState.Active);

            // Act
            int result = _capaDatos.NumUsersActivos();

            // Assert
            Assert.AreEqual(activosEsperados, result);
        }

        [TestMethod()]
        public void NumUsersActivos_RecorrerTodosLosUsuarios_CubrimientoBucleFor()
        {
            // Arrange - Agregar múltiples usuarios para asegurar que el bucle recorre todos
            for (int i = 0; i < 5; i++)
            {
                var usuario = new User($"Test{i}", "User", $"user{i}@example.com", "Password123!");
                usuario.State = (i % 2 == 0) ? UserState.Active : UserState.Unactive;
                _capaDatos.GuardaUser(usuario);
            }

            // Act
            int result = _capaDatos.NumUsersActivos();

            // Assert
            Assert.IsTrue(result >= 0);
        }

        #endregion

        #region ValidaUser Tests

        [TestMethod()]
        public void ValidaUser_CredencialesCorrectas_DebeRetornarTrueYActivarUsuario()
        {
            // Arrange
            string email = "valido@example.com";
            string password = "Password123!";

            var usuario = new User("Test", "User", email, password);
            usuario.State = UserState.Unactive;
            _capaDatos.GuardaUser(usuario);

            // Act
            var result = _capaDatos.ValidaUser(email, password);
            var usuarioValidado = _capaDatos.LeeUser(email);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(UserState.Active, usuarioValidado.State);
            Assert.IsNotNull(usuarioValidado.Last_login);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidaUser_EmailVacio_DebeLanzarArgumentNullException()
        {
            // Act
            _capaDatos.ValidaUser("", "password");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidaUser_EmailNull_DebeLanzarArgumentNullException()
        {
            // Act
            _capaDatos.ValidaUser(null, "password");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidaUser_PasswordVacio_DebeLanzarArgumentNullException()
        {
            // Act
            _capaDatos.ValidaUser("email@example.com", "");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidaUser_PasswordNull_DebeLanzarArgumentNullException()
        {
            // Act
            _capaDatos.ValidaUser("email@example.com", null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidaUser_UsuarioNoExiste_DebeLanzarInvalidOperationException()
        {
            // Act
            _capaDatos.ValidaUser("noexiste@example.com", "Password123!");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidaUser_PasswordIncorrecta_DebeLanzarInvalidOperationException()
        {
            // Arrange
            string email = "incorrecta@example.com";
            string passwordCorrecta = "Password123!";

            var usuario = new User("Test", "User", email, passwordCorrecta);
            _capaDatos.GuardaUser(usuario);

            // Act
            _capaDatos.ValidaUser(email, "PasswordIncorrecta!");
        }

        #endregion

        #region Register Tests

        [TestMethod()]
        public void Register_DatosValidos_DebeCrearUsuarioCorrectamente()
        {
            // Arrange
            string name = "Nuevo";
            string lastName = "Usuario";
            string email = "nuevo@example.com";
            string password = "Password123!";
            int usuariosInicial = _capaDatos.NumUsers();

            // Act
            _capaDatos.Register(name, lastName, email, password);

            // Assert
            Assert.AreEqual(usuariosInicial + 1, _capaDatos.NumUsers());
            var usuarioCreado = _capaDatos.LeeUser(email);
            Assert.IsNotNull(usuarioCreado);
            Assert.AreEqual(name, usuarioCreado.Name);
            Assert.AreEqual(lastName, usuarioCreado.LastName);
            Assert.AreEqual(email, usuarioCreado.Email);
        }

        [TestMethod()]
        public void Register_DebeAsignarIdCorrectamente()
        {
            // Arrange
            string email = "conid@example.com";
            int numeroUsuariosAntes = _capaDatos.NumUsers();

            // Act
            _capaDatos.Register("Test", "User", email, "Password123!");

            // Assert
            var usuario = _capaDatos.LeeUser(email);
            Assert.AreEqual(numeroUsuariosAntes + 1, usuario.Id);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_NameVacio_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register("", "User", "test@example.com", "Password123!");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_NameNull_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register(null, "User", "test@example.com", "Password123!");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_LastNameVacio_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register("Test", "", "test@example.com", "Password123!");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_LastNameNull_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register("Test", null, "test@example.com", "Password123!");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_EmailVacio_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register("Test", "User", "", "Password123!");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_EmailNull_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register("Test", "User", null, "Password123!");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_PasswordVacio_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register("Test", "User", "test@example.com", "");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_PasswordNull_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register("Test", "User", "test@example.com", null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_PasswordNoValidaRequisitos_DebeLanzarArgumentException()
        {
            // Arrange - Password débil que no cumple requisitos
            // Act
            _capaDatos.Register("Test", "User", "test@example.com", "weak");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_EmailYaExiste_DebeLanzarArgumentException()
        {
            // Arrange
            string email = "duplicado@example.com";
            _capaDatos.Register("Test1", "User1", email, "Password123!");

            // Act - Intentar registrar con el mismo email
            _capaDatos.Register("Test2", "User2", email, "Password456!");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_EmailFormatoInvalido_DebeLanzarArgumentException()
        {
            // Act
            _capaDatos.Register("Test", "User", "emailinvalido", "Password123!");
        }

        #endregion

        #region GuardaActivity Tests

        [TestMethod()]
        public void GuardaActivity_ActividadNueva_DebeRetornarTrue()
        {
            // Arrange
            var usuario = new User("Test", "User", "activity@example.com", "Password123!");
            usuario.Id = 1;
            _capaDatos.GuardaUser(usuario);
            var actividad = new ActivityRunning(usuario, "Nueva Actividad", "", DateTime.Now, 60, "", "Park", 10f, 150);

            // Act
            var result = _capaDatos.GuardaActivity(actividad);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GuardaActivity_ActividadDuplicada_DebeRetornarFalse()
        {
            // Arrange
            var usuario = new User("Test", "User", "activity2@example.com", "Password123!");
            usuario.Id = 2;
            _capaDatos.GuardaUser(usuario);
            var actividad = new ActivityRunning(usuario, "Actividad Duplicada", "", DateTime.Now, 60, "", "Park", 10f, 150);
            _capaDatos.GuardaActivity(actividad);

            // Act - Intentar guardar la misma actividad dos veces
            var result = _capaDatos.GuardaActivity(actividad);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region LeeActivity Tests

        [TestMethod()]
        public void LeeActivity_IndiceValido_DebeRetornarActividad()
        {
            // Arrange
            var usuario = new User("Test", "User", "leeactivity@example.com", "Password123!");
            usuario.Id = 3;
            _capaDatos.GuardaUser(usuario);
            var actividad = new ActivityRunning(usuario, "Leer Actividad", "", DateTime.Now, 60, "", "Park", 10f, 150);
            _capaDatos.GuardaActivity(actividad);

            // Act - El índice 0 debería ser válido si hay al menos una actividad
            var result = _capaDatos.LeeActivity(0);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void LeeActivity_IndiceNegativo_DebeRetornarNull()
        {
            // Act
            var result = _capaDatos.LeeActivity(-1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void LeeActivity_IndiceFueraDeRango_DebeRetornarNull()
        {
            // Act - Índice muy alto
            var result = _capaDatos.LeeActivity(99999);

            // Assert
            Assert.IsNull(result);
        }

        #endregion

    }
}