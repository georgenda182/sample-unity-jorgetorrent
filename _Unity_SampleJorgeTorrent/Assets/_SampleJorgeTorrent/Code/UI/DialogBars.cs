using DG.Tweening;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.UI
{
    public class DialogBars : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _bars;

        public void Display()
        {
            _bars.DOFade(1, 0.5f);
        }

        public void Hide()
        {
            _bars.DOFade(0, 0.5f);
        }
    }
}