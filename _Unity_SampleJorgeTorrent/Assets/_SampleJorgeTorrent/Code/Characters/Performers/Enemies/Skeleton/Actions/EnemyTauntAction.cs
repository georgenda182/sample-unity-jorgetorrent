using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyTauntAction : PerformerAction
    {
        private Animator _enemyAnimator;
        private AnimationEventsDispatcher _enemyAnimationEventsDispatcher;
        private Health _playerHealth;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
            _enemyAnimationEventsDispatcher = performerServiceLocator.GetService<AnimationEventsDispatcher>();
            _playerHealth = performerServiceLocator.GetService<PlayerGlobalServices>().Health;
        }

        protected override void DefinePerformanceConditions()
        {
            _playerHealth.OnDamaged += PerformIfAllowed;
            _enemyAnimationEventsDispatcher.SubscribeEventCallbackToAnimation("OnFinishedTaunting", CancelIfActive);
        }

        protected override void Perform()
        {
            _enemyAnimator.SetBool("IsTaunting", true);
        }

        protected override void Cancel()
        {
            _enemyAnimator.SetBool("IsTaunting", false);
        }
    }
}