using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using Assets._SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using UnityEngine;
using UnityEngine.AI;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton
{
    public class EnemyInstaller : Performer, ServicesConsumer
    {
        [Header("Services")]
        [SerializeField] private EnemyBrain _enemyBrain;
        [SerializeField] private NavMeshAgent _enemyNavMeshAgent;
        [SerializeField] private Animator _enemyAnimator;

        private Transform _playerTransform;
        private EnemyAnimationController _enemyAnimationController;

        public void Install(ServiceLocator globalServiceLocator)
        {
            StoreGlobalServices(globalServiceLocator);
            ConfigureEnemyServiceLocator();
            InitializeBrain();
            InstallActions();
            InstallOtherConsumers();
        }

        private void StoreGlobalServices(ServiceLocator globalServiceLocator)
        {
            _playerTransform = globalServiceLocator.GetService<Transform>();
        }

        private void ConfigureEnemyServiceLocator()
        {
            _performerServiceLocator.RegisterService(_enemyBrain);
            _performerServiceLocator.RegisterService(_enemyNavMeshAgent);
            _performerServiceLocator.RegisterService(_enemyAnimator);
            _performerServiceLocator.RegisterService(_playerTransform);
        }

        private void InitializeBrain()
        {
            _enemyBrain.Install(_performerServiceLocator);
        }

        private void InstallOtherConsumers()
        {
            _enemyAnimationController = new EnemyAnimationController();
            _enemyAnimationController.Install(_performerServiceLocator);
        }
    }
}
