using System.IO;
using System.Linq;
using Knorr.Core;
using NUnit.Framework;

namespace Knorr.Tests
{
    [TestFixture]
    public class Given_ExtractSalesData
    {
        private Script _script;
        private FileInfo _file;

        [SetUp]
        public void setup()
        {
            _file = new FileInfo(@"~\..\..\..\TestFiles\1_Extract\Extract SalesData.qvw");
            _script = ScriptFactory.Create(_file);
        }
        [Test]
        public void should_find_file()
        {
            Assert.True(_file.Exists);
        }
        [Test]
        public void should_find_several_sources()
        {
            Assert.That(_script.Sources.Count(), Is.EqualTo(14));
        }

        [Test]
        public void should_find_several_targets()
        {
            Assert.That(_script.Targets.Count(), Is.EqualTo(14));
        }
    }
}
