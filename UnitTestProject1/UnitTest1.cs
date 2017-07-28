using System;
using NUnit.Framework;
using FluentAssertions;
namespace UnitTestProject1
{
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            // count of tast  maindata should be 47566
        }

        [Test]
        public void TestMethod3()
        {
            int b = 3;
            b.Should().Be(3);
        }
    }
}
