using UnityEngine;

namespace _SampleJorgeTorrent.Code.HealthSystem
{
    [RequireComponent(typeof(Collider))]
    public class Health : MonoBehaviour, Damageable
    {
        public delegate void HealthCallback();
        public event HealthCallback OnDamaged;
        public event HealthCallback OnKilled;

        [SerializeField] private int _healthPoints = 100;
        private Collider _damageableVolume;

        private void Start()
        {
            _damageableVolume = GetComponent<Collider>();
        }

        public void TakeDamage(int damageAmount)
        {
            _healthPoints -= damageAmount;
            ManageEventsAfterDamageTaken();
        }

        public void SetVulnerability(bool mustBeVulnerable)
        {
            _damageableVolume.enabled = mustBeVulnerable;
        }

        private void ManageEventsAfterDamageTaken()
        {
            if (_healthPoints <= 0)
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