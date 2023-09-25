using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Utilities;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;
using UnityEngine.AI;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyAttackAction : PerformerAction
    {
        private EnemyBrain _enemyBrain;
        private Transform _enemyTransform;
        private Animator _enemyAnimator;
        private Transform _playerTransform;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _enemyBrain = performerServiceLocator.GetService<EnemyBrain>();
            _enemyTransform = performerServiceLocator.GetService<Transform>();
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
            _playerTransform = performerServiceLocator.GetService<PlayerTransformWrapper>().Value;
        }

        protected override void DefinePerformanceConditions()
        {
            _enemyBrain.OnPlayerAtLowDistance += PerformIfAllowed;
            _enemyBrain.OnPlayerAtMidDistance += CancelIfActive;
        }

        protected override void Perform()
        {
            _enemyAnimator.SetBool("IsAttacking", true);
        }

        protected override void Cancel()
        {
            _enemyAnimator.SetBool("IsAttacking", false);
        }

        private void Update()
        {
            if (IsInactive)
            {
                return;
            }

            LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            Vector3 forwardToPlayer = Vector3.Normalize(_playerTransform.position - _enemyTransform.position);
            forwardToPlayer.y = 0;
            Quaternion lookAtPlayerRotation = Quaternion.LookRotation(forwardToPlayer);
            Quaternion finalRotation = Quaternion.Lerp(transform.rotation, lookAtPlayerRotation, 0.5f);
            _enemyTransform.rotation = finalRotation;
        }
    }
}