using System;
using System.IO;
using NUnit.Framework;
using NSubstitute;
using Ladeskab;
using Ladeskab.Interfaces;

namespace LadeskabUnitTest
{
    public class TestStationContol
    {
        StationControl _uut;
        IRFIDReader _fakeRFIDRead;
        IDoor _fakeDoor;
        IChargeControl _fakeChargeControl;
        IDisplay _fakeDisplay;

        [SetUp]
        public void Setup()
        {
            _fakeRFIDRead = Substitute.For<IRFIDReader>();
            _fakeDoor = Substitute.For<IDoor>();
            _fakeChargeControl = Substitute.For<IChargeControl>();
            _fakeDisplay = Substitute.For<IDisplay>();


            _uut = new StationControl(_fakeDoor, _fakeRFIDRead, _fakeDisplay, _fakeChargeControl);
        }
        [Test]
        void TestDoorOpenedCorrect()
        {
           //test af doorchanged event == false
            _fakeDoor.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs() {DoorStatus = false});
            
            //assert 
            _fakeDisplay.Received().PrintConnectPhone(); //received er en assert i sig selv
        }
        [Test]
        void TestDoorClosedCorrect()
        {
            //test af doorchanged event == true
            _fakeDoor.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs() { DoorStatus = false });
            _fakeDoor.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs() { DoorStatus = true });

            //assert 
            _fakeDisplay.Received().PrintLoadRFID(); 
        }


        [Test]
        void TestRfidDetectedAvailableLockDoor()
        {
            _fakeChargeControl.SimulatePhoneConnected(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() {Id = 69});
            _fakeDoor.Received().LockDoor();
        }

        [Test]
        void TestRfidDetectedAvailableStartCharge()
        {
            _fakeChargeControl.SimulatePhoneConnected(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });
            _fakeDoor.Received().LockDoor();
            _fakeChargeControl.Received().StartCharge();

        }

        [Test]
        void TestRfidDetectedAvailableAppendLogfile()
        {
            _fakeChargeControl.SimulatePhoneConnected(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });
            _fakeDoor.Received().LockDoor();
            _fakeChargeControl.Received().StartCharge();

            //hvordan tester man for om et dokument er oprettet

            //og hvordan tester man for hvad der findes i et dokument


        
        }


        void TestRfidDetectedAvailableLockedID()
        {
            _fakeChargeControl.SimulatePhoneConnected(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });

            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 6969 });

            var expected = "Forkert RFID tag\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());

        }

        void TestRfidDetectedAvailableOldEqualNewIDStopCharge()
        {
            _fakeChargeControl.SimulatePhoneConnected(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });

            _fakeChargeControl.Received().StopCharge();

        }

        void TestRfidDetectedAvailableOldEqualNewIDUnlock()
        {
            _fakeChargeControl.SimulatePhoneConnected(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });

            _fakeChargeControl.Received().StopCharge();
            _fakeDoor.Received().UnlockDoor();

        }

        void TestRfidDetectedAvailableOldEqualNewIDLogFile()
        {
            _fakeChargeControl.SimulatePhoneConnected(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });

            _fakeChargeControl.Received().StopCharge();
            _fakeDoor.Received().UnlockDoor();

        }

      
    }
}

