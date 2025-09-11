using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica_0.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_0.utils.Tests
{
    [TestClass()]
    public class ValidarTests
    {
        [TestMethod()]
        public void NIFTest()
        {
            Assert.IsTrue(Validar.NIF("71706830Y"));
            Assert.IsFalse(Validar.NIF("71706830E"));
        }

        [TestMethod()]
        public void IBANTest(){
            Assert.IsTrue(Validar.IBAN("ES91 2100 0418 4502 0005 1332"));
        }
    }
}