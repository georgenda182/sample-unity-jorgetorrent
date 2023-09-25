using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyIdleAction : PerformerAction
    {
        private EnemyBrain _enemyBrain;
        private Animator _enemyAnimator;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _enemyBrain = performerServiceLocator.GetService<EnemyBrain>();
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
        }

        protected override void DefinePerformanceConditions()
        {
            _enemyBrain.OnPlayerAtLargeDistance += delegate
            {
                PerformIfAllowed();
            };
            _enemyBrain.OnPlayerAtMidDistance += delegate
            {
                CancelIfActive();
            };
        }

        protected override void Perform()
        {
            _enemyAnimator.SetBool("IsIdle", true);
        }

        protected override void Cancel()
        {
            _enemyAnimator.SetBool("IsIdle", false);
        }
    }
}