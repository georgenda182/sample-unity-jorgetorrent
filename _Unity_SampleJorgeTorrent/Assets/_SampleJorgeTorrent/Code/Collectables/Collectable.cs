using UnityEngine;

namespace _SampleJorgeTorrent.Code.Collectables
{
    [RequireComponent(typeof(Collider))]
    public abstract class Collectable : MonoBehaviour
    {
        [SerializeField] private float _differenceFromInitialYPosition = 0.03f;
        [SerializeField] private float _verticalMovementVelocity = 5f;
        [SerializeField] private float _rotationVelocity = 180f;
        [SerializeField] private GameObject _particlesAfterCollection;

        private float _currentYPosition;
        protected Vector3 _originPosition;

        private void Start()
        {
            _originPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            bool hasCollidedWithPlayer = other.gameObject.layer == LayerMask.NameToLayer("Player");
            if (hasCollidedWithPlayer)
            {
                Collect();
                SpawnParticles();
                Destroy();
            }
        }

        protected abstract void Collect();
        protected abstract void Destroy();

        private void SpawnParticles()
        {
            GameObject particlesInstance = Instantiate(_particlesAfterCollection);
            particlesInstance.transform.position = transform.position;
            Destroy(particlesInstance, 1f);
        }

        private void Update()
        {
            _currentYPosition = _originPosition.y + _differenceFromInitialYPosition * Mathf.Sin(Time.time * _verticalMovementVelocity);
            transform.position = new Vector3(_originPosition.x, _currentYPosition, _originPosition.z);

            transform.Rotate(Vector3.up * _rotationVelocity * Time.deltaTime);
        }
    }
}