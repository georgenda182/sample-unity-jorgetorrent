using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using DG.Tweening;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _SampleJorgeTorrent.Code.UI
{
    public class HealthVisualizer : MonoBehaviour
    {
        [SerializeField] private IntProperty _health;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _healthBarShadow;

        private int _currentHealthPoints;
        private int _initialHealthPoints;

        private IDisposable _subscriptionToHealthChanges;

        public void Initialize()
        {
            _subscriptionToHealthChanges = _health.Property.Subscribe(VisualizeHealthPoints);
            _initialHealthPoints = _health.Property.Value;
            _currentHealthPoints = _health.Property.Value;
        }

        private void VisualizeHealthPoints(int healthPoints)
        {
            bool healthHasIncreased = _currentHealthPoints < healthPoints;

            _currentHealthPoints = healthPoints;
            float fillAmount = CalculateHealthBarFillAmount();
            if (healthHasIncreased)
            {
                _healthBarShadow.fillAmount = fillAmount;
                _healthBar.DOFillAmount(fillAmount, 0.4f).SetDelay(0.5f);
            }
            else
            {
                _healthBar.fillAmount = fillAmount;
                _healthBarShadow.DOFillAmount(fillAmount, 0.4f).SetDelay(0.5f);
            }
        }

        private float CalculateHealthBarFillAmount()
        {
            return (float) _currentHealthPoints / (float) _initialHealthPoints;
        }

        private void OnDestroy()
        {
            _subscriptionToHealthChanges.Dispose();
        }
    }
}