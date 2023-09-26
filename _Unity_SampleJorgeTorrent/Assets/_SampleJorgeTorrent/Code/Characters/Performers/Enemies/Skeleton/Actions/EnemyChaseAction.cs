using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyChaseAction : PerformerAction
    {
        private DistanceToPlayerThresholds _distanceToPlayerThresholds;
        private DistanceToPlayerCalculator _distanceToPlayerCalculator;
        private NavMeshAgent _enemyNavMeshAgent;
        private Animator _enemyAnimator;
        private Transform _playerTransform;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _distanceToPlayerThresholds = performerServiceLocator.GetService<DistanceToPlayerThresholds>();
            _distanceToPlayerCalculator = performerServiceLocator.GetService<DistanceToPlayerCalculator>();
            _enemyNavMeshAgent = performerServiceLocator.GetService<NavMeshAgent>();
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
            _playerTransform = performerServiceLocator.GetService<PlayerGlobalServices>().Transform;
        }

        protected override void DefinePerformanceConditions()
        {
            _distanceToPlayerCalculator.DistanceToPlayer.Subscribe(TriggerPerformanceByDistanceToPlayer);
            DefineReactivation();
        }

        private void DefineReactivation()
        {
            foreach (BoolProperty prohibitorState in _prohibitorStates)
            {
                prohibitorState.Property.Subscribe(TryReactivation);
            }
        }

        private void TryReactivation(bool prohibitorStateIsActive)
        {
            if (prohibitorStateIsActive)
            {
                return;
            }

            TriggerPerformanceByDistanceToPlayer(_distanceToPlayerCalculator.DistanceToPlayer.Value);
        }

        private void TriggerPerformanceByDistanceToPlayer(float distanceToPlayer)
        {
            float lowDistance = _distanceToPlayerThresholds.LowDistance;
            float midDistance = _distanceToPlayerThresholds.MidDistance;

            if (distanceToPlayer > lowDistance && distanceToPlayer <= midDistance)
            {
                PerformIfAllowed();
            }
            else
            {
                CancelIfActive();
            }
        }

        protected override void Perform()
        {
            _enemyAnimator.SetBool("IsChasing", true);
            _enemyNavMeshAgent.enabled = true;
        }

        protected override void Cancel()
        {
            _enemyAnimator.SetBool("IsChasing", false);
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