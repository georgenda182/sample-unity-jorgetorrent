using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;

namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public abstract class Dialog : ServicesConsumer
    {
        public abstract void Install(ServiceLocator serviceLocator);
        public abstract void Show();
        public abstract void Close();
    }
}