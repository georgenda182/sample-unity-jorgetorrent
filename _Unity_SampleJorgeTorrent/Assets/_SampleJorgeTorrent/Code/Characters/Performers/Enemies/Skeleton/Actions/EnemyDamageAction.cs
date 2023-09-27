using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using DG.Tweening;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyDamageAction : PerformerAction
    {
        private Transform _enemyTransform;
        private Material[] _enemyMaterials;
        private Animator _enemyAnimator;
        private AnimationEventsDispatcher _enemyAnimationEventsDispatcher;
        private Health _enemyHealth;

        private Transform _playerTransform;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _enemyTransform = performerServiceLocator.GetService<Transform>();
            _enemyMaterials = performerServiceLocator.GetService<Renderer>().materials;
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
            _enemyAnimationEventsDispatcher = performerServiceLocator.GetService<AnimationEventsDispatcher>();
            _enemyHealth = performerServiceLocator.GetService<Health>();
            _playerTransform = performerServiceLocator.GetService<PlayerGlobalServices>().Transform;
        }

        protected override void DefinePerformanceConditions()
        {
            _enemyHealth.OnDamaged += PerformIfAllowed;
            _enemyAnimationEventsDispatcher.SubscribeEventCallbackToAnimation("OnRecoveredFromDamage", CancelIfActive);
        }

        protected override void Perform()
        {
            StartInvulnerability();
            LookAtPlayer();
            SetDamageGraphicalResponse();
        }

        private void StartInvulnerability()
        {
            _enemyHealth.SetVulnerability(false);
        }

        private void LookAtPlayer()
        {
            Vector3 forwardToPlayer = _playerTransform.position - _enemyTransform.position;
            forwardToPlayer.y = 0;
            forwardToPlayer.Normalize();
            Vector3 rotationFromForward = Quaternion.LookRotation(forwardToPlayer).eulerAngles;
            _enemyTransform.DORotate(rotationFromForward, 0.5f);
        }

        private void SetDamageGraphicalResponse()
        {
            _enemyAnimator.SetTrigger("Damaged");
            foreach (Material enemyMaterial in _enemyMaterials)
            {
                enemyMaterial.DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo);
            }
        }

        protected override void Cancel()
        {
            EndInvulnerability();
        }

        private void EndInvulnerability()
        {
            _enemyHealth.SetVulnerability(true);
        }
    }
}