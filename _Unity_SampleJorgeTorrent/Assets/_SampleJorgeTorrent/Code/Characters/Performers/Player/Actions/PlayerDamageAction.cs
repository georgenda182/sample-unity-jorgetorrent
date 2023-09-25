using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class PlayerDamageAction : PerformerAction
    {
        [SerializeField] private float _impulseForce = 300f;

        private Health _playerHealth;
        private Transform _playerTransform;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        private Transform _enemyTransform;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _playerHealth = performerServiceLocator.GetService<Health>();
            _playerTransform = performerServiceLocator.GetService<Transform>();
            _playerRigidbody = performerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = performerServiceLocator.GetService<Animator>();
            _enemyTransform = performerServiceLocator.GetService<EnemyTransformWrapper>().Value;
        }

        protected override void DefinePerformanceConditions()
        {
            _playerHealth.OnDamaged += PerformIfAllowed;
        }

        protected override void Perform()
        {
            _playerHealth.enabled = false;
            _playerAnimator.SetTrigger("Damaged");

            Vector3 damageImpulseForward = Vector3.Normalize(_playerTransform.position - _enemyTransform.position);
            damageImpulseForward.y = 0;
            Vector3 damageImpulse = damageImpulseForward * _impulseForce;
            _playerRigidbody.AddForce(damageImpulse, ForceMode.Impulse);
            _playerTransform.rotation = Quaternion.LookRotation(damageImpulseForward);
        }

        protected override void Cancel()
        {
            _playerHealth.enabled = true;
        }
    }
}