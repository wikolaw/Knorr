using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Knorr.Core
{
    public static class QlikViewObjectFactory
    {
        public static IEnumerable<QlikViewFile> Create(string directoryRoot)
        {
            var startRoot = new DirectoryInfo(directoryRoot);
            var files = startRoot.GetFiles("*.qvw", SearchOption.AllDirectories);
            return files.Select(file => new QlikViewFile
                                            {
                                                Id = Guid.NewGuid().ToString(),
                                                Script = ScriptFactory.Create(file),
                                                FileInfo = file
                                            }).ToList();
        
        }
    }
}