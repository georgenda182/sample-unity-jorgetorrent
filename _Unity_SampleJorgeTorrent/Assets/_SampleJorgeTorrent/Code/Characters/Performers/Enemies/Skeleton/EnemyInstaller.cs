using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Utilities;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;
using UnityEngine.AI;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton
{
    public class EnemyInstaller : Performer, ServicesConsumer
    {
        [Header("Services")]
        [SerializeField] private EnemyBrain _enemyBrain;
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private NavMeshAgent _enemyNavMeshAgent;
        [SerializeField] private Animator _enemyAnimator;

        private PlayerTransformWrapper _playerTransform;

        public void Install(ServiceLocator globalServiceLocator)
        {
            StoreGlobalServices(globalServiceLocator);
            ConfigureEnemyServiceLocator();
            InitializeBrain();
            InstallActions();
        }

        private void StoreGlobalServices(ServiceLocator globalServiceLocator)
        {
            _playerTransform = new PlayerTransformWrapper();
            _playerTransform.Value = globalServiceLocator.GetService<Transform>();
        }

        private void ConfigureEnemyServiceLocator()
        {
            _performerServiceLocator.RegisterService(_enemyBrain);
            _performerServiceLocator.RegisterService(_enemyTransform);
            _performerServiceLocator.RegisterService(_enemyNavMeshAgent);
            _performerServiceLocator.RegisterService(_enemyAnimator);
            _performerServiceLocator.RegisterService(_playerTransform);
        }

        private void InitializeBrain()
        {
            _enemyBrain.Install(_performerServiceLocator);
        }
    }
}
