using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.LoadingProcess;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _SampleJorgeTorrent.Code.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private float _appearFadeDuration = 0.1667f;
        [SerializeField] private Button _replayButton;

        private CanvasGroup _canvasGroup;

        public void Install(Health health)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;

            health.OnKilled += Show;

            _replayButton.onClick.AddListener(Replay);
        }

        private void Replay()
        {
            SceneLoader.Instance.LoadScene("BattleLevel");
        }

        private void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1f, _appearFadeDuration);
        }
    }
}