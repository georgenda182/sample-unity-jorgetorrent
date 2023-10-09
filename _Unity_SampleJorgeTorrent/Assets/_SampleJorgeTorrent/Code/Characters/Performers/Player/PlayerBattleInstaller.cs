using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using AYellowpaper;
using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player
{
    public class PlayerBattleInstaller : Performer, ServicesConsumer
    {
        private GameInputControls _playerInputControls;

        [Header("Services")]
        [SerializeField] private Health _playerHealth;
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private AnimationEventsDispatcher _playerAnimationEventsDispatcher;
        [SerializeField] private Renderer _playerRenderer;
        [SerializeField] private GroundTriggerDetector _groundDetector;

        [Header("Consumers")]
        [SerializeField] private List<InterfaceReference<ServicesConsumer>> _servicesConsumers;

        private Camera _playerCamera;
        private PlayerMaths _playerMaths;
        private EnemyGlobalServices _enemyGlobalServices;

        public void Install(ServiceLocator globalServiceLocator)
        {
            StoreGlobalServices(globalServiceLocator);
            ConfigurePlayerServiceLocator();
            InstallActions();
            InstallOtherServicesConsumers();
        }

        private void StoreGlobalServices(ServiceLocator globalServiceLocator)
        {
            _playerInputControls = globalServiceLocator.GetService<GameInputControls>();
            _playerCamera = globalServiceLocator.GetService<Camera>();
            _enemyGlobalServices = globalServiceLocator.GetService<EnemyGlobalServices>();
        }

        private void ConfigurePlayerServiceLocator()
        {
            _playerAnimationEventsDispatcher.Configure();
            _playerMaths = new PlayerMaths();

            _performerServiceLocator.RegisterService(_playerCamera);
            _performerServiceLocator.RegisterService(_playerInputControls);
            _performerServiceLocator.RegisterService(transform);
            _performerServiceLocator.RegisterService(_playerHealth);
            _performerServiceLocator.RegisterService(_playerRigidbody);
            _performerServiceLocator.RegisterService(_playerAnimator);
            _performerServiceLocator.RegisterService(_playerAnimationEventsDispatcher);
            _performerServiceLocator.RegisterService(_playerRenderer);
            _performerServiceLocator.RegisterService<GroundDetector>(_groundDetector);
            _performerServiceLocator.RegisterService(_playerMaths);
            _performerServiceLocator.RegisterService(_enemyGlobalServices);
        }

        private void InstallOtherServicesConsumers()
        {
            _playerMaths.Install(_performerServiceLocator);
            foreach (var consumer in _servicesConsumers)
            {
                consumer.Value.Install(_performerServiceLocator);
            }
        }
    }
}