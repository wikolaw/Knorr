using QlikView;

namespace Knorr.Core
{

    public interface IQlikViewApplication
    {
        string GetScript(string fileName);
    }
    
    public class QlikViewApplication : IQlikViewApplication
    {
        private static Application _app;

        public string GetScript(string filename)
        {
            if(_app == null)
                _app =  new QlikView.Application();
           return _app.OpenDoc(filename).GetProperties().Script;

        }
    }

 

    public static class QlikViewApp
    {
        private static IQlikViewApplication _app;

        public static IQlikViewApplication App
        {
            get { return _app ?? (_app = new QlikViewApplication()); }
            set { _app = value; }
        }

    }
}
