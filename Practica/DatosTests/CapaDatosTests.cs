using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datos;
using Practica.Model;
using Practica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Tests {
    [TestClass()]
    public class CapaDatosTests {
        private CapaDatos _capaDatos;

        [TestInitialize]
        public void Setup() {

            _capaDatos = new CapaDatos();
        }

        #region GetAllUsers Tests

        [TestMethod()]
        public void GetAllUsers_AlIniciar_NoDebeSerNulo() {
            // Act
            var result = _capaDatos.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<User>));
        }

        #endregion

        #region GuardaUser / LeeUser / LeeUserPorId Tests

        [TestMethod()]
        public void GuardaUser_ConUsuarioNuevo_DebeAgregarloALaLista() {
            // Arrange
            var nuevoUsuario = new User("Test", "User", "test.nuevo@example.com", "Password123!");
            nuevoUsuario.Id = 99;

            // Act
            bool resultadoGuardado = _capaDatos.GuardaUser(nuevoUsuario);

            // Assert
            Assert.IsTrue(resultadoGuardado, "GuardaUser debería devolver true para un usuario nuevo.");

            var usuarioRecuperado = _capaDatos.LeeUser("test.nuevo@example.com");
            Assert.IsNotNull(usuarioRecuperado, "El usuario debería poder recuperarse por email.");
            Assert.AreEqual("Test", usuarioRecuperado.Name, "El nombre no coincide.");

            var usuarioPorId = _capaDatos.LeeUserPorId(99);
            Assert.IsNotNull(usuarioPorId, "El usuario debería poder recuperarse por ID.");
            Assert.AreEqual("test.nuevo@example.com", usuarioPorId.Email, "El email no coincide.");
        }

        [TestMethod()]
        public void GuardaUser_ConUsuarioDuplicado_DebeRetornarFalse() {
            // Arrange
            var usuario = new User("Duplicado", "Test", "duplicado@example.com", "Password123!");
            _capaDatos.GuardaUser(usuario);

            // Act - Intentamos guardar el mismo usuario de nuevo
            var result = _capaDatos.GuardaUser(usuario);

            // Assert
            Assert.IsFalse(result, "No se debería poder guardar un usuario duplicado.");
        }

        [TestMethod]
        public void LeeUser_ConEmailNoExistente_DebeRetornarNull() {
            // Act
            var result = _capaDatos.LeeUser("no.existe@example.com");
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LeeUserPorId_ConIdNoExistente_DebeRetornarNull() {
            // Act
            var result = _capaDatos.LeeUserPorId(99999);
            // Assert
            Assert.IsNull(result);
        }

        #endregion

        #region DeleteUser Tests

        [TestMethod]
        public void DeleteUser_ConUsuarioExistente_DebeEliminarloYRetornarTrue() {
            // Arrange
            var usuario = new User("Borrar", "Usuario", "borrar@example.com", "Password123!");
            _capaDatos.GuardaUser(usuario);
            Assert.IsNotNull(_capaDatos.LeeUser("borrar@example.com"), "Precondición: El usuario debe existir.");

            // Act
            bool resultado = _capaDatos.DeleteUser(usuario);

            // Assert
            Assert.IsTrue(resultado, "DeleteUser debería retornar true.");
            Assert.IsNull(_capaDatos.LeeUser("borrar@example.com"), "El usuario ya no debería existir.");
        }

        [TestMethod]
        public void DeleteUser_ConUsuarioExistenteYActividades_DebeEliminarAmbos() {
            // Arrange
            var usuario = new User("Borrar", "ConActividades", "borrar.act@example.com", "Password123!");
            _capaDatos.GuardaUser(usuario);
            var actividad = new ActivityRunning(usuario, "Carrera", "", DateTime.Now, 30, "", "Parque", 5f, 100);
            _capaDatos.GuardaActivity(actividad);

            Assert.HasCount(1, _capaDatos.GetActivitiesForUser(usuario.Id), "Precondición: El usuario debe tener una actividad.");

            // Act
            _capaDatos.DeleteUser(usuario);

            // Assert
            Assert.IsNull(_capaDatos.LeeUser("borrar.act@example.com"), "El usuario debería haber sido eliminado.");
            Assert.HasCount(0, _capaDatos.GetActivitiesForUser(usuario.Id), "Las actividades del usuario también deberían haber sido eliminadas.");
        }

        [TestMethod]
        public void DeleteUser_ConUsuarioNulo_DebeRetornarFalse() {
            // Act
            bool resultado = _capaDatos.DeleteUser(null);
            // Assert
            Assert.IsFalse(resultado);
        }

        #endregion

        #region ValidaUser Tests

        [TestMethod()]
        public void ValidaUser_ConCredencialesCorrectas_DebeRetornarTrueYActivarUsuario() {
            // Arrange
            var usuario = new User("Validar", "User", "validar@example.com", "PasswordValida123!");
            usuario.State = UserState.Unactive;
            _capaDatos.GuardaUser(usuario);

            // Act
            var result = _capaDatos.ValidaUser("validar@example.com", "PasswordValida123!");
            var usuarioValidado = _capaDatos.LeeUser("validar@example.com");

            // Assert
            Assert.IsTrue(result, "La validación debería ser exitosa.");
            Assert.AreEqual(UserState.Active, usuarioValidado.State, "El estado del usuario debería cambiar a Active.");
            Assert.IsLessThan(5, (DateTime.Now - usuarioValidado.Last_login).TotalSeconds, "La fecha de último login debería ser muy reciente.");
        }

        [TestMethod]
        [DataRow(null, "pass", typeof(ArgumentNullException))]
        [DataRow("email", null, typeof(ArgumentNullException))]
        [DataRow("no.existe@email.com", "pass", typeof(InvalidOperationException))]
        [DataRow("validar@example.com", "passIncorrecta", typeof(InvalidOperationException))]
        public void ValidaUser_ConDatosInvalidos_DebeLanzarExcepcionCorrecta(string email, string password, Type expectedExceptionType) {
            // Arrange: Se crea un usuario para el caso de contraseña incorrecta
            var usuario = new User("Validar", "User", "validar@example.com", "PasswordValida123!");
            _capaDatos.GuardaUser(usuario);

            // Act & Assert
            try {
                _capaDatos.ValidaUser(email, password);
                Assert.Fail("Se esperaba una excepción, pero no se lanzó ninguna.");
            } catch (Exception ex) {
                Assert.IsInstanceOfType(ex, expectedExceptionType, "Se lanzó un tipo de excepción incorrecto.");
            }
        }
        #endregion

        #region Register Tests

        [TestMethod()]
        public void Register_ConDatosValidos_DebeCrearUsuarioCorrectamente() {
            // Arrange
            int usuariosInicial = _capaDatos.NumUsers();

            // Act
            _capaDatos.Register("Nuevo", "Usuario", "nuevo.reg@example.com", "PasswordValido123!");

            // Assert
            Assert.AreEqual(usuariosInicial + 1, _capaDatos.NumUsers());
            var usuarioCreado = _capaDatos.LeeUser("nuevo.reg@example.com");
            Assert.IsNotNull(usuarioCreado);
            Assert.AreEqual("Nuevo", usuarioCreado.Name);
            Assert.AreEqual(UserState.Unactive, usuarioCreado.State, "El usuario debe crearse como inactivo.");
        }

        [TestMethod]
        [DataRow(null, "User", "email@test.com", "Pass123!", "Todos los campos son obligatorios.")]
        [DataRow("Test", null, "email@test.com", "Pass123!", "Todos los campos son obligatorios.")]
        [DataRow("Test", "User", null, "Pass123!", "Todos los campos son obligatorios.")]
        [DataRow("Test", "User", "email@test.com", null, "Todos los campos son obligatorios.")]
        [DataRow("Test", "User", "existente@test.com", "Pass123!", "El correo electrónico ya está en uso.")]
        [DataRow("Test", "User", "email-invalido", "Pass123!", "El formato del correo electrónico no es válido.")]
        [DataRow("Test", "User", "email@test.com", "debil", "La contraseña no cumple con los requisitos de seguridad.")]
        public void Register_ConDatosInvalidos_DebeLanzarArgumentException(string name, string lastName, string email, string password, string expectedMessage) {
            // Arrange
            _capaDatos.Register("Existente", "Usuario", "existente@test.com", "PasswordValido123!");

            // Act & Assert
            var ex = AssertThrows<ArgumentException>(() => _capaDatos.Register(name, lastName, email, password));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        #endregion

        #region GetActivitiesForUser Tests

        [TestMethod]
        public void GetActivitiesForUser_ConUsuarioYActividades_DebeRetornarSusActividades() {
            // Arrange
            var user1 = new User("User", "One", "user1@example.com", "Password123!");
            user1.Id = 1;
            _capaDatos.GuardaUser(user1);

            var user2 = new User("User", "Two", "user2@example.com", "Password123!");
            user2.Id = 2;
            _capaDatos.GuardaUser(user2);

            // Actividades para user1
            _capaDatos.GuardaActivity(new ActivityRunning(user1, "Carrera Mañana", "", DateTime.Now, 30, "", "Parque", 5f, 100));
            _capaDatos.GuardaActivity(new ActivityGym(user1, "Pesas", "", DateTime.Now, 60, "", 300, "Full Body"));
            // Actividad para user2
            _capaDatos.GuardaActivity(new ActivitySwimming(user2, "Natación", "", DateTime.Now, 45, "", "Piscina", 1500));

            // Act
            var activitiesUser1 = _capaDatos.GetActivitiesForUser(1);

            // Assert
            Assert.IsNotNull(activitiesUser1);
            Assert.HasCount(2, activitiesUser1, "Debería retornar solo las actividades del usuario 1.");
            Assert.IsTrue(activitiesUser1.All(a => a.User.Id == 1));
        }

        [TestMethod]
        public void GetActivitiesForUser_ConUsuarioSinActividades_DebeRetornarListaVacia() {
            // Arrange
            var user = new User("User", "SinActividades", "sinact@example.com", "Password123!");
            user.Id = 10;
            _capaDatos.GuardaUser(user);

            // Act
            var activities = _capaDatos.GetActivitiesForUser(10);

            // Assert
            Assert.IsNotNull(activities);
            Assert.IsEmpty(activities);
        }

        [TestMethod]
        public void GetActivitiesForUser_ConIdUsuarioNoExistente_DebeRetornarListaVacia() {
            // Act
            var activities = _capaDatos.GetActivitiesForUser(999);
            // Assert
            Assert.IsNotNull(activities);
            Assert.IsEmpty(activities);
        }

        #endregion

        #region GuardaActivity Tests

        [TestMethod()]
        public void GuardaActivity_ConActividadNueva_DebeAgregarlaYRetornarTrue() {
            // Arrange
            var usuario = new User("Test", "User", "actividad@test.com", "Password123!");
            _capaDatos.GuardaUser(usuario);
            var actividad = new ActivityRunning(usuario, "Carrera de prueba", "", DateTime.Now, 30, "", "Parque", 5f, 150);
            int actividadesAntes = _capaDatos.GetActivitiesForUser(usuario.Id).Count;

            // Act
            bool resultado = _capaDatos.GuardaActivity(actividad);
            int actividadesDespues = _capaDatos.GetActivitiesForUser(usuario.Id).Count;

            // Assert
            Assert.IsTrue(resultado, "Debería retornar true al guardar una nueva actividad.");
            Assert.AreEqual(actividadesAntes + 1, actividadesDespues, "El número de actividades debería haberse incrementado en uno.");
        }

        [TestMethod()]
        public void GuardaActivity_ConActividadDuplicada_DebeRetornarFalse() {
            // Arrange
            var usuario = new User("Test", "User", "actividad.duplicada@test.com", "Password123!");
            _capaDatos.GuardaUser(usuario);
            var actividad = new ActivityRunning(usuario, "Carrera duplicada", "", DateTime.Now, 30, "", "Parque", 5f, 150);

            // Guardamos la actividad por primera vez (debería tener éxito)
            _capaDatos.GuardaActivity(actividad);
            int actividadesAntes = _capaDatos.GetActivitiesForUser(usuario.Id).Count;

            // Act: Intentamos guardar la misma instancia de la actividad por segunda vez
            bool resultado = _capaDatos.GuardaActivity(actividad);
            int actividadesDespues = _capaDatos.GetActivitiesForUser(usuario.Id).Count;

            // Assert
            Assert.IsFalse(resultado, "Debería retornar false al intentar guardar una actividad duplicada.");
            Assert.AreEqual(actividadesAntes, actividadesDespues, "El número de actividades no debería haber cambiado.");
        }

        #endregion

        private TException AssertThrows<TException>(Action action) where TException : Exception {
            try {
                action(); // Intenta ejecutar el código que esperamos que falle
            } catch (TException ex) {
                // Captura la excepción esperada y la devuelve. El test es exitoso en este punto.
                return ex;
            } catch (Exception ex) {
                // Captura cualquier otra excepción (tipo incorrecto)
                Assert.Fail($"Se esperaba la excepción {typeof(TException).Name}, pero se lanzó una excepción de tipo {ex.GetType().Name}.");
            }

            // Si llegamos aquí, no se lanzó ninguna excepción.
            Assert.Fail($"Se esperaba la excepción {typeof(TException).Name}, pero no se lanzó ninguna.");

            // Retorno dummy para cumplir con la firma del método
            return null;
        }
    }
}

