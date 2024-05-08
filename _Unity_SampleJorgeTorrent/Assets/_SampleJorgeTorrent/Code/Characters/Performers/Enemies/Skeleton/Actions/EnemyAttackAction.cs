using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Characters.Performers.Player;
using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyAttackAction : PerformerAction
    {
        private DistanceToPlayerThresholds _distanceToPlayerThresholds;
        private DistanceToPlayerCalculator _distanceToPlayerCalculator;
        private EnemyWeapon _enemyWeapon;
        private Transform _enemyTransform;
        private Animator _enemyAnimator;
        private Transform _playerTransform;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _distanceToPlayerThresholds = performerServiceLocator.GetService<DistanceToPlayerThresholds>();
            _distanceToPlayerCalculator = performerServiceLocator.GetService<DistanceToPlayerCalculator>();
            _enemyWeapon = performerServiceLocator.GetService<EnemyWeapon>();
            _enemyTransform = performerServiceLocator.GetService<Transform>();
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
            _playerTransform = performerServiceLocator.GetService<PerformerServices<PlayerBattleInstaller>>().Transform;
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

            if (distanceToPlayer <= lowDistance)
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
            _enemyAnimator.SetBool("IsAttacking", true);
        }

        protected override void Cancel()
        {
            _enemyAnimator.SetBool("IsAttacking", false);
            _enemyWeapon.DisableHitVolume();
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