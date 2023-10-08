using _SampleJorgeTorrent.Code.UI;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public abstract class DialogsDispatcher : MonoBehaviour, ServicesConsumer
    {
        [SerializeField] private DialogBars _dialogBars;
        [SerializeField] private DialogSpeechBubble _dialogSpeechBubble;

        protected Queue<Dialog> _dialogs;

        private ServiceLocator _dialogsServiceLocator;
        private GameInputControls _gameInputControls;
        private DialogsContext _dialogsContext;

        private Dialog _previousDialog;
        private Dialog _currentDialog;

        public void Install(ServiceLocator globalServiceLocator)
        {
            RegisterServices(globalServiceLocator);
            InitializeContext();
            InitializeDialogs();
            StartDialogs();
        }

        private void RegisterServices(ServiceLocator globalServiceLocator)
        {
            _dialogsServiceLocator = new ServiceLocator();

            _gameInputControls = globalServiceLocator.GetService<GameInputControls>();
            _gameInputControls.Menus.Confirm.performed += context => DispatchDialog();
            _dialogsServiceLocator.RegisterService(_gameInputControls);

            _dialogsServiceLocator.RegisterService(_dialogBars);

            _dialogsServiceLocator.RegisterService(_dialogSpeechBubble);

            Camera camera = globalServiceLocator.GetService<Camera>();
            _dialogsServiceLocator.RegisterService(camera);
        }

        private void InitializeContext()
        {
            _dialogsContext = new DialogsContext();
            _dialogsContext.Install(_dialogsServiceLocator);
        }

        private void InitializeDialogs()
        {
            _dialogs = new Queue<Dialog>();
            DefineDialogs();
            foreach (Dialog dialog in _dialogs)
            {
                dialog.Install(_dialogsServiceLocator);
            }
        }

        protected abstract void DefineDialogs();

        private void StartDialogs()
        {
            _dialogsContext.Mount();
            DispatchDialog();
        }

        private void DispatchDialog()
        {
            bool anyDialogRemaining = _dialogs.Count > 0;
            if (anyDialogRemaining)
            {
                _previousDialog = _currentDialog;
                _currentDialog = _dialogs.Dequeue();

                _previousDialog?.Close();
                _currentDialog?.Show();
            }
            else
            {
                _currentDialog.Close();
                _dialogsContext.Unmount();
            }
        }
    }
}