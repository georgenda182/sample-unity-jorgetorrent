using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyCelebrateAction : PerformerAction
    {
        private Animator _enemyAnimator;
        private Health _playerHealth;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
            _playerHealth = performerServiceLocator.GetService<PlayerGlobalServices>().Health;
        }

        protected override void DefinePerformanceConditions()
        {
            _playerHealth.OnKilled += PerformIfAllowed;
        }

        protected override void Perform()
        {
            _enemyAnimator.SetBool("IsCelebrating", true);
        }

        protected override void Cancel()
        {
            _enemyAnimator.SetBool("IsCelebrating", false);
        }
    }
}