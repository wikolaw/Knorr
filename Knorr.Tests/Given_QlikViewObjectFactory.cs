using System.Linq;
using Knorr.Core;
using NUnit.Framework;

namespace Knorr.Tests
{
    [TestFixture]
    public class Given_QlikViewObjectFactory
    {
        [Test]
        public void should_find_some_files()
        {
            Assert.That(QlikViewObjectFactory.Create(@"D:\Dokument\Private\").Count(), Is.GreaterThan(1));
        }
        [Test]
        public void should_not_find_some_files()
        {
            Assert.That(QlikViewObjectFactory.Create(@"c:\Temp").Count(), Is.EqualTo(0));
        }

    }
}
