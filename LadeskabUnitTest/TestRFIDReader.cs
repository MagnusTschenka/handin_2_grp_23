using System;
using NUnit.Framework;
using Ladeskab;
using Ladeskab.Interfaces;


namespace LadeskabUnitTest
{
    [TestFixture]

    public class TestRFIDReader
    {
        private RFIDReader _uut;
        private RFIDDetectedEventArgs _RecievedEventArgs;
        [SetUp]
        public void SetUp()
        {
            _RecievedEventArgs = null;
            _uut = new RFIDReader();
            _uut.SetRFIDStatus(420);

            _uut.RfidEventDetected += (o, args) => { _RecievedEventArgs = args; };
        }

        [Test]
        public void TestSetRFIDStatusEvent_Fired()
        {
            _uut.SetRFIDStatus(69);
            Assert.That(_RecievedEventArgs, Is.Not.Null);
        }
        [Test]
        public void TestSetRFIDStatusEvent_CorrectRFIDStatus()
        {
            _uut.SetRFIDStatus(69);
            Assert.That(_RecievedEventArgs.Id, Is.EqualTo(69));
        }
      
    }
}

