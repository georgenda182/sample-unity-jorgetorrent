using _SampleJorgeTorrent.Code.LoadingProcess;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _SampleJorgeTorrent.Code.UI
{
    public class LoadingProgressVisualizerImpl : MonoBehaviour, LoadingProgressVisualizer
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private CanvasGroup _canvasGroup;

        public event LoadingProgressVisualizer.LoadingProgressVisualizerCallback OnShown;
        public event LoadingProgressVisualizer.LoadingProgressVisualizerCallback OnHidden;

        public void Show()
        {
            gameObject.SetActive(true);
            Tween showTween = _canvasGroup.DOFade(1, 0.5f).Pause();
            showTween.onComplete = () =>
            {
                OnShown?.Invoke();
            };
            showTween.Play();
        }

        public void Hide()
        {
            Tween hideTween = _canvasGroup.DOFade(0, 0.5f).Pause();
            hideTween.SetDelay(1);
            hideTween.onComplete = () =>
            {
                gameObject.SetActive(false);
                OnHidden?.Invoke();
            };
            hideTween.Play();
        }

        public void SetProgress(float progress)
        {
            _progressBar.fillAmount = progress;
        }
    }
}