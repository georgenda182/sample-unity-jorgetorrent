using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.HealthSystem
{
    [RequireComponent(typeof(Collider))]
    public class Health : MonoBehaviour, Damageable
    {
        public delegate void HealthCallback();
        public event HealthCallback OnDamaged;
        public event HealthCallback OnKilled;

        [SerializeField] private int _initialHealthPoints = 100;
        [SerializeField] private IntProperty _healthPoints;
        private int HealthPoints
        {
            get => _healthPoints.Property.Value;
            set => _healthPoints.Property.Value = value;
        }

        private Collider _damageableVolume;

        public void Initialize()
        {
            _damageableVolume = GetComponent<Collider>();
            HealthPoints = _initialHealthPoints;
        }

        public void TakeDamage(int damageAmount)
        {
            _healthPoints.Property.Value -= damageAmount;
            ManageEventsAfterDamageTaken();
        }

        public void SetVulnerability(bool mustBeVulnerable)
        {
            _damageableVolume.enabled = mustBeVulnerable;
        }

        private void ManageEventsAfterDamageTaken()
        {
            if (HealthPoints <= 0)
            {
                _damageableVolume.enabled = false;
                CallEventIfNotNull(OnKilled);
            }
            else
            {
                CallEventIfNotNull(OnDamaged);
            }
        }

        private void CallEventIfNotNull(HealthCallback eventToBeCalled)
        {
            if (eventToBeCalled != null)
            {
                eventToBeCalled();
            }
        }
    }
}