using _SampleJorgeTorrent.Code.DialogSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _SampleJorgeTorrent.Code.Characters.NPCs
{
    [RequireComponent(typeof(SphereCollider))]
    public class SpeakerNPC : DialogsDispatcher
    {
        [SerializeField] private Image _interactionMark;
        [SerializeField] private string[] _texts;

        private Tween _interactionMarkVisibilityTween;

        private bool _canSpeak;
        private bool _interactionMarkVisibilityTweenPlaying;

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

        private void Update()
        {
            if (_canSpeak)
            {

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _canSpeak = true;
            ShowInteractionMark();
        }

        private void ShowInteractionMark()
        {
            if (_interactionMarkVisibilityTweenPlaying)
            {
                _interactionMarkVisibilityTween.Complete(false);
            }
            _interactionMarkVisibilityTween = _interactionMark.DOFade(1, 0.35f);
            _interactionMarkVisibilityTween.onPlay = delegate
            {
                _interactionMarkVisibilityTweenPlaying = true;
                _interactionMark.gameObject.SetActive(true);
            };
            _interactionMarkVisibilityTween.onComplete = delegate
            {
                _interactionMarkVisibilityTweenPlaying = false;
            };
        }

        private void OnTriggerExit(Collider other)
        {
            _canSpeak = false;
            HideInteractionMark();
        }

        private void HideInteractionMark()
        {
            if (_interactionMarkVisibilityTweenPlaying)
            {
                _interactionMarkVisibilityTween.Complete(false);
            }
            _interactionMarkVisibilityTween = _interactionMark.DOFade(0, 0.35f);
            _interactionMarkVisibilityTween.onPlay = delegate
            {
                _interactionMarkVisibilityTweenPlaying = true;
            };
            _interactionMarkVisibilityTween.onComplete = delegate
            {
                _interactionMarkVisibilityTweenPlaying = false;
                _interactionMark.gameObject.SetActive(false);
            };
        }
    }
}
