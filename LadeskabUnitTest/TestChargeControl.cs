using System;
using NSubstitute;
using NUnit.Framework;
using Ladeskab.Interfaces;
using Ladeskab;

namespace LadeskabUnitTest
{

    [TestFixture]
    public class TestChargeControl
    {

        private IUsbCharger _fakeUSBCharger;
        private IDisplay _fakeDisplay;
        private ChargeControl _uut;

        [SetUp]
        public void Setup()
        {
            _fakeUSBCharger = Substitute.For<IUsbCharger>();
            _fakeDisplay = Substitute.For<IDisplay>();

            _uut = new ChargeControl(_fakeUSBCharger, _fakeDisplay);
        }


        //SKAL TJEKKES
        [Test]
        public void NewCurrentDetected_Current_LessThanOrEquals_0()
        {
            _fakeUSBCharger.CurrentValue.Returns(0);

            _fakeDisplay.DidNotReceive().PrintConnectionError();
            
        }

        [TestCase(0.1)]
        [TestCase(1.1)]
        [TestCase(2.2)]
        [TestCase(3.3)]
        [TestCase(4.4)]
        [TestCase(5.0)]
        [Test]
        public void NewCurrentDetected_Current_GreaterThan0_and_LessThanOrEqual5(double curr)
        {
            _fakeUSBCharger.CurrentValue.Returns(curr);
            _fakeDisplay.Received().PrintFullyCharged();
        }

        [TestCase(5.1)]
        [TestCase(10.2)]
        [TestCase(25.3)]
        [TestCase(50.4)]
        [TestCase(100.5)]
        [TestCase(200.6)]
        [TestCase(400.7)]
        [TestCase(500.0)]
        [Test]
        public void NewCurrentDetected_Current_GreaterThan5_and_LessThanOrEqual500(double curr)
        {
            _fakeUSBCharger.CurrentValue.Returns(curr);
            _fakeDisplay.Received().PrintCurrentlyCharging();
        }


        [TestCase(500.1)]
        [TestCase(double.MaxValue)]
        [Test]
        public void NewCurrentDetected_Current_GreaterThan500(double curr)
        {
            _fakeUSBCharger.CurrentValue.Returns(curr);
            _fakeDisplay.Received().PrintOverchargeError();
            _uut.Received().StopCharge();
        }


        [Test]
        public void SimulatePhoneHasBeenConnected_RecievesTrueArgument()
        {
            _uut.SimulatePhoneConnected(true);
            Assert.That(_uut.Connected, Is.EqualTo(true));
        }

        [Test]
        public void SimulatePhoneHasBeenConnected_RecievesFalseArgument()
        {
            _uut.SimulatePhoneConnected(false);
            Assert.That(_uut.Connected, Is.EqualTo(false));
        }
    }
}

