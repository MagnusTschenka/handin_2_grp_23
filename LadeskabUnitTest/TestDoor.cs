using NUnit.Framework;
using Ladeskab;
using Ladeskab.Interfaces;

namespace LadeskabUnitTest
{

    [TestFixture]
    public class TestsDoor
    {
        private Door _uut;
        private DoorChangedEventArgs _uutRecievedEventArgs;

        [SetUp]
        public void Setup()
        {
            _uutRecievedEventArgs = null;

            _uut = new Door();
            _uut.SetDoorStatus(false);

            _uut.DoorChangedEvent += (o, args) => { _uutRecievedEventArgs = args; };
        }

        [Test]
        public void TestLockDoorFunction()
        {
            _uut.LockDoor();
            Assert.That(_uut.GetDoorLockedStatus(), Is.True);
        }
        [Test]
        public void TestUnLockDoorFunction()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.GetDoorLockedStatus(), Is.False);
        }
        [Test]
        public void TestSimulateDoorBeingOpened()
        {
            _uut.simulateDoorBeingOpened();
            Assert.That(_uut.GetDoorStatus(), Is.True);
        }
        [Test]
        public void TestSimulateDoorBeingClosed()
        {
            _uut.simulateDoorBeingClosed();
            Assert.That(_uut.GetDoorStatus(), Is.False);
        }

        //test af events uden Nsubstitute
        [Test]
        public void TestSetDoorStatusEvent_Fired()
        {
            _uut.SetDoorStatus(true);
            Assert.That(_uutRecievedEventArgs, Is.Not.Null);
        }
        [Test]
        public void TestSetDoorStatusEvent_CorrectDoorStatus()
        {
            _uut.SetDoorStatus(true);
            Assert.That(_uutRecievedEventArgs.DoorStatus, Is.True);
        }
        [Test]
        public void TestSetDoorStatusEvent_DoorStatus()
        {
            _uut.SetDoorStatus(true);
            _uut.SetDoorStatus(false);
            Assert.That(_uutRecievedEventArgs.DoorStatus, Is.False);
        }
        
    }
}