using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datos.Seeder;
using Practica.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using Database; // Necesario para la interfaz ICapaDatos

namespace Datos.Tests {
    [TestClass]
    public class DatabaseSeederTests {
        private FakeCapaDatos _fakeCapaDatos;
        private DatabaseSeeder _seeder;

        [TestInitialize]
        public void Setup() {
            // Antes de cada test, creamos una implementación falsa y limpia de la capa de datos
            _fakeCapaDatos = new FakeCapaDatos();
            _seeder = new DatabaseSeeder(_fakeCapaDatos);
        }

        #region Seed Tests

        [TestMethod]
        public void Seed_CuandoLaBaseDeDatosEstaVacia_DebePoblarlaConUsuariosYActividades() {
            // Arrange
            Assert.AreEqual(0, _fakeCapaDatos.NumUsers(), "Precondición: No debe haber usuarios.");

            // Act
            _seeder.Seed();

            // Assert
            Assert.AreEqual(3, _fakeCapaDatos.NumUsers(), "Debe añadir 3 usuarios iniciales.");
            Assert.IsNotNull(_fakeCapaDatos.LeeUser("admin@example.com"), "El usuario admin debería existir.");

            // Verifica que se han añadido las actividades esperadas
            var allActivities = _fakeCapaDatos.GetAllActivities();
            Assert.HasCount(8, allActivities, "Debe añadir 8 actividades iniciales.");

            // Verificación más específica: Juan debería tener 4 actividades
            var juan = _fakeCapaDatos.LeeUser("juan.perez@example.com");
            var juanActivities = allActivities.Where(a => a.User.Id == juan.Id).ToList();
            Assert.HasCount(4, juanActivities, "El usuario Juan debería tener 4 actividades.");
        }

        [TestMethod]
        public void Seed_CuandoLaBaseDeDatosYaTieneUsuarios_NoDebeHacerNada() {
            // Arrange
            _fakeCapaDatos.GuardaUser(new User("Existing", "User", "existing@user.com", "Password123!"));
            Assert.AreEqual(1, _fakeCapaDatos.NumUsers(), "Precondición: Debe haber 1 usuario.");

            // Act
            _seeder.Seed(); // Se invoca el método a probar

            // Assert
            Assert.AreEqual(1, _fakeCapaDatos.NumUsers(), "El número de usuarios no debería cambiar si la BD no está vacía.");
            Assert.IsEmpty(_fakeCapaDatos.GetAllActivities(), "No se debería haber añadido ninguna actividad.");
        }

        [TestMethod]
        public void Seed_CuandoLosUsuariosNoSeEncuentran_NoDebeAñadirActividades() {
            // Arrange
            _fakeCapaDatos.ShouldReturnUsersOnRead = false;

            // Act
            _seeder.Seed();

            // Assert
            Assert.AreEqual(3, _fakeCapaDatos.NumUsers(), "Los usuarios se guardan igualmente.");
            Assert.IsEmpty(_fakeCapaDatos.GetAllActivities(), "No se debería haber añadido ninguna actividad.");
        }

        #endregion
    }

    public class FakeCapaDatos : ICapaDatos {
        private readonly List<User> _users = new List<User>();
        private readonly List<Activity> _activities = new List<Activity>();
        public bool ShouldReturnUsersOnRead { get; set; } = true;

        public int NumUsers() => _users.Count;
        public List<User> GetAllUsers() => _users;
        public bool GuardaUser(User u) {
            if (_users.Contains(u)) return false;
            _users.Add(u);
            return true;
        }

        public User LeeUser(string email) {
            if (!ShouldReturnUsersOnRead) return null;
            return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool GuardaActivity(Activity e) {
            if (_activities.Contains(e)) return false;
            _activities.Add(e);
            return true;
        }

        public List<Activity> GetAllActivities() => _activities;

        public Activity GetActivityById(int activityId) {
            return _activities.FirstOrDefault(a => a.Id == activityId);
        }

        public bool DeleteUser(User userToDelete) => throw new NotImplementedException();
        public User LeeUserPorId(int userId) => throw new NotImplementedException();
        public bool ValidaUser(string email, string password) => throw new NotImplementedException();
        public void Register(string name, string lastName, string email, string password, bool isSuperUser = false) => throw new NotImplementedException();
        public List<Activity> GetActivitiesForUser(int userId) => throw new NotImplementedException();
    }
}