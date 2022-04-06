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
        private StationControl _uut;
        private IRFIDReader _fakeRFIDRead;
        private IDoor _fakeDoor;
        private IChargeControl _fakeChargeControl;
        private IDisplay _fakeDisplay;
        private ILogFile _fakeLogFile;

        [SetUp]
        public void Setup()
        {
            _fakeRFIDRead = Substitute.For<IRFIDReader>();
            _fakeDoor = Substitute.For<IDoor>();
            _fakeChargeControl = Substitute.For<IChargeControl>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLogFile = Substitute.For<ILogFile>();

            _uut = new StationControl(_fakeDoor, _fakeRFIDRead, _fakeDisplay, _fakeChargeControl, _fakeLogFile);
        }
        [Test]
        public void TestDoorOpenedCorrect()
        {
           //test af doorchanged event == false
            _fakeDoor.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs() {DoorStatus = false});
            
            //assert 
            _fakeDisplay.Received(1).PrintConnectPhone(); //received er en assert i sig selv
        }

        [Test]
        public void TestDoorClosedCorrect()
        {
            //test af doorchanged event == true
            _fakeDoor.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs() { DoorStatus = false });
            _fakeDoor.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs() { DoorStatus = true });

            //assert 
            _fakeDisplay.Received(1).PrintLoadRFID(); 
        }

        [Test]
        public void TestRfidDetectedAvailableLockDoor()
        {
            //_uut.SetLadeskabsState(StationControl.LadeskabState.Available);
            _fakeChargeControl.Connected().Returns(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() {Id = 69});
            _fakeDoor.Received(1).LockDoor();
        }

        [Test]
        public void TestRfidDetectedAvailableLockDoorFalse()
        {
            //_uut.SetLadeskabsState(StationControl.LadeskabState.Available);
            _fakeChargeControl.Connected().Returns(false);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });
            _fakeDisplay.Received(1).PrintPhoneConnectionError();
        }

        [Test]
        public void TestRfidDetectedAvailableStartCharge()
        {
            //_uut.SetLadeskabsState(StationControl.LadeskabState.Available);
            _fakeChargeControl.Connected().Returns(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });
            _fakeDoor.Received(1).LockDoor();
            _fakeChargeControl.Received(1).StartCharge();
        }

        [Test]
        public void TestRfidDetectedAvailableLockedID()
        {
            _fakeChargeControl.Connected().Returns(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });

            //var expected = "Forkert RFID tag\r\n";
            //var stringWriter = new StringWriter();
            //Console.SetOut(stringWriter);

            


            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 6969 });
            _fakeDisplay.Received(1).PrintRFIDError();
            //assert
            //Assert.AreEqual(expected, stringWriter.ToString());
        }

        //test hvor vi tjekker at eventet virker og at det korrekte bliver appended til logfile (uden event, brug "appendtextlock()")
        //[Test]
        //public void TestRfidLogWhenLocked()
        //{
        //    if (File.Exists("logfile.txt"))
        //    {
        //        File.Delete("logfile.txt");
        //    }
        //    _fakeChargeControl.Connected = true;
        //    _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });

        //    string text;
        //    using(StreamReader reader = new StreamReader("logfile.txt"))
        //    {
        //        text = reader.ReadToEnd();
        //    }
        //    string test = DateTime.Now.ToString("MM/dd/yyyy HH:mm") + ": Skab låst med RFID: 69\r\n";
        //    Assert.AreEqual(test, text);
        //}

        ////denne test bruger vi metoden "appendtext..." da vi på den måde kun får 1 string indsat i loggen (nemmere at læse)
        //[Test]
        //public void TestRfidLogWhenUnlocked()
        //{
        //    if (File.Exists("logfile.txt"))
        //    {
        //        File.Delete("logfile.txt");
        //    }
        //    _uut.AppendTextUnlock(69);

        //    string text;
        //    using (StreamReader reader = new StreamReader("logfile.txt"))
        //    {
        //        text = reader.ReadToEnd();
        //    }
        //    string test = DateTime.Now.ToString("MM/dd/yyyy HH:mm") + ": Skab låst op med RFID: 69\r\n";
        //    Assert.AreEqual(test, text);
        //}

        [Test]
        public void TestRfidDetectedAvailableOldEqualNewIDUnlock()
        {
            _fakeChargeControl.Connected().Returns(true);
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });
            _fakeRFIDRead.RfidEventDetected += Raise.EventWith(new RFIDDetectedEventArgs() { Id = 69 });

            _fakeChargeControl.Received(1).StopCharge();
            _fakeDoor.Received(1).UnlockDoor();
        }
    }
}

