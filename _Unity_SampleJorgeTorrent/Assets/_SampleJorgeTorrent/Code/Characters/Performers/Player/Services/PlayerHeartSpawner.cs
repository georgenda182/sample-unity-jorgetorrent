using _SampleJorgeTorrent.Code.Collectables;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ObjectPoolPattern;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Services
{
    public class PlayerHeartSpawner : MonoBehaviour, ServicesConsumer
    {
        [SerializeField] private Heart _heartPrefab;
        [SerializeField] private float _spawnMaxDistance = 5f;

        private ObjectPool<Heart> _heartPool;
        private Health _playerHealth;

        public void Install(ServiceLocator playerServiceLocator)
        {
            _playerHealth = playerServiceLocator.GetService<Health>();
            _heartPool = new ObjectPool<Heart>(_heartPrefab);
            SubscribeSpawnToPlayerDamage();
        }

        private void SubscribeSpawnToPlayerDamage()
        {
            _playerHealth.OnDamaged += SpawnHeart;
        }

        private void SpawnHeart()
        {
            Vector3 heartPosition = transform.position + GetRandomSpawnPosition();
            Heart heartInstance = _heartPool.GetObject();
            heartInstance.Spawn(heartPosition);
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 randomOffsetDirection = new Vector3(Random.value, 0, Random.value);
            float randomOffsetDistance = Random.Range(-1f, 1f) * _spawnMaxDistance;
            return (randomOffsetDirection.normalized * randomOffsetDistance) + Vector3.up * 0.7f;
        }
    }
}