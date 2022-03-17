using NUnit.Framework;
using Ladeskab;

namespace LadeskabUnitTest
{

    [TestFixture]
    public class Tests
    {
        private Door _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Door(); 
        }

        [Test]
        public void TestLockDoorFunction()
        {
            _uut.LockDoor();
            Assert.That(_uut.GetDoorLockedStatus(), Is.True);
        }
        public void TestUnLockDoorFunction()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.GetDoorLockedStatus(), Is.False);
        }

        public void TestSimulateDoorBeingOpened()
        {
            _uut.simulateDoorBeingOpened();
            Assert.That(_uut.GetDoorStatus(), Is.True);
        }
        public void TestSimulateDoorBeingClosed()
        {
            _uut.simulateDoorBeingClosed();
            Assert.That(_uut.GetDoorStatus(), Is.False);
        }

        //mangler test for event???
    }
}