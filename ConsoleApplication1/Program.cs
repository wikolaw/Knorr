using System;
using Knorr.Core;
using Knorr.Core.FileDb;
using Knorr.Core.ViewModels;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var qlikViewFiles = QlikViewObjectFactory.Create(@"C:\Users\Wiko\Documents\visual studio 2010\Projects\ConsoleApplication1\Knorr.Tests\TestFiles\");
            var db = new FileDB<QlikViewFile>(@"C:\Temp\");
            var dbNodes = new FileDB<Node>(@"C:\Temp\");
            var dbEdges = new FileDB<Edge>(@"C:\Temp\");

            db.DeleteAll();
            dbNodes.DeleteAll();
            dbEdges.DeleteAll();

            var nodes = NodeFactory.CreateNodes(qlikViewFiles);
            foreach (var node in nodes)
                dbNodes.Create(node);

            var edges = NodeFactory.CreateEdges(qlikViewFiles);
            foreach (var edge in edges)
                dbEdges.Create(edge);
            
           
            foreach (var qlikViewFile in qlikViewFiles)
                db.Create(qlikViewFile);
            

            var qlikviewObjects = db.Read();

            foreach (var qlikviewObject in qlikviewObjects)
            {
                Console.WriteLine("Filename " + qlikviewObject.FileInfo.FullName);
                foreach (var source in qlikviewObject.Script.Sources)
                {
                    Console.WriteLine("  Source " + source);
                }

                foreach (var target in qlikviewObject.Script.Targets)
                {
                    Console.WriteLine("  Target " + target);
                }
            }

            Console.Read();   
       }


    }
}
