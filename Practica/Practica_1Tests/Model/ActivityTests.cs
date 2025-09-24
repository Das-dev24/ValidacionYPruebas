using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Model; // Asegúrate que este namespace sea el correcto
using System; //Hecho por mi

namespace Practica.Model.Tests
{
    [TestClass]
    public class ActivityTests
    {
        // --- Datos comunes para los tests ---
        private readonly int testId = 1;
        private readonly int testUserId = 101;
        private readonly string testName = "Test Activity";
        private readonly DateTime testDate = new DateTime(2025, 9, 23);

        #region ActivityRunning Tests

        [TestMethod]
        public void ActivityRunning_Constructor_ValidData_CreatesInstance()
        {
            var activity = new ActivityRunning(testId, testUserId, testName, "", testDate, 60, "", "Parque", 10f, 150);
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivityRunning_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActivityRunning(testId, testUserId, testName, "", testDate, 60, "", "Parque", -10f, 150);}, "Debería lanzar ArgumentException para distancia negativa.");
        }

        [TestMethod]
        public void ActivityRunning_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActivityRunning(testId, testUserId, testName, "", testDate, 0, "", "Parque", 10f, 150); }, "Debería lanzar ArgumentException para duración cero.");
        }

        [TestMethod]
        public void ActivityRunning_Constructor_NegativeDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActivityRunning(testId, testUserId, testName, "", testDate, -60, "", "Parque", 10f, 150); }, "Debería lanzar ArgumentException para duración negativa.");
        }

        [TestMethod]
        public void ActivityRunning_Rythm_CalculatesCorrectly()
        {
            var activity = new ActivityRunning(testId, testUserId, testName, "", testDate, 45, "", "Pista", 8.5f, 50);
            string expectedRythm = "5,29"; // 45 / 8.5 = 5.2941...
            string actualRythm = activity.Rythm();
            Assert.AreEqual(expectedRythm, actualRythm);
        }

        [TestMethod]
        public void ActivityRunning_Rythm_ZeroDistance_ThrowsDivideByZeroException()
        {
            var activity = new ActivityRunning(testId, testUserId, testName, "", testDate, 60, "", "Parque", 0f, 150);
            Assert.ThrowsException<DivideByZeroException>(() => activity.Rythm());
        }

        [TestMethod]
        public void ActivityRunning_ObtainActivity_ReturnsCorrectFormat()
        {
            var activity = new ActivityRunning(testId, testUserId, "Series", "", testDate, 60, "", "Montaña", 10f, 200);
            string expectedString = "Carrera en Montaña de 10 a un ritmo de: 6,00 mins/km";
            string actualString = activity.ObtainActivity();
            Assert.AreEqual(expectedString, actualString);
        }

        #endregion

        #region ActivitySwimming Tests

        [TestMethod]
        public void ActivitySwimming_Constructor_ValidData_CreatesInstance()
        {
            var activity = new ActivitySwimming(testId, testUserId, testName, "", testDate, 45, "", "Piscina", 1500);
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivitySwimming_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActivitySwimming(testId, testUserId, testName, "", testDate, 45, "", "Piscina", -1500);}, "Debería lanzar ArgumentException para distancia negativa.");
        }

        // --- TEST AÑADIDO ---
        [TestMethod]
        public void ActivitySwimming_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActivitySwimming(testId, testUserId, testName, "", testDate, 0, "", "Piscina", 1500); }, "Debería lanzar ArgumentException para duración cero.");
        }

        // --- TEST AÑADIDO ---
        [TestMethod]
        public void ActivitySwimming_Constructor_NegativeDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActivitySwimming(testId, testUserId, testName, "", testDate, -45, "", "Piscina", 1500); }, "Debería lanzar ArgumentException para duración negativa.");
        }

        [TestMethod]
        public void ActivitySwimming_ObtainActivity_ReturnsCorrectFormat()
        {
            var activity = new ActivitySwimming(testId, testUserId, "Entrenamiento", "", testDate, 50, "", "Mar", 2000);
            string expectedString = "Natación en Mar de 2000 m en 50 mins";
            string actualString = activity.ObtainActivity();
            Assert.AreEqual(expectedString, actualString);
        }

        #endregion

        #region ActividadCycling Tests

        [TestMethod]
        public void ActividadCycling_Constructor_ValidData_CreatesInstance()
        {
            var activity = new ActividadCycling(testId, testUserId, testName, "", testDate, 120, "", "Puerto", 50f, 800);
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActividadCycling_Constructor_NegativeDistance_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActividadCycling(testId, testUserId, testName, "", testDate, 120, "", "Puerto", -50f, 800); }, "Debería lanzar ArgumentException para distancia negativa.");
        }

        // --- TEST AÑADIDO ---
        [TestMethod]
        public void ActividadCycling_Constructor_ZeroDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActividadCycling(testId, testUserId, testName, "", testDate, 0, "", "Puerto", 50f, 800); }, "Debería lanzar ArgumentException para duración cero.");
        }

        // --- TEST AÑADIDO ---
        [TestMethod]
        public void ActividadCycling_Constructor_NegativeDuration_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ActividadCycling(testId, testUserId, testName, "", testDate, -120, "", "Puerto", 50f, 800); }, "Debería lanzar ArgumentException para duración negativa.");
        }

        [TestMethod]
        public void ActividadCycling_RythmAndSpeed_CalculateCorrectly()
        {
            var activity = new ActividadCycling(testId, testUserId, testName, "", testDate, 90, "", "Carretera", 45f, 300);
            string expectedRythm = "2,00"; // 90 / 45 = 2
            string expectedSpeed = "30,00"; // 45 / (90 / 60) = 30

            string actualRythm = activity.Rythm();
            string actualSpeed = activity.Speed();

            Assert.AreEqual(expectedRythm, actualRythm, "El cálculo del ritmo es incorrecto.");
            Assert.AreEqual(expectedSpeed, actualSpeed, "El cálculo de la velocidad es incorrecto.");
        }

        [TestMethod]
        public void ActividadCycling_Rythm_ZeroDistance_ThrowsDivideByZeroException()
        {
            var activity = new ActividadCycling(testId, testUserId, testName, "", testDate, 120, "", "Puerto", 0f, 800);
            Assert.ThrowsException<DivideByZeroException>(() => activity.Rythm());
        }


        [TestMethod]
        public void ActividadCycling_ObtainActivity_ReturnsCorrectFormat()
        {
            var activity = new ActividadCycling(testId, testUserId, "Ruta", "", testDate, 120, "", "Merindades", 40f, 500);
            string expectedString = "Ciclismo en Merindades de 40 a un ritmo de: 3,00 mins/km. Y una velocidad media de: 20,00 km/h, y un desnivel de 500";

            string actualString = activity.ObtainActivity();

            Assert.AreEqual(expectedString, actualString);
        }

        #endregion
    }
}