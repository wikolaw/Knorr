using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Knorr.Core
{
   
    public class ScriptFactory
    {
       
        public static Script Create(FileInfo fileInfo )
        {
            var text = QlikViewApp.App.GetScript(fileInfo.FullName);
           
            return new Script
                             {
                                 Text = text,
                                 Sources = GetSources(text),
                                 Targets = GetTargets(text)
                             };
        }

        private static IEnumerable<string> GetTargets(string script)
        {
            var targets = Regex(@"into[\s]+([^\s][^;]*)")
                .Matches(script)
                .Cast<Match>()
                .Select(s => s.Groups[1].Value);


            var hasWhiteSpace = Regex(@"\[([^\]]+)");
            var hasNoWhiteSpace = Regex(@"[^\s]+");

            Func<string, string> cleanFormatSpecifikation = s => s.Replace("(qvd)", "").Replace("(txt)", "");
                                                                

            foreach (var target in targets)
            {
                if (hasWhiteSpace.IsMatch(target))
                    yield return cleanFormatSpecifikation(hasWhiteSpace.Match(target).Groups[1].Value);
                else
                    yield return cleanFormatSpecifikation(hasNoWhiteSpace.Match(target).Value);
            }


        }


        private static IEnumerable<string> GetSources(string script)
        {
            var sources = Regex(@"from[\s]+([^\s][^;]*)")
                .Matches(script)
                .Cast<Match>()
                .Select(s => s.Groups[1].Value);

            var hasWhiteSpace = Regex("`([^`]+)`");
            var hasNoWhiteSpace = Regex(@"[^\s]+");
            
            foreach (var source in sources)
            {
                if (hasWhiteSpace.IsMatch(source))
                    yield return hasWhiteSpace.Match(source).Groups[1].Value;
                else
                    yield return hasNoWhiteSpace.Match(source).Value;
            }
        }

        private static Regex Regex(string regexText)
        {
            return new Regex(regexText, RegexOptions.IgnoreCase
                                             | RegexOptions.Multiline
                                             | RegexOptions.IgnorePatternWhitespace
                                             | RegexOptions.Singleline
                                             | RegexOptions.Compiled);
        }
    }
}