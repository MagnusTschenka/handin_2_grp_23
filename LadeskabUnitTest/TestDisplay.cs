using System;
using System.IO;
using NUnit.Framework;
using Ladeskab;
using NSubstitute;

namespace LadeskabUnitTest
{
    [TestFixture]
    public class TestDisplay
    {
        //oprettelse af objekter
        private Display _uut = null;



        //setup funktionen hvor der laves en Nsubstitute (fake/mock)
        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }



        [Test]
        public void Test_Print_Statement_PrintConnectionError()
        {
            //arrange
            var expected = "Tilslutningsfejl\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintConnectionError();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }



        [Test]
        public void Test_Print_Statement_PrintConnectPhone()
        {
            //arrange
            var expected = "Tilsut telefon\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintConnectPhone();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [Test]
        public void Test_Print_Statement_PrintCurrentlyCharging()
        {
            //arrange
            var expected = "Telefonen er igang med at oplade\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintCurrentlyCharging();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [Test]
        public void Test_Print_Statement_PrintFullyCharged()
        {
            //arrange
            var expected = "Telefon er fuldt opladet og kan nu fjernes\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintFullyCharged();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [Test]
        public void Test_Print_Statement_PrintLoadRFID()
        {
            //arrange
            var expected = "Indlæs RFID\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintLoadRFID();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [Test]
        public void Test_Print_Statement_PrintOccupied()
        {
            //arrange
            var expected = "Ladeskab optaget\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintOccupied();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [Test]
        public void Test_Print_Statement_PrintOverchargeError()
        {
            //arrange
            var expected = "Teknisk fejl, fjern venligst telefon!\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintOverchargeError();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [Test]
        public void Test_Print_Statement_PrintRemovedPhone()
        {
            //arrange
            var expected = "Fjern telefon\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintRemovedPhone();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [Test]
        public void Test_Print_Statement_PrintRFIDError()
        {
            //arrange
            var expected = "RFID fejl\r\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _uut.PrintRFIDError();

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }


    }
}

