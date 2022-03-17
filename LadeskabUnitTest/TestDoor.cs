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
        public void Test1()
        {
            Assert.Pass();
            
        }
    }
}