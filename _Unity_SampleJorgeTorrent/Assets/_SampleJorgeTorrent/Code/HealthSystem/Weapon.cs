using UnityEngine;

namespace _SampleJorgeTorrent.Code.HealthSystem
{
    [RequireComponent(typeof(Collider))]
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected int _healthPointsToSubtract = 25;

        protected Collider _hitVolume;

        private void Start()
        {
            _hitVolume = GetComponent<Collider>();
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