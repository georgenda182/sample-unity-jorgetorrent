using _SampleJorgeTorrent.Code.UI;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;

namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public class DialogsContext : ServicesConsumer
    {
        private GameInputControls _gameInputControls;
        private DialogBars _dialogBars;

        public void Install(ServiceLocator dialogServiceLocator)
        {
            _gameInputControls = dialogServiceLocator.GetService<GameInputControls>();
            _dialogBars = dialogServiceLocator.GetService<DialogBars>();
        }

        public void Mount()
        {
            _dialogBars.Display();
            _gameInputControls.Menus.Enable();
            _gameInputControls.Player.Disable();
        }

        public void Unmount()
        {
            _dialogBars.Hide();
            _gameInputControls.Menus.Disable();
            _gameInputControls.Player.Enable();
        }
    }
}