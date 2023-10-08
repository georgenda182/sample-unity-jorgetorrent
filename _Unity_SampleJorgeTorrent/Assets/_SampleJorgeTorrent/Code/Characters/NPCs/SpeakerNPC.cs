using _SampleJorgeTorrent.Code.DialogSystem;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.NPCs
{
    [RequireComponent(typeof(SphereCollider))]
    public class SpeakerNPC : DialogsDispatcher
    {
        [SerializeField] private string[] _texts;

        private bool _canSpeak;

        protected override void DefineDialogs()
        {
            foreach (string text in _texts)
            {
                _dialogs.Add(new DialogWithText(text));
            }
            SubscribeDialogPerformanceToInput();
        }

        private void SubscribeDialogPerformanceToInput()
        {
            _gameInputControls.Player.Interact.performed += context => StartDialogsIfCanSpeak();
        }

        private void StartDialogsIfCanSpeak()
        {
            if (_canSpeak)
            {
                StartDialogs();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _canSpeak = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _canSpeak = false;
        }
    }
}
