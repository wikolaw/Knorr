using System.IO;
using System.Linq;
using Knorr.Core;
using NUnit.Framework;

namespace Knorr.Tests.Given_Script_From
{
    //hej
    public abstract class Given_Script_From
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
    public class when_simple : Given_Script_From
    {
        public override string Script
        {
            get
            {
                return "SQL SELECT * FROM tblDragListBox; store tblDragListBox into tblDragListBox.qvd; drop table tblDragListBox;";
            }
        }

        [Test]
        public void should_be_tblDragListBox()
        {
            Assert.That(_script.Sources.ElementAt(0), Is.EqualTo("tblDragListBox"));
        }


    }

    [TestFixture]
    public class when_using_where_clause : Given_Script_From
    {
        public override string Script
        {
            get
            {
                return "SQL SELECT * FROM tblDragListBox where pkeyCustomerID = 'ALFKI'; store tblDragListBox into tblDragListBox.qvd; drop table tblDragListBox;";
            }
        }

        [Test]
        public void should_be_tblDragListBox()
        {
            Assert.That(_script.Sources.ElementAt(0), Is.EqualTo("tblDragListBox"));
        }


    }

    [TestFixture]
    public class when_table_name_has_spaces : Given_Script_From
    {
        public override string Script
        {
            get
            {
                return "SQL SELECT * FROM `Table 1`;";
            }
        }

        [Test]
        public void should_be_Table_1()
        {
            Assert.That(_script.Sources.ElementAt(0), Is.EqualTo("Table 1"));
        }


    }
}