using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Model; // Asegúrate de que este es el namespace correcto
using Practica.Utils;
using System;

namespace Practica.Model.Tests
{
    [TestClass]
    public class ActivityTests
    {
        // --- Datos comunes para reutilizar en los tests ---
        private readonly User testUser = new User();
        private readonly string testName = "Entrenamiento de prueba";
        private readonly DateTime testDate = new DateTime(2025, 10, 28);

        #region ActivityRunning Tests

        [TestMethod]
        public void ActivityRunning_Constructor_ValidData_CreatesInstance()
        {
            var activity = new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", 10f, 150);
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivityRunning_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", -10f, 150));
        }

        [TestMethod]
        public void ActivityRunning_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActivityRunning(testUser, testName, "", testDate, 0, "", "Parque", 10f, 150));
        }

        [TestMethod]
        public void ActivityRunning_Rythm_ZeroDistance_ThrowsDivideByZeroException()
        {
            var activity = new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", 0f, 150);
            Assert.ThrowsException<DivideByZeroException>(() => activity.Rythm());
        }

        [TestMethod]
        public void ActivityRunning_ObtainActivity_ReturnsCorrectFormat()
        {
            var activity = new ActivityRunning(testUser, "Series en cuesta", "", testDate, 45, "", "La Quinta", 8.5f, 200);
            string expected = "Carrera en La Quinta de 8,5 a un ritmo de: 5,29 mins/km";
            Assert.AreEqual(expected, activity.ObtainActivity());
        }

        #endregion

        #region ActividadCycling Tests

        [TestMethod]
        public void ActividadCycling_Constructor_ValidData_CreatesInstance()
        {
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 50f, 800);
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActividadCycling_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", -50f, 800));
        }

        [TestMethod]
        public void ActividadCycling_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActividadCycling(testUser, testName, "", testDate, 0, "", "Puerto", 50f, 800));
        }

        [TestMethod]
        public void ActividadCycling_RythmAndSpeed_CalculateCorrectly()
        {
            var activity = new ActividadCycling(testUser, testName, "", testDate, 90, "", "Carretera", 45f, 300);
            Assert.AreEqual("2,00", activity.Rythm(), "El cálculo del ritmo es incorrecto.");
            Assert.AreEqual("30,00", activity.Speed(), "El cálculo de la velocidad es incorrecto.");
        }

        [TestMethod]
        public void ActividadCycling_Rythm_ZeroDistance_ThrowsDivideByZeroException()
        {
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 0f, 800);
            Assert.ThrowsException<DivideByZeroException>(() => activity.Rythm());
        }

        [TestMethod]
        public void ActividadCycling_ObtainActivity_ReturnsCorrectFormat()
        {
            var activity = new ActividadCycling(testUser, "Ruta larga", "", testDate, 120, "", "Merindades", 40f, 500);
            string expected = "Ciclismo en Merindades de 40 a un ritmo de: 3,00 mins/km. Y una velocidad media de: 20,00 km/h, y un desnivel de 500";
            Assert.AreEqual(expected, activity.ObtainActivity());
        }

        #endregion

        #region ActivitySwimming Tests

        [TestMethod]
        public void ActivitySwimming_Constructor_ValidData_CreatesInstance()
        {
            var activity = new ActivitySwimming(testUser, testName, "", testDate, 45, "", "Piscina", 1500);
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivitySwimming_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActivitySwimming(testUser, testName, "", testDate, 45, "", "Piscina", -1500));
        }

        [TestMethod]
        public void ActivitySwimming_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActivitySwimming(testUser, testName, "", testDate, 0, "", "Piscina", 1500));
        }

        [TestMethod]
        public void ActivitySwimming_ObtainActivity_ReturnsCorrectFormat()
        {
            var activity = new ActivitySwimming(testUser, "Técnica de crol", "", testDate, 50, "", "San Amaro", 2000);
            string expected = "Natación en San Amaro de 2000 m en 50 mins";
            Assert.AreEqual(expected, activity.ObtainActivity());
        }

        #endregion

        #region ActivityGym Tests

        [TestMethod]
        public void ActivityGym_Constructor_ValidData_CreatesInstance()
        {
            var activity = new ActivityGym(testUser, testName, "", testDate, 60, "", 300, "Tren superior");
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivityGym_Constructor_NegativeCalories_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActivityGym(testUser, testName, "", testDate, 60, "", -100, "Tren superior"));
        }

        [TestMethod]
        public void ActivityGym_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActivityGym(testUser, testName, "", testDate, 0, "", 300, "Tren superior"));
        }

        [TestMethod]
        public void ActivityGym_ObtainActivity_ReturnsCorrectFormat()
        {
            var activity = new ActivityGym(testUser, "Día de pierna", "", testDate, 75, "", 450, "Tren inferior");
            string expected = "Entrenamiento de Tren inferior, quemando 450 calorías en 75 mins";
            Assert.AreEqual(expected, activity.ObtainActivity());
        }

        #endregion

        #region ActivityOther Tests

        [TestMethod]
        public void ActivityOther_Constructor_ValidData_CreatesInstance()
        {
            var activity = new ActivityOther(testUser, testName, "", testDate, 45, "", "Polideportivo", "Pádel");
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivityOther_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ActivityOther(testUser, testName, "", testDate, 0, "", "Polideportivo", "Pádel"));
        }

        [TestMethod]
        public void ActivityOther_ObtainActivity_ReturnsCorrectFormat()
        {
            var activity = new ActivityOther(testUser, "Partido semanal", "", testDate, 90, "", "Club de Campo", "Tenis");
            string expected = "Entrenamiento de Tenis en 90 mins en Club de Campo .";
            Assert.AreEqual(expected, activity.ObtainActivity());
        }

        #endregion
    }
}