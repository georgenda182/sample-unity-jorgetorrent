using _SampleJorgeTorrent.Code.Characters.Performers;
using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyChaseAction : PerformerAction
    {
        private EnemyBrain _enemyBrain;
        private NavMeshAgent _enemyNavMeshAgent;
        private Transform _playerTransform;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _enemyBrain = performerServiceLocator.GetService<EnemyBrain>();
            _enemyNavMeshAgent = performerServiceLocator.GetService<NavMeshAgent>();
            _playerTransform = performerServiceLocator.GetService<Transform>();
        }

        protected override void DefinePerformanceConditions()
        {
            _enemyBrain.OnPlayerAtMidDistance += delegate
            {
                PerformIfAllowed();
            };
            _enemyBrain.OnPlayerAtLargeDistance += delegate
            {
                CancelIfActive();
            };
            _enemyBrain.OnPlayerAtLowDistance += delegate
            {
                CancelIfActive();
            };
        }

        protected override void Perform()
        {
            _enemyNavMeshAgent.enabled = true;
        }

        protected override void Cancel()
        {
            _enemyNavMeshAgent.enabled = false;
        }

        private void Update()
        {
            if (IsInactive)
            {
                return;
            }
            SetNavMeshDestination();
        }

        private void SetNavMeshDestination()
        {
            _enemyNavMeshAgent.SetDestination(_playerTransform.position);
        }
    }
}