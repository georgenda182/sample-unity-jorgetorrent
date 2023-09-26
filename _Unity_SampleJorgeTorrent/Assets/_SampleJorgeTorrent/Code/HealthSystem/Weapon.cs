using UnityEngine;

namespace _SampleJorgeTorrent.Code.HealthSystem
{
    [RequireComponent(typeof(Collider))]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int _healthPointsToSubtract = 25;

        private Collider _hitVolume;

        private void Start()
        {
            _hitVolume = GetComponent<Collider>();
        }

        public void EnableHitVolume()
        {
            _hitVolume.enabled = true;
        }

        public void DisableHitVolume()
        {
            _hitVolume.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Damageable damageable = other.GetComponent<Damageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_healthPointsToSubtract);
            }
        }
    }
}