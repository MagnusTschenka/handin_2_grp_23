using System;
using NUnit.Framework;
using Ladeskab;


namespace LadeskabUnitTest
{
    [TestFixture]

    public class TestRFIDReader
    {
        RFIDReader _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new RFIDReader();
        }


        [Test]
        public void Test_RFID_Event()
        {
            var wasCalled = false;
            _uut.SetRFIDStatus += (o, e) => wasCalled = true;
        }

    }
}

