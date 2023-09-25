using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;
using UnityEngine.AI;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyIdleAction : PerformerAction
    {
        private EnemyBrain _enemyBrain;
        private NavMeshAgent _enemyNavMeshAgent;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _enemyBrain = performerServiceLocator.GetService<EnemyBrain>();
            _enemyNavMeshAgent = performerServiceLocator.GetService<NavMeshAgent>();
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
            _enemyNavMeshAgent.enabled = false;
        }

        protected override void Cancel()
        {
            _enemyNavMeshAgent.enabled = true;
        }
    }
}