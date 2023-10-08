using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player
{
    public class PlayerPedestrianInstaller : Performer, ServicesConsumer
    {
        private GameInputControls _playerInputControls;

        [Header("Services")]
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private AnimationEventsDispatcher _playerAnimationEventsDispatcher;
        [SerializeField] private GroundTriggerDetector _groundDetector;

        private Camera _playerCamera;
        private PlayerMaths _playerMaths;

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
        }

        private void ConfigurePlayerServiceLocator()
        {
            _playerAnimationEventsDispatcher.Configure();
            _playerMaths = new PlayerMaths();

            _performerServiceLocator.RegisterService(_playerCamera);
            _performerServiceLocator.RegisterService(_playerInputControls);
            _performerServiceLocator.RegisterService(transform);
            _performerServiceLocator.RegisterService(_playerRigidbody);
            _performerServiceLocator.RegisterService(_playerAnimator);
            _performerServiceLocator.RegisterService(_playerAnimationEventsDispatcher);
            _performerServiceLocator.RegisterService<GroundDetector>(_groundDetector);
            _performerServiceLocator.RegisterService(_playerMaths);
        }

        private void InstallOtherServicesConsumers()
        {
            _playerMaths.Install(_performerServiceLocator);
        }
    }
}