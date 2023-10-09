using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _SampleJorgeTorrent.Code.UI
{
    [RequireComponent(typeof(Image))]
    public class InteractionMark : MonoBehaviour, ServicesConsumer
    {
        [SerializeField] private float _fadeDuration = 0.2f;
        private Image _mark;
        private Camera _camera;
        private Transform _target;

        private Tween _visibilityTween;
        private bool _tweenIsPlaying;

        public void Install(ServiceLocator globalServiceLocator)
        {
            _camera = globalServiceLocator.GetService<Camera>();
            _mark = GetComponent<Image>();
        }

        public void Show(Transform target)
        {
            _target = target;
            StopPreviousTween();
            PlayShowTween();
        }

        private void StopPreviousTween()
        {
            if (_tweenIsPlaying)
            {
                _visibilityTween.Complete(false);
            }
        }

        private void PlayShowTween()
        {
            _visibilityTween = _mark.DOFade(1, _fadeDuration);
            _visibilityTween.onPlay = delegate
            {
                _tweenIsPlaying = true;
                SetPositionInTarget();
                gameObject.SetActive(true);
            };
            _visibilityTween.onComplete = delegate
            {
                _tweenIsPlaying = false;
            };
        }

        private void SetPositionInTarget()
        {
            transform.position = _camera.WorldToScreenPoint(_target.position);
        }

        public void Hide()
        {
            StopPreviousTween();
            PlayHideTween();
        }

        private void PlayHideTween()
        {
            _visibilityTween = _mark.DOFade(0, _fadeDuration);
            _visibilityTween.onPlay = delegate
            {
                _tweenIsPlaying = true;
            };
            _visibilityTween.onComplete = delegate
            {
                _tweenIsPlaying = false;
                gameObject.SetActive(false);
            };
        }

        private void Update()
        {
            SetPositionInTarget();
        }
    }
}