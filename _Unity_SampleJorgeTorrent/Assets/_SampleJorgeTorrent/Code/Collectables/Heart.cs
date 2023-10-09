using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using System.Collections;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Collectables
{
    public class Heart : Collectable
    {
        [Header("Spawning")]
        [SerializeField] private GameObject _particlesOnSpawn;
        [SerializeField] private float _delayBeforeAppear = 0.2f;

        [Header("Health")]
        [SerializeField] private IntProperty _playerHealth;
        [SerializeField] private int _healthPointsToAdd = 15;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            DrawAppearEffect();
        }

        private void DrawAppearEffect()
        {
            _renderer.enabled = false;

            GameObject spawnParticlesInstance = Instantiate(_particlesOnSpawn);
            spawnParticlesInstance.transform.position = transform.position;
            Destroy(spawnParticlesInstance, 1f);

            StartCoroutine(AppearAfterDelay());
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
    }
}