using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using DG.Tweening;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class PlayerDieAction : PerformerAction
    {
        [SerializeField] private float _impulseVelocity = 10f;

        private Health _playerHealth;
        private Transform _playerTransform;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;
        private Material[] _playerMaterials;
        private Transform _enemyTransform;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _playerHealth = performerServiceLocator.GetService<Health>();
            _playerTransform = performerServiceLocator.GetService<Transform>();
            _playerRigidbody = performerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = performerServiceLocator.GetService<Animator>();
            _playerMaterials = performerServiceLocator.GetService<Renderer>().materials;
            _enemyTransform = performerServiceLocator.GetService<EnemyGlobalServices>().Transform;
        }

        protected override void DefinePerformanceConditions()
        {
            _playerHealth.OnKilled += PerformIfAllowed;
        }

        protected override void Perform()
        {
            SetImpulseAndDirection();
            SetDamageGraphicalResponse();
        }

        private void SetImpulseAndDirection()
        {
            Vector3 damageImpulseForward = Vector3.Normalize(_enemyTransform.position - _playerTransform.position);
            damageImpulseForward.y = 0;
            Vector2 damageImpulse = -new Vector2(damageImpulseForward.x, damageImpulseForward.z) * _impulseVelocity;
            Tween damageVelocityChangeTween = DOTween.To(() => damageImpulse, x => damageImpulse = x, Vector2.zero, 0.5f);
            damageVelocityChangeTween.onUpdate = delegate ()
            {
                _playerRigidbody.velocity = new Vector3(damageImpulse.x, _playerRigidbody.velocity.y, damageImpulse.y);
            };
            Vector3 impulseForwardEulerAngles = Quaternion.LookRotation(damageImpulseForward).eulerAngles;
            _playerTransform.DORotate(impulseForwardEulerAngles, 0.3f);
        }

        private void SetDamageGraphicalResponse()
        {
            _playerAnimator.SetTrigger("IsDead");
            foreach (Material playerMaterial in _playerMaterials)
            {
                playerMaterial.DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo);
            }
        }

        protected override void Cancel()
        {
        }
    }
}