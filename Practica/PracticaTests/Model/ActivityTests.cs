using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Model;
using System;

// No se necesitan todas las directivas using no utilizadas (ej. System.Collections.Generic)

namespace Practica.Model.Tests {
    [TestClass]
    public class ActivityTests {

        private User testUser;
        private readonly string testName = "Entrenamiento de prueba";
        private readonly DateTime testDate = new DateTime(2025, 10, 28);

        [TestInitialize]
        public void Setup() {
            // Crear un usuario de prueba antes de cada test
            testUser = new User("Test", "User", "test@activity.com", "Password123!");
            testUser.Id = 1;
        }

        #region ActivityRunning Tests

        [TestMethod]
        [DataRow(-10f, 60, "La distancia debe ser mayor que cero.")]
        [DataRow(10f, 0, "La duración debe ser mayor que cero.")]
        [DataRow(10f, -5, "La duración debe ser mayor que cero.")]
        public void ActivityRunning_Constructor_WithInvalidArguments_ThrowsArgumentException(float distance, int duration, string expectedMessage) {
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<ArgumentException>(() => {
                new ActivityRunning(testUser, testName, "", testDate, duration, "", "Parque", distance, 150);
            });
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestMethod]
        public void ActivityRunning_Constructor_WithZeroDistance_CreatesInstanceSuccessfully() {
            var activity = new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", 0f, 150);
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActivityRunning_Rythm_WhenDistanceIsZero_ThrowsDivideByZeroException() {
            var activity = new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", 0f, 150);
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<DivideByZeroException>(() => activity.Rythm());
            Assert.AreEqual("La distancia no puede ser cero al calcular la velocidad.", ex.Message);
        }

        [TestMethod]
        public void ActivityRunning_Rythm_WithValidInputs_CalculatesCorrectly() {
            var activity = new ActivityRunning(testUser, testName, "", testDate, 50, "", "Parque", 10f, 150);
            string rythm = activity.Rythm();
            string expectedRythm = (50f / 10f).ToString("0.00");
            Assert.AreEqual(expectedRythm, rythm);
        }

        [TestMethod]
        [DataRow("Series en cuesta", 45, "La Quinta", 8.5f, "Carrera en La Quinta de 8,5 a un ritmo de: 5,29 mins/km")]
        [DataRow("Carrera larga", 120, "Montaña", 15.0f, "Carrera en Montaña de 15 a un ritmo de: 8,00 mins/km")]
        public void ActivityRunning_ObtainActivity_WithValidInputs_ReturnsCorrectlyFormattedString(string name, int duration, string place, float distance, string expected) {
            var activity = new ActivityRunning(testUser, name, "", testDate, duration, "", place, distance, 200);
            string result = activity.ObtainActivity();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ActivityRunning_ObtainActivity_WhenDistanceIsZero_ThrowsDivideByZeroException() {
            var activity = new ActivityRunning(testUser, testName, "", testDate, 60, "", "Parque", 0f, 150);
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<DivideByZeroException>(() => activity.ObtainActivity());
            Assert.AreEqual("La distancia no puede ser cero al calcular la velocidad.", ex.Message);
        }

        #endregion

        #region ActividadCycling Tests

        [TestMethod]
        [DataRow(-50f, 120, "La distancia debe ser mayor que cero.")]
        [DataRow(50f, 0, "La duración debe ser mayor que cero.")]
        [DataRow(50f, -10, "La duración debe ser mayor que cero.")]
        public void ActividadCycling_Constructor_WithInvalidArguments_ThrowsArgumentException(float distance, int duration, string expectedMessage) {
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<ArgumentException>(() => {
                new ActividadCycling(testUser, testName, "", testDate, duration, "", "Puerto", distance, 800);
            });
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestMethod]
        public void ActividadCycling_Constructor_WithZeroDistance_CreatesInstanceSuccessfully() {
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 0f, 800);
            Assert.IsNotNull(activity);
        }

        [TestMethod]
        public void ActividadCycling_Rythm_WhenDistanceIsZero_ThrowsDivideByZeroException() {
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 0f, 800);
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<DivideByZeroException>(() => activity.Rythm());
            Assert.AreEqual("La distancia no puede ser cero al calcular el ritmo.", ex.Message);
        }

        [TestMethod]
        [DataRow(90, 45f, "2,00")]
        [DataRow(120, 40f, "3,00")]
        public void ActividadCycling_SpeedAndRythm_WithValidInputs_CalculateCorrectly(int duration, float distance, string expectedRythm) {
            var activity = new ActividadCycling(testUser, testName, "", testDate, duration, "", "Carretera", distance, 300);

            string rythm = activity.Rythm();
            string speed = activity.Speed();

            string expectedSpeed = (distance / (duration / 60.0f)).ToString("0.00");

            Assert.AreEqual(expectedRythm, rythm, "El cálculo del ritmo es incorrecto.");
            Assert.AreEqual(expectedSpeed, speed, "El cálculo de la velocidad es incorrecto.");
        }

        [TestMethod]
        public void ActividadCycling_Speed_WhenDistanceIsZero_ReturnsZero() {
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 0f, 800);
            string speed = activity.Speed();
            Assert.AreEqual("0,00", speed);
        }

        [TestMethod]
        [DataRow("Ruta larga", 120, "Merindades", 40f, 500, "Ciclismo en Merindades de 40 a un ritmo de: 3,00 mins/km. Y una velocidad media de: 20,00 km/h, y un desnivel de 500")]
        [DataRow("Salida corta", 60, "Ciudad", 20f, 100, "Ciclismo en Ciudad de 20 a un ritmo de: 3,00 mins/km. Y una velocidad media de: 20,00 km/h, y un desnivel de 100")]
        public void ActividadCycling_ObtainActivity_WithValidInputs_ReturnsCorrectlyFormattedString(string name, int duration, string place, float distance, int slope, string expected) {
            var activity = new ActividadCycling(testUser, name, "", testDate, duration, "", place, distance, slope);
            string result = activity.ObtainActivity();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ActividadCycling_ObtainActivity_WhenDistanceIsZero_ThrowsDivideByZeroException() {
            var activity = new ActividadCycling(testUser, testName, "", testDate, 120, "", "Puerto", 0f, 800);
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<DivideByZeroException>(() => activity.ObtainActivity());
            Assert.AreEqual("La distancia no puede ser cero al calcular el ritmo.", ex.Message);
        }

        #endregion

        #region ActivitySwimming Tests

        [TestMethod]
        [DataRow(-1500, 45, "La distancia en natación no puede ser negativa.")]
        [DataRow(1500, 0, "La duración debe ser mayor que cero.")]
        [DataRow(1500, -5, "La duración debe ser mayor que cero.")]
        public void ActivitySwimming_Constructor_WithInvalidArguments_ThrowsArgumentException(int distance, int duration, string expectedMessage) {
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<ArgumentException>(() => {
                new ActivitySwimming(testUser, testName, "", testDate, duration, "", "Piscina", distance);
            });
            Assert.AreEqual(expectedMessage, ex.Message, "El mensaje de la excepción no es el esperado.");
        }

        [TestMethod]
        public void ActivitySwimming_Constructor_WithZeroDistance_CreatesInstanceSuccessfully() {
            var activity = new ActivitySwimming(testUser, testName, "", testDate, 45, "", "Piscina", 0);
            Assert.IsNotNull(activity, "La actividad no debería ser nula con una distancia de cero.");
        }

        [TestMethod]
        [DataRow("Técnica de crol", 50, "San Amaro", 2000, "Natación en San Amaro de 2000 m en 50 mins")]
        [DataRow("Entrenamiento corto", 30, "Club Deportivo", 1000, "Natación en Club Deportivo de 1000 m en 30 mins")]
        public void ActivitySwimming_ObtainActivity_WithValidInputs_ReturnsCorrectlyFormattedString(string name, int duration, string place, int distance, string expected) {
            var activity = new ActivitySwimming(testUser, name, "", testDate, duration, "", place, distance);
            string result = activity.ObtainActivity();
            Assert.AreEqual(expected, result, "El formato de la cadena de la actividad es incorrecto.");
        }

        #endregion

        #region ActivityGym Tests

        [TestMethod]
        [DataRow(-100, 60, "Las calorias quemadas no pueden ser negativas.")]
        [DataRow(300, 0, "La duración debe ser mayor que cero.")]
        [DataRow(300, -10, "La duración debe ser mayor que cero.")]
        public void ActivityGym_Constructor_WithInvalidArguments_ThrowsArgumentException(int calories, int duration, string expectedMessage) {
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<ArgumentException>(() => {
                new ActivityGym(testUser, testName, "", testDate, duration, "", calories, "Tren superior");
            });
            Assert.AreEqual(expectedMessage, ex.Message, "El mensaje de la excepción no es el esperado.");
        }

        [TestMethod]
        public void ActivityGym_Constructor_WithZeroCalories_CreatesInstanceSuccessfully() {
            var activity = new ActivityGym(testUser, testName, "", testDate, 60, "", 0, "Tren superior");
            Assert.IsNotNull(activity, "La actividad no debería ser nula con cero calorías.");
        }

        [TestMethod]
        [DataRow("Día de pierna", 75, 450, "Tren inferior", "Entrenamiento de Tren inferior, quemando 450 calorías en 75 mins")]
        [DataRow("Cardio", 45, 350, "Cardiovascular", "Entrenamiento de Cardiovascular, quemando 350 calorías en 45 mins")]
        public void ActivityGym_ObtainActivity_WithValidInputs_ReturnsCorrectlyFormattedString(string name, int duration, int calories, string trainingType, string expected) {
            var activity = new ActivityGym(testUser, name, "", testDate, duration, "", calories, trainingType);
            string result = activity.ObtainActivity();
            Assert.AreEqual(expected, result, "El formato de la cadena de la actividad es incorrecto.");
        }

        #endregion

        #region ActivityOther Tests

        [TestMethod]
        [DataRow(0)]
        [DataRow(-15)]
        public void ActivityOther_Constructor_WithInvalidDuration_ThrowsArgumentException(int invalidDuration) {
            // Usando el método de utilidad AssertThrows
            var ex = AssertThrows<ArgumentException>(() => {
                new ActivityOther(testUser, testName, "", testDate, invalidDuration, "", "Polideportivo", "Pádel");
            });
            Assert.AreEqual("La duración debe ser mayor que cero.", ex.Message, "El mensaje de la excepción no es el esperado.");
        }

        [TestMethod]
        [DataRow("Partido semanal", 90, "Club de Campo", "Tenis", "Entrenamiento de Tenis en 90 mins en Club de Campo .")]
        [DataRow("Clase de yoga", 60, "Estudio", "Yoga", "Entrenamiento de Yoga en 60 mins en Estudio .")]
        public void ActivityOther_ObtainActivity_WithValidInputs_ReturnsCorrectlyFormattedString(string name, int duration, string place, string otherActivity, string expected) {
            var activity = new ActivityOther(testUser, name, "", testDate, duration, "", place, otherActivity);
            string result = activity.ObtainActivity();
            Assert.AreEqual(expected, result, "El formato de la cadena de la actividad es incorrecto.");
        }

        #endregion

        #region Edge Cases and Integration Tests

        [TestMethod]
        public void AllActivities_TypeActivity_SetCorrectly() {
            var running = new ActivityRunning(testUser, "Run", "", testDate, 60, "", "Park", 10f, 150);
            var cycling = new ActividadCycling(testUser, "Bike", "", testDate, 120, "", "Road", 50f, 800);
            var swimming = new ActivitySwimming(testUser, "Swim", "", testDate, 45, "", "Pool", 1500);
            var gym = new ActivityGym(testUser, "Gym", "", testDate, 60, "", 300, "Upper");
            var other = new ActivityOther(testUser, "Other", "", testDate, 45, "", "Place", "Tennis");

            Assert.AreEqual("Carrera", running.TypeActivity);
            Assert.AreEqual("Ciclismo", cycling.TypeActivity);
            Assert.AreEqual("Natación", swimming.TypeActivity);
            Assert.AreEqual("Gimnasio", gym.TypeActivity);
            Assert.AreEqual("Other", other.TypeActivity);
        }

        [TestMethod]
        public void GetUser_WhenCalledOnAnyActivity_ReturnsCorrectUser() {
            var activity = new ActivityRunning(testUser, "Carrera de prueba", "", testDate, 60, "", "Parque", 10f, 150);
            User returnedUser = activity.GetUser();

            Assert.IsNotNull(returnedUser, "El usuario devuelto no debería ser nulo.");
            Assert.AreSame(testUser, returnedUser, "El objeto User devuelto debe ser la misma instancia que el original.");
            Assert.AreEqual(testUser.Id, returnedUser.Id, "El ID del usuario devuelto debe coincidir.");
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