using Knorr.Core;

namespace Knorr.Tests
{
    public class mocked_qlikview : IQlikViewApplication
    {
        private readonly string _script;

        public mocked_qlikview(string script)
        {
            _script = script;
        }

        public string GetScript(string fileName)
        {
            return _script;
        }
    }
}