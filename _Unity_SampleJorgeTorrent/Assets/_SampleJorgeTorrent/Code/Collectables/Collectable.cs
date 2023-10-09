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
        private Vector3 _originalPosition;

        private void Start()
        {
            _originalPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            bool hasCollidedWithPlayer = other.gameObject.layer == LayerMask.NameToLayer("Player");
            if (hasCollidedWithPlayer)
            {
                Collect();
                SpawnParticles();
                Destroy(gameObject);
            }
        }

        protected abstract void Collect();

        private void SpawnParticles()
        {
            GameObject particlesInstance = Instantiate(_particlesAfterCollection);
            particlesInstance.transform.position = _originalPosition;
            Destroy(particlesInstance, 1f);
        }

        private void Update()
        {
            _currentYPosition = _originalPosition.y + _differenceFromInitialYPosition * Mathf.Sin(Time.time * _verticalMovementVelocity);
            transform.position = new Vector3(_originalPosition.x, _currentYPosition, _originalPosition.z);

            transform.Rotate(Vector3.up * _rotationVelocity * Time.deltaTime);
        }
    }
}