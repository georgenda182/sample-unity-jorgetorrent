using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Services
{
    public class PlayerHeartSpawner : MonoBehaviour, ServicesConsumer
    {
        [SerializeField] private GameObject _heart;
        [SerializeField] private float _spawnMaxDistance = 5f;

        private Health _playerHealth;

        public void Install(ServiceLocator playerServiceLocator)
        {
            _playerHealth = playerServiceLocator.GetService<Health>();
            SubscribeSpawnToPlayerDamage();
        }

        private void SubscribeSpawnToPlayerDamage()
        {
            _playerHealth.OnDamaged += SpawnHeart;
        }

        private void SpawnHeart()
        {
            Vector3 heartPosition = transform.position + GetRandomSpawnPosition();
            Instantiate(_heart, heartPosition, Quaternion.identity);
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 randomOffsetDirection = new Vector3(Random.value, 0, Random.value);
            float randomOffsetDistance = Random.Range(-1f, 1f) * _spawnMaxDistance;
            return (randomOffsetDirection.normalized * randomOffsetDistance) + Vector3.up * 0.7f;
        }
    }
}