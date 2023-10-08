using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public class DialogSpeechBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _charactersPerSecond;

        private Tween _textDisplayTween;
        private bool _hidden = true;

        public void DisplayText(string textToShow)
        {
            ShowIfNot();
            float displayDuration = textToShow.Length / _charactersPerSecond;
            int currentCharactersDisplayed = 0;
            _textDisplayTween = DOTween.To(() => currentCharactersDisplayed, x => currentCharactersDisplayed = x, textToShow.Length, displayDuration);
            _textDisplayTween.SetEase(Ease.Linear);
            _textDisplayTween.onUpdate = delegate
            {
                _text.text = textToShow.Substring(0, currentCharactersDisplayed);
            };
            _textDisplayTween.onComplete = delegate
            {
                _text.text = textToShow;
            };
        }

        private void ShowIfNot()
        {
            if (_hidden)
            {
                gameObject.SetActive(true);
                _hidden = false;
            }
        }

        public void ForceFinish()
        {
            _textDisplayTween.Complete(true);
            HideIfNot();
        }

        private void HideIfNot()
        {
            if (!_hidden)
            {
                gameObject.SetActive(false);
                _hidden = true;
            }
        }
    }
}