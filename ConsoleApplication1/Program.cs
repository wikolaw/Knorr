using System;
using Knorr.Core;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var clickViewObjects = QlikViewObjectFactory.Create(@"C:\Users\Wiko\Documents\visual studio 2010\Projects\ConsoleApplication1\Knorr.Tests\TestFiles\");

            foreach (var clickViewObject in clickViewObjects)
            {
                Console.WriteLine(@"FILE:" + clickViewObject.FileInfo.Name);
                foreach (var source in clickViewObject.Script.Sources)
                {
                    Console.WriteLine("  SOURCE:" + source);
                    
                }

                foreach (var target in clickViewObject.Script.Targets)
                {
                    Console.WriteLine("  TARGET:" + target);

                }

            }
          
            Console.Read();   
       }


    }
}
