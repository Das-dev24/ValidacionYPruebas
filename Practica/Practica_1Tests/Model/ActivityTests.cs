using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica_1.Model;

namespace Practica.Model.Tests
{ 
[TestClass]
public class ActivityTests
{           
            //Ahora dan un coverage del 100% con estos tests
            // --- TEST PARA LA LÓGICA COMÚN (a través de RunningActivity) ---
            [TestMethod]
            public void Constructor_InitializesBaseProperties_Correctly()
            {
                // Arrange
                var expectedName = "Carrera";
                var expectedDistance = 7.5f;

                // Act
                var runningActivity = new ActivityRunning(1, 101, expectedName, "", DateTime.Now, 60, "Buen ritmo","Penstasa", expectedDistance,200);

                // Assert
                string result = runningActivity.ObtainActivity();
                StringAssert.Contains(result, expectedName);
                StringAssert.Contains(result, expectedDistance.ToString());
            }

            // --- TESTS ESPECÍFICOS PARA CADA CLASE HIJA ---

            [TestMethod]
            public void ObtainActivity_ReturnsCorrectFormat_ForRunningActivity()
            {
                // Arrange
                var activity = new ActivityRunning(2, 102, "Entrenamiento series", "", DateTime.Now, 60, "Series en cuesta","La quinta", 10.0f, 350);
                string expectedString = "Carrera en La quinta de 10 a un ritmo de: 6.00 mins/km";

            // Act
            string actualString = activity.ObtainActivity();

                // Assert
                Assert.AreEqual(expectedString, actualString);
            }

            [TestMethod]
            public void ObtainActivity_ReturnsCorrectFormat_ForCyclingActivity()
            {
                // Arrange
                var activity = new ActividadCycling(3, 103, "Ruta de montaña","",DateTime.Now, 120, "Mucho viento","Merindades", 22.5f, +200);
                string expectedString = "Ciclismo en Merindades de 22.5 a un ritmo de: 5.33 mins/km. Y una velocidad media de: 11.25 km/h, y un desnivel de 200" ;

                // Act
                string actualString = activity.ObtainActivity();

                // Assert
                Assert.AreEqual(expectedString, actualString);
            }

            [TestMethod]
            public void ObtainActivity_ReturnsCorrectFormat_ForSwimmingActivity()
            {
                // Arrange
                var activity = new ActivitySwimming(4, 104, "Entrenamiento en piscina", "" ,DateTime.Now, 50, "Técnica de crol","San Amaro", 1500);
                string expectedString = "Natación en San Amaro de 1500 m en 50 mins";

            // Act
            string actualString = activity.ObtainActivity();

                // Assert
                Assert.AreEqual(expectedString, actualString);
            }
        }

    }