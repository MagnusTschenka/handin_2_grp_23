using System;
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

        [SetUp]
        public void Setup()
        {
            _fakeRFIDRead = Substitute.For<IRFIDReader>();
            _fakeDoor = Substitute.For<IDoor>();
            _uut = new StationControl(_fakeDoor, _fakeRFIDRead);
        }

        //void TestDoorOpenedCorrect()
        //{
        //    _uut.DoorOpened();
        //    Assert.That(uut_.LadeskabState, Is.EqualTo();
        //}


        //skal sikkert bare slettes
        //[TestCase(true)]
        //[TestCase(false)]

        //void TestDoorOpenedCorrect(bool newDoorStatus)
        //{
        //    _fakeDoor.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorStatus = newDoorStatus });
        //    Assert.That(_uut., Is.EqualTo(newDoorStatus));

        //}
    }
}

