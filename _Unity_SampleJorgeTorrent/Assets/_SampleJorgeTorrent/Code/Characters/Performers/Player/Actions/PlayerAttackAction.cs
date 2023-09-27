using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using DG.Tweening;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class PlayerAttackAction : PerformerAction
    {
        [SerializeField] private float _impulseHorizontalVelocity = 30f;
        [SerializeField] private float _impulseVerticalVelocity = 5;

        private Transform _playerTransform;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;
        private AnimationEventsDispatcher _playerAnimationEventsDispatcher;
        private Health _enemyHealth;
        private Transform _enemyTransform;

        protected override void StorePerformerServices(ServiceLocator playerServiceLocator)
        {
            _playerTransform = playerServiceLocator.GetService<Transform>();
            _playerRigidbody = playerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
            _playerAnimationEventsDispatcher = playerServiceLocator.GetService<AnimationEventsDispatcher>();
            _enemyHealth = playerServiceLocator.GetService<EnemyGlobalServices>().Health;
            _enemyTransform = playerServiceLocator.GetService<EnemyGlobalServices>().Transform;
        }

        protected override void DefinePerformanceConditions()
        {
            _enemyHealth.OnDamaged += PerformIfAllowed;
            _enemyHealth.OnKilled += PerformIfAllowed;
            _playerAnimationEventsDispatcher.SubscribeEventCallbackToAnimation("OnAttackCooldownFinished", CancelIfActive);
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsAttacking", true);
            SetImpulseAndDirection();
        }

        private void SetImpulseAndDirection()
        {
            Vector3 damageImpulseForward = Vector3.Normalize(_enemyTransform.position - _playerTransform.position);
            Vector3 damageImpulse = -damageImpulseForward.normalized * _impulseHorizontalVelocity;
            _playerRigidbody.velocity = new Vector3(damageImpulse.x, _impulseVerticalVelocity, damageImpulse.z);
            Tween damageVelocityChangeTween = DOTween.To(() => damageImpulse, x => damageImpulse = x, Vector3.zero, 1f);
            damageVelocityChangeTween.onUpdate = delegate ()
            {
                _playerRigidbody.velocity = new Vector3(damageImpulse.x, _playerRigidbody.velocity.y, damageImpulse.z);
            };
            Vector3 forwardToEnemy = damageImpulseForward;
            forwardToEnemy.y = 0;
            Vector3 impulseForwardEulerAngles = Quaternion.LookRotation(forwardToEnemy).eulerAngles;
            _playerTransform.DORotate(impulseForwardEulerAngles, 0.3f);
        }

        protected override void Cancel()
        {
            _playerAnimator.SetBool("IsAttacking", false);
        }
    }
}