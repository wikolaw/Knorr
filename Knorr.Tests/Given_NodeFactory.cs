using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Knorr.Core;
using NUnit.Framework;

namespace Knorr.Tests
{
    public abstract class Given_NodeFactory
    {
        protected IEnumerable<QlikViewFile> QlikViewFiles()
        {
            return new []
            {
                new QlikViewFile(){FileInfo = new FileInfo("file1"),Id="1", Script = new Script(){
                                                                                                Sources = new[]{"data.xls"},
                                                                                                Targets = new []{"order.qvd"}
                                                                                        }},
                new QlikViewFile(){FileInfo = new FileInfo("file2"),Id="2", Script = new Script(){
                                                                                                Sources = new[]{"order.qvd"},
                                                                                                Targets = new[]{"sales.qvd"}
                                                                                        }},
                new QlikViewFile(){FileInfo = new FileInfo("file3"),Id="3", Script = new Script(){
                                                                                                Sources = new[]{"order.qvd"}
                                                                                        }},
            };
        }
    }
    [TestFixture]
    public class when_CreateNodes : Given_NodeFactory
    {
        [Test]
        public void should_be_6_nodes()
        {
            Assert.That(NodeFactory.CreateNodes(QlikViewFiles()).Count(), Is.EqualTo(6));
        }
    }
    [TestFixture]
    public class when_CreateEdges : Given_NodeFactory
    {
        [Test]
        public void should_be_5_edges()
        {
            Assert.That(NodeFactory.CreateEdges(QlikViewFiles()).Count(), Is.EqualTo(5));
        }
    }
}
