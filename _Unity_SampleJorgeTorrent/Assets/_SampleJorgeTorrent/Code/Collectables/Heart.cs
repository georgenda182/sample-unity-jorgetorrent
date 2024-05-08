using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ObjectPoolPattern;
using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using System.Collections;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Collectables
{
    public class Heart : Collectable, PooledObject
    {
        [Header("Spawning")]
        [SerializeField] private GameObject _particlesOnSpawnPrefab;
        [SerializeField] private float _delayBeforeAppear = 0.2f;

        [Header("Health")]
        [SerializeField] private IntProperty _playerHealth;
        [SerializeField] private int _healthPointsToAdd = 15;

        private Renderer _renderer;
        private GameObject _particlesOnSpawn;
        private ObjectPool<Heart> _pool;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _particlesOnSpawn = Instantiate(_particlesOnSpawnPrefab);
        }

        private void DrawAppearEffect()
        {
            _renderer.enabled = false;

            _particlesOnSpawn.SetActive(true);
            _particlesOnSpawn.transform.position = _originPosition;
            StartCoroutine(DeactivateOnSpawnParticlesAfterDelay());

            StartCoroutine(AppearAfterDelay());
        }

        private IEnumerator DeactivateOnSpawnParticlesAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            _particlesOnSpawn.SetActive(false);
        }

        private IEnumerator AppearAfterDelay()
        {
            yield return new WaitForSeconds(_delayBeforeAppear);
            _renderer.enabled = true;
        }

        protected override void Collect()
        {
            _playerHealth.Property.Value += _healthPointsToAdd;
        }

        protected override void Destroy()
        {
            _pool.RecycleObject(this);
        }

        void PooledObject.OnTakenFromPool()
        {}

        void PooledObject.OnLeftOnPool()
        {}

        void PooledObject.StorePool<T>(ObjectPool<T> pool)
        {
            _pool = pool as ObjectPool<Heart>;
        }

        public void Spawn(Vector3 position)
        {
            _originPosition = position;
            transform.position = _originPosition;
            DrawAppearEffect();
        }
    }
}