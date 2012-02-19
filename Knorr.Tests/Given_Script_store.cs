using System.IO;
using System.Linq;
using Knorr.Core;
using NUnit.Framework;

namespace Knorr.Tests.Given_Script_store
{
    public abstract class Given_Script_store
    {
        protected Script _script;

        [SetUp]
        public void setup()
        {
            QlikViewApp.App = new mocked_qlikview(Script);
            _script = ScriptFactory.Create(new FileInfo("c:\\notapath"));

        }

        public abstract string Script { get; }
    }

    [TestFixture]
    public class when_simple : Given_Script_store
    {
        public override string Script
        {
            get
            {
                return "store tblShippers into tblShippers.qvd;";
            }
        }

        [Test]
        public void should_be_tblShippers_qvd()
        {
            Assert.That(_script.Targets.ElementAt(0), Is.EqualTo("tblShippers.qvd"));
        }


    }

    [TestFixture]
    public class when_storing_as_txt : Given_Script_store
    {
        public override string Script
        {
            get
            {
                return "store tblType into tblType.txt(txt);";
            }
        }

        [Test]
        public void should_be_tblType_txt()
        {
            Assert.That(_script.Targets.ElementAt(0), Is.EqualTo("tblType.txt"));
        }


    }

    [TestFixture]
    public class when_format_spec_is_qvd : Given_Script_store
    {
        public override string Script
        {
            get
            {
                return "store tblType into tblType.qvd(qvd);";
            }
        }

        [Test]
        public void should_be_tblType_qvd()
        {
            Assert.That(_script.Targets.ElementAt(0), Is.EqualTo("tblType.qvd"));
        }


    }

    [TestFixture]
    public class when_filename_has_whitespace : Given_Script_store
    {
        public override string Script
        {
            get
            {
                return "store `Table 1` into [Table 1.qvd];";
            }
        }

        [Test]
        public void should_be_Table_1_qvd()
        {
            Assert.That(_script.Targets.ElementAt(0), Is.EqualTo("Table 1.qvd"));
        }
    }

    [TestFixture]
    public class when_filename_has_whitespace_and_format_spec_is_qvd : Given_Script_store
    {
        public override string Script
        {
            get
            {
                return "store `Table 1` into [Table 1.qvd](qvd);";
            }
        }

        [Test]
        public void should_be_Table_1_qvd()
        {
            Assert.That(_script.Targets.ElementAt(0), Is.EqualTo("Table 1.qvd"));
        }
    }

}