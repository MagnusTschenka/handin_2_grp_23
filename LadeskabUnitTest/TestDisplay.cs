using System;
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
        private ChargeControl _chargeControl = null;



        //setup funktionen hvor der laves en Nsubstitute (fake/mock)
        [SetUp]
        public void Setup()
        {
            _uut = new Display();


            //opretter en instans af en "fake/mock" der kan påvirke displayet. 
            _chargeControl = Substitute.For<ChargeControl>(_uut);
           
        }

        [Test]
        public void Test_Print_Statement_PrintConnectionError()
        {
            //laver en simpel test på default-værdien for display. Umiddelbart
            //returneres ingenting.
            //Assert.That(_uut.RunSelfTest().Returns(Test_Print_Statement_PrintConnectionError()));

            _chargeControl.Connected.Returns(false);
            Assert.IsFalse(_uut.Returns());


        }


                //*** Indsat fra slides til inspiration med en mock***

        //[Test]
        //public void DoorBreached_DoorStateIsBreached_AlarmCalled()
        //{
        //    _uut.DoorOpened();  // Breach door
        //    Assert.That(_mockFactory.Alarm.WasAlarmCalled, Is.True);
        //}


    }
}

