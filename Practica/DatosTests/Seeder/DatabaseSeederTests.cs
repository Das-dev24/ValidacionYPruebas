// Importaciones necesarias para el framework de pruebas, el seeder y los modelos.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datos.Seeder;
using Practica.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using Database; // Necesario para la interfaz ICapaDatos

namespace Datos.Tests {
    // Atributo que identifica esta clase como una clase de pruebas.
    [TestClass]
    public class DatabaseSeederTests {
        // Campos privados para la implementación falsa de la capa de datos y el seeder.
        private FakeCapaDatos _fakeCapaDatos;
        private DatabaseSeeder _seeder;

        // Este método se ejecuta antes de cada prueba para preparar el entorno.
        [TestInitialize]
        public void Setup() {
            // Se crea una nueva instancia de la capa de datos falsa para asegurar que cada test es independiente.
            _fakeCapaDatos = new FakeCapaDatos();
            // Se crea una instancia del seeder, pasándole la capa de datos falsa.
            _seeder = new DatabaseSeeder(_fakeCapaDatos);
        }

        #region Pruebas del método Seed

        // Atributo que identifica esto como un método de prueba.
        [TestMethod]
        public void Seed_CuandoLaBaseDeDatosEstaVacia_DebePoblarlaConUsuariosYActividades() {
            // --- 1. Preparación (Arrange) ---
            // Se asegura de que la base de datos simulada está vacía antes de empezar.
            Assert.AreEqual(0, _fakeCapaDatos.NumUsers(), "Precondición: No debe haber usuarios.");

            // --- 2. Ejecución (Act) ---
            // Se llama al método que se quiere probar.
            _seeder.Seed();

            // --- 3. Verificación (Assert) ---
            // Se comprueba que el resultado es el esperado.
            Assert.AreEqual(3, _fakeCapaDatos.NumUsers(), "Debe añadir 3 usuarios iniciales.");
            Assert.IsNotNull(_fakeCapaDatos.LeeUser("admin@example.com"), "El usuario admin debería existir.");

            // Verifica que se han añadido las actividades esperadas.
            var allActivities = _fakeCapaDatos.GetAllActivities();
            Assert.HasCount(8, allActivities, "Debe añadir 8 actividades iniciales.");

            // Verificación más específica: el usuario Juan debería tener 4 actividades.
            var juan = _fakeCapaDatos.LeeUser("juan.perez@example.com");
            var juanActivities = allActivities.Where(a => a.User.Id == juan.Id).ToList();
            Assert.HasCount(4, juanActivities, "El usuario Juan debería tener 4 actividades.");
        }

        [TestMethod]
        public void Seed_CuandoLaBaseDeDatosYaTieneUsuarios_NoDebeHacerNada() {
            // --- 1. Preparación (Arrange) ---
            // Se añade un usuario para simular que la base de datos no está vacía.
            _fakeCapaDatos.GuardaUser(new User("Existing", "User", "existing@user.com", "Password123!"));
            Assert.AreEqual(1, _fakeCapaDatos.NumUsers(), "Precondición: Debe haber 1 usuario.");

            // --- 2. Ejecución (Act) ---
            // Se invoca el método a probar.
            _seeder.Seed();

            // --- 3. Verificación (Assert) ---
            // Se comprueba que el número de usuarios no ha cambiado y no se añadieron actividades.
            Assert.AreEqual(1, _fakeCapaDatos.NumUsers(), "El número de usuarios no debería cambiar si la BD no está vacía.");
            Assert.IsEmpty(_fakeCapaDatos.GetAllActivities(), "No se debería haber añadido ninguna actividad.");
        }

        [TestMethod]
        public void Seed_CuandoLosUsuariosNoSeEncuentran_NoDebeAñadirActividades() {
            // --- 1. Preparación (Arrange) ---
            // Se configura la capa de datos falsa para que simule no encontrar usuarios al leerlos.
            _fakeCapaDatos.ShouldReturnUsersOnRead = false;

            // --- 2. Ejecución (Act) ---
            // Se llama al método que se quiere probar.
            _seeder.Seed();

            // --- 3. Verificación (Assert) ---
            // Se comprueba que los usuarios se guardaron, pero como no se pudieron leer después, no se crearon actividades.
            Assert.AreEqual(3, _fakeCapaDatos.NumUsers(), "Los usuarios se guardan igualmente.");
            Assert.IsEmpty(_fakeCapaDatos.GetAllActivities(), "No se debería haber añadido ninguna actividad.");
        }

        #endregion
    }

    /// <summary>
    /// Implementación "falsa" de la interfaz ICapaDatos para usar en las pruebas.
    /// Simula el comportamiento de la base de datos en memoria, sin conectarse a una real.
    /// </summary>
    public class FakeCapaDatos : ICapaDatos {
        // Listas en memoria que simulan ser las tablas de la base de datos.
        private readonly List<User> _users = new List<User>();
        private readonly List<Activity> _activities = new List<Activity>();
        // Propiedad para controlar si el método LeeUser debe devolver usuarios o null (para simular errores).
        public bool ShouldReturnUsersOnRead { get; set; } = true;

        // Devuelve el número de usuarios en la lista.
        public int NumUsers() => _users.Count;

        // Guarda un usuario en la lista si no existe ya.
        public bool GuardaUser(User u) {
            if (_users.Contains(u)) return false;
            _users.Add(u);
            return true;
        }

        // Lee un usuario por su email. Su comportamiento se puede controlar con 'ShouldReturnUsersOnRead'.
        public User LeeUser(string email) {
            if (!ShouldReturnUsersOnRead) return null;
            return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // Guarda una actividad en la lista.
        public bool GuardaActivity(Activity e) {
            if (_activities.Contains(e)) return false;
            _activities.Add(e);
            return true;
        }

        // Devuelve todas las actividades de la lista.
        public List<Activity> GetAllActivities() => _activities;

        // Busca una actividad por su ID.
        public Activity GetActivityById(int activityId) {
            return _activities.FirstOrDefault(a => a.Id == activityId);
        }

        // --- Métodos de la interfaz no necesarios para estas pruebas ---
        // Se lanzan excepciones para indicar que no han sido implementados porque no se usan en estos tests.
        public bool ValidaUser(string email, string password) => throw new NotImplementedException();
        public void Register(string name, string lastName, string email, string password, bool isSuperUser = false) => throw new NotImplementedException();
    }
}