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

        private List<Dialog> _dialogs;
        private int _currentDialogIndex = 0;

        protected ServiceLocator _globalServiceLocator;
        protected ServiceLocator _dialogsServiceLocator;
        protected GameInputControls _gameInputControls;
        private DialogsContext _dialogsContext;

        private Dialog _previousDialog;
        private Dialog _currentDialog;

        public void Install(ServiceLocator globalServiceLocator)
        {
            RegisterServices(globalServiceLocator);
            InitializeContext();
            InitializeDialogs();
        }

        private void RegisterServices(ServiceLocator globalServiceLocator)
        {
            _globalServiceLocator = globalServiceLocator;

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
            _dialogs = new List<Dialog>();
            DefineDialogs();
            foreach (Dialog dialog in _dialogs)
            {
                dialog.Install(_dialogsServiceLocator);
            }
        }

        protected abstract void DefineDialogs();

        protected void AddDialog(Dialog newDialog)
        {
            _dialogs.Add(newDialog);
            newDialog.Install(_dialogsServiceLocator);
        }

        protected void StartDialogs()
        {
            _dialogsContext.Mount();
            DispatchDialog();
        }

        private void DispatchDialog()
        {
            bool anyDialogRemaining = _currentDialogIndex < _dialogs.Count;
            if (anyDialogRemaining)
            {
                _previousDialog = _currentDialog;
                _currentDialog = _dialogs[_currentDialogIndex];

                _previousDialog?.Close();
                _currentDialog?.Show();

                _currentDialogIndex++;
            }
            else
            {
                _currentDialog.Close();
                _dialogsContext.Unmount();
                Reset();
            }
        }

        private void Reset()
        {
            _currentDialogIndex = 0;
        }
    }
}