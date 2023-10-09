using _SampleJorgeTorrent.Code.DialogSystem;
using _SampleJorgeTorrent.Code.UI;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.NPCs
{
    [RequireComponent(typeof(SphereCollider))]
    public class SpeakerNPC : DialogsDispatcher
    {
        [SerializeField] private Transform _interactionMarkTarget;
        [SerializeField] private string[] _texts;

        private InteractionMark _interactionMark;
        private bool _canSpeak;

        protected override void DefineDialogs()
        {
            foreach (string text in _texts)
            {
                AddDialog(new DialogWithText(text));
            }
            _interactionMark = _globalServiceLocator.GetService<InteractionMark>();
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
            if (_canSpeak)
            {
                return;
            }
            _canSpeak = true;
            _interactionMark.Show(_interactionMarkTarget);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_canSpeak)
            {
                return;
            }
            _canSpeak = false;
            _interactionMark.Hide();
        }
    }
}
