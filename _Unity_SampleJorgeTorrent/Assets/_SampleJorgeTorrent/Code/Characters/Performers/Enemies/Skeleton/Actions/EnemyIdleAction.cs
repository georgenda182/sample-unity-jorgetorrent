using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyIdleAction : PerformerAction
    {
        private DistanceToPlayerThresholds _distanceToPlayerThresholds;
        private DistanceToPlayerCalculator _distanceToPlayerCalculator;
        private Animator _enemyAnimator;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _distanceToPlayerThresholds = performerServiceLocator.GetService<DistanceToPlayerThresholds>();
            _distanceToPlayerCalculator = performerServiceLocator.GetService<DistanceToPlayerCalculator>();
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
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
            float midDistance = _distanceToPlayerThresholds.MidDistance;

            if (distanceToPlayer > midDistance)
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
            _enemyAnimator.SetBool("IsIdle", true);
        }

        protected override void Cancel()
        {
            _enemyAnimator.SetBool("IsIdle", false);
        }
    }
}