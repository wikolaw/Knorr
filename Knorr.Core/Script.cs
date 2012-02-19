using System.Collections.Generic;

namespace Knorr.Core
{
    public class Script
    {
        public string Text { get; set; }
        public IEnumerable<string> Sources { get; set; }  
        public IEnumerable<string> Targets { get; set; }  
    }
}