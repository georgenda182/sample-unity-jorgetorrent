using UnityEngine;

namespace _SampleJorgeTorrent.Code.HealthSystem
{
    [RequireComponent(typeof(Collider))]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int _healthPointsToSubtract = 25;

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