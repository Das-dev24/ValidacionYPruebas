using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Model;
using Practica.Utils;
using System;

namespace Practica.Model.Tests
{
    [TestClass]
    public class ActivityTests
    {
        // --- Datos comunes para reutilizar en los tests ---
        private User testUser;
        private readonly string testName = "Entrenamiento de prueba";
        private readonly DateTime testDate = new DateTime(2025, 10, 28);

        [TestInitialize]
        public void Setup()
        {
            // Crear un usuario de prueba antes de cada test
            testUser = new User("Test", "User", "test@activity.com", "Password123!");
            testUser.Id = 1;
        }

        #region ActivityRunning Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityRunning_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            // Act
            new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", -10f, 150);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityRunning_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            // Act
            new ActivityRunning(testUser, testName, "", testDate, 0, "", "Parque", 10f, 150);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityRunning_Constructor_NegativeDuration_ThrowsArgumentException()
        {
            // Act
            new ActivityRunning(testUser, testName, "", testDate, -5, "", "Parque", 10f, 150);
        }

        [TestMethod]
        public void ActivityRunning_Constructor_ZeroDistance_CreatesInstance()
        {
            // Arrange & Act
            var activity = new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", 0f, 150);

            // Assert
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void ActivityRunning_Rythm_ZeroDistance_ThrowsDivideByZeroException()
        {
            // Arrange
            var activity = new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", 0f, 150);

            // Act
            activity.Rythm();
        }

        [TestMethod]
        public void ActivityRunning_Rythm_ValidDistance_CalculatesCorrectly()
        {
            // Arrange
            var activity = new ActivityRunning(testUser, testName, "", testDate, 50, "", "Parque", 10f, 150);

            // Act
            string rythm = activity.Rythm();

            // Assert
            Assert.AreEqual("5,00", rythm);
        }

        [TestMethod]
        public void ActivityRunning_ObtainActivity_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActivityRunning(testUser, "Series en cuesta", "", testDate, 45, "", "La Quinta", 8.5f, 200);

            // Act
            string result = activity.ObtainActivity();

            // Assert
            string expected = "Carrera en La Quinta de 8,5 a un ritmo de: 5,29 mins/km";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ActivityRunning_ObtainActivity_DifferentValues_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActivityRunning(testUser, "Carrera larga", "", testDate, 120, "", "Montaña", 15.0f, 350);

            // Act
            string result = activity.ObtainActivity();

            // Assert
            Assert.IsTrue(result.Contains("Carrera en Montaña"));
            Assert.IsTrue(result.Contains("15"));
            Assert.IsTrue(result.Contains("mins/km"));
        }

        #endregion

        #region ActividadCycling Tests
   

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActividadCycling_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            // Act
            new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", -50f, 800);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActividadCycling_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            // Act
            new ActividadCycling(testUser, testName, "", testDate, 0, "", "Puerto", 50f, 800);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActividadCycling_Constructor_NegativeDuration_ThrowsArgumentException()
        {
            // Act
            new ActividadCycling(testUser, testName, "", testDate, -10, "", "Puerto", 50f, 800);
        }

        [TestMethod]
        public void ActividadCycling_Constructor_ZeroDistance_CreatesInstance()
        {
            // Arrange & Act
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 0f, 800);

            // Assert
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void ActividadCycling_Rythm_ZeroDistance_ThrowsDivideByZeroException()
        {
            // Arrange
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 0f, 800);

            // Act
            activity.Rythm();
        }

        [TestMethod]
        public void ActividadCycling_Rythm_ValidDistance_CalculatesCorrectly()
        {
            // Arrange
            var activity = new ActividadCycling(testUser, testName, "", testDate, 90, "", "Carretera", 45f, 300);

            // Act
            string rythm = activity.Rythm();

            // Assert
            Assert.AreEqual("2,00", rythm);
        }

        [TestMethod]
        public void ActividadCycling_Speed_ValidData_CalculatesCorrectly()
        {
            // Arrange
            var activity = new ActividadCycling(testUser, testName, "", testDate, 90, "", "Carretera", 45f, 300);

            // Act
            string speed = activity.Speed();

            // Assert
            Assert.AreEqual("30,00", speed);
        }

        [TestMethod]
        public void ActividadCycling_Speed_DifferentValues_CalculatesCorrectly()
        {
            // Arrange
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Montaña", 40f, 500);

            // Act
            string speed = activity.Speed();

            // Assert
            Assert.AreEqual("20,00", speed);
        }

        [TestMethod]
        public void ActividadCycling_Speed_ZeroDistance_ReturnsZero()
        {
            // Arrange
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 0f, 800);

            // Act
            string speed = activity.Speed();

            // Assert
            Assert.AreEqual("0,00", speed);
        }

        [TestMethod]
        public void ActividadCycling_RythmAndSpeed_CalculateCorrectly()
        {
            // Arrange
            var activity = new ActividadCycling(testUser, testName, "", testDate, 90, "", "Carretera", 45f, 300);

            // Act
            string rythm = activity.Rythm();
            string speed = activity.Speed();

            // Assert
            Assert.AreEqual("2,00", rythm, "El cálculo del ritmo es incorrecto.");
            Assert.AreEqual("30,00", speed, "El cálculo de la velocidad es incorrecto.");
        }

        [TestMethod]
        public void ActividadCycling_ObtainActivity_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActividadCycling(testUser, "Ruta larga", "", testDate, 120, "", "Merindades", 40f, 500);

            // Act
            string result = activity.ObtainActivity();

            // Assert
            string expected = "Ciclismo en Merindades de 40 a un ritmo de: 3,00 mins/km. Y una velocidad media de: 20,00 km/h, y un desnivel de 500";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ActividadCycling_ObtainActivity_DifferentValues_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActividadCycling(testUser, "Salida corta", "", testDate, 60, "", "Ciudad", 20f, 100);

            // Act
            string result = activity.ObtainActivity();

            // Assert
            Assert.IsTrue(result.Contains("Ciclismo en Ciudad"));
            Assert.IsTrue(result.Contains("20"));
            Assert.IsTrue(result.Contains("desnivel de 100"));
        }

        #endregion

        #region ActivitySwimming Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivitySwimming_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            // Act
            new ActivitySwimming(testUser, testName, "", testDate, 45, "", "Piscina", -1500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivitySwimming_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            // Act
            new ActivitySwimming(testUser, testName, "", testDate, 0, "", "Piscina", 1500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivitySwimming_Constructor_NegativeDuration_ThrowsArgumentException()
        {
            // Act
            new ActivitySwimming(testUser, testName, "", testDate, -5, "", "Piscina", 1500);
        }

        [TestMethod]
        public void ActivitySwimming_Constructor_ZeroDistance_CreatesInstance()
        {
            // Arrange & Act
            var activity = new ActivitySwimming(testUser, testName, "", testDate, 45, "", "Piscina", 0);

            // Assert
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivitySwimming_ObtainActivity_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActivitySwimming(testUser, "Técnica de crol", "", testDate, 50, "", "San Amaro", 2000);

            // Act
            string result = activity.ObtainActivity();

            // Assert
            string expected = "Natación en San Amaro de 2000 m en 50 mins";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ActivitySwimming_ObtainActivity_DifferentValues_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActivitySwimming(testUser, "Entrenamiento corto", "", testDate, 30, "", "Club Deportivo", 1000);

            // Act
            string result = activity.ObtainActivity();

            // Assert
            Assert.IsTrue(result.Contains("Natación en Club Deportivo"));
            Assert.IsTrue(result.Contains("1000 m"));
            Assert.IsTrue(result.Contains("30 mins"));
        }

        #endregion

        #region ActivityGym Tests


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityGym_Constructor_NegativeCalories_ThrowsArgumentException()
        {
            // Act
            new ActivityGym(testUser, testName, "", testDate, 60, "", -100, "Tren superior");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityGym_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            // Act
            new ActivityGym(testUser, testName, "", testDate, 0, "", 300, "Tren superior");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityGym_Constructor_NegativeDuration_ThrowsArgumentException()
        {
            // Act
            new ActivityGym(testUser, testName, "", testDate, -10, "", 300, "Tren superior");
        }

        [TestMethod]
        public void ActivityGym_Constructor_ZeroCalories_CreatesInstance()
        {
            // Arrange & Act
            var activity = new ActivityGym(testUser, testName, "", testDate, 60, "", 0, "Tren superior");

            // Assert
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivityGym_ObtainActivity_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActivityGym(testUser, "Día de pierna", "", testDate, 75, "", 450, "Tren inferior");

            // Act
            string result = activity.ObtainActivity();

            // Assert
            string expected = "Entrenamiento de Tren inferior, quemando 450 calorías en 75 mins";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ActivityGym_ObtainActivity_DifferentValues_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActivityGym(testUser, "Cardio", "", testDate, 45, "", 350, "Cardiovascular");

            // Act
            string result = activity.ObtainActivity();

            // Assert
            Assert.IsTrue(result.Contains("Entrenamiento de Cardiovascular"));
            Assert.IsTrue(result.Contains("350 calorías"));
            Assert.IsTrue(result.Contains("45 mins"));
        }

        #endregion

        #region ActivityOther Tests


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityOther_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            // Act
            new ActivityOther(testUser, testName, "", testDate, 0, "", "Polideportivo", "Pádel");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityOther_Constructor_NegativeDuration_ThrowsArgumentException()
        {
            // Act
            new ActivityOther(testUser, testName, "", testDate, -15, "", "Polideportivo", "Pádel");
        }

        [TestMethod]
        public void ActivityOther_ObtainActivity_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActivityOther(testUser, "Partido semanal", "", testDate, 90, "", "Club de Campo", "Tenis");

            // Act
            string result = activity.ObtainActivity();

            // Assert
            string expected = "Entrenamiento de Tenis en 90 mins en Club de Campo .";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ActivityOther_ObtainActivity_DifferentValues_ReturnsCorrectFormat()
        {
            // Arrange
            var activity = new ActivityOther(testUser, "Clase de yoga", "", testDate, 60, "", "Estudio", "Yoga");

            // Act
            string result = activity.ObtainActivity();

            // Assert
            Assert.IsTrue(result.Contains("Entrenamiento de Yoga"));
            Assert.IsTrue(result.Contains("60 mins"));
            Assert.IsTrue(result.Contains("Estudio"));
        }

        #endregion

        #region Edge Cases and Integration Tests

        [TestMethod]
        public void AllActivities_TypeActivity_SetCorrectly()
        {
            // Arrange & Act
            var running = new ActivityRunning(testUser, "Run", "", testDate, 60, "", "Park", 10f, 150);
            var cycling = new ActividadCycling(testUser, "Bike", "", testDate, 120, "", "Road", 50f, 800);
            var swimming = new ActivitySwimming(testUser, "Swim", "", testDate, 45, "", "Pool", 1500);
            var gym = new ActivityGym(testUser, "Gym", "", testDate, 60, "", 300, "Upper");
            var other = new ActivityOther(testUser, "Other", "", testDate, 45, "", "Place", "Tennis");

            // Assert
            Assert.AreEqual("Carrera", running.TypeActivity);
            Assert.AreEqual("Ciclismo", cycling.TypeActivity);
            Assert.AreEqual("Natación", swimming.TypeActivity);
            Assert.AreEqual("Gimnasio", gym.TypeActivity);
            Assert.AreEqual("Other", other.TypeActivity);
        }

        #endregion
    }
}