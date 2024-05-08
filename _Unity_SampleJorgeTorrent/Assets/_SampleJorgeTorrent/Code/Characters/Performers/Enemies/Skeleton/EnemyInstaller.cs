using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Characters.Performers.Player;
using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton
{
    [Serializable]
    public struct DistanceToPlayerThresholds
    {
        public float LowDistance;
        public float MidDistance;
    }

    public class EnemyInstaller : Performer, ServicesConsumer
    {
        [Header("Services")]
        [SerializeField] private DistanceToPlayerCalculator _distanceToPlayerCalculator;
        [SerializeField] private Health _enemyHealth;
        [SerializeField] private EnemyWeapon _enemyWeapon;
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private NavMeshAgent _enemyNavMeshAgent;
        [SerializeField] private Renderer _enemyRenderer;
        [SerializeField] private Animator _enemyAnimator;
        [SerializeField] private AnimationEventsDispatcher _enemyAnimationEventsDispatcher;

        [SerializeField] private DistanceToPlayerThresholds _distanceToPlayerThresholds;

        private PerformerServices<PlayerBattleInstaller> _playerGlobalServices;

        public void Install(ServiceLocator globalServiceLocator)
        {
            StoreGlobalServices(globalServiceLocator);
            ConfigureEnemyServiceLocator();
            InitializeOwnServices();
            InstallActions();
        }

        private void StoreGlobalServices(ServiceLocator globalServiceLocator)
        {
            _playerGlobalServices = globalServiceLocator.GetService<PerformerServices<PlayerBattleInstaller>>();
        }

        private void ConfigureEnemyServiceLocator()
        {
            _enemyAnimationEventsDispatcher.Configure();

            _performerServiceLocator.RegisterService(_distanceToPlayerCalculator);
            _performerServiceLocator.RegisterService(_enemyHealth);
            _performerServiceLocator.RegisterService(_enemyWeapon);
            _performerServiceLocator.RegisterService(_enemyTransform);
            _performerServiceLocator.RegisterService(_enemyNavMeshAgent);
            _performerServiceLocator.RegisterService(_enemyRenderer);
            _performerServiceLocator.RegisterService(_enemyAnimator);
            _performerServiceLocator.RegisterService(_enemyAnimationEventsDispatcher);
            _performerServiceLocator.RegisterService(_distanceToPlayerThresholds);
            _performerServiceLocator.RegisterService(_playerGlobalServices);
        }

        private void InitializeOwnServices()
        {
            _distanceToPlayerCalculator.Install(_performerServiceLocator);
            _enemyWeapon.Install(_performerServiceLocator);
        }
    }
}
