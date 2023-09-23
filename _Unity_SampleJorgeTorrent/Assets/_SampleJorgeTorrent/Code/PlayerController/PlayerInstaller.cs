using _SampleJorgeTorrent.Code.DesignPatterns.ServiceLocatorPattern;
using _SampleJorgeTorrent.Code.PlayerController.Actions;
using _SampleJorgeTorrent.Code.PlayerController.CameraSystem;
using _SampleJorgeTorrent.Code.PlayerController.Services;
using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController
{
    public class PlayerInstaller : MonoBehaviour, ServicesConsumer
    {
        private GameInputControls _playerInputControls;

        [Header("Services")]
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private GroundTriggerDetector _groundDetector;
        private Camera _playerCamera;
        private PlayerMaths _playerMaths;

        [Header("Actions")]
        [SerializeField] private List<PlayerAction> _playerActions;

        private ServiceLocator _playerServiceLocator;

        public void Install(ServiceLocator globalServiceLocator)
        {
            StoreGlobalServices(globalServiceLocator);
            ConfigurePlayerServiceLocator();
            InstallActions();
            InstallOtherServicesConsumers();
            ReparentToRoot();
        }

        private void StoreGlobalServices(ServiceLocator globalServiceLocator)
        {
            _playerInputControls = globalServiceLocator.GetService<GameInputControls>();
            _playerCamera = globalServiceLocator.GetService<Camera>();
        }

        private void ConfigurePlayerServiceLocator()
        {
            _playerServiceLocator = new ServiceLocator();

            _playerMaths = new PlayerMaths();

            _playerServiceLocator.RegisterService(_playerCamera);
            _playerServiceLocator.RegisterService(_playerInputControls);
            _playerServiceLocator.RegisterService(transform);
            _playerServiceLocator.RegisterService(_playerRigidbody);
            _playerServiceLocator.RegisterService(_playerAnimator);
            _playerServiceLocator.RegisterService<GroundDetector>(_groundDetector);
            _playerServiceLocator.RegisterService(_playerMaths);
        }

        private void InstallActions()
        {
            foreach (PlayerAction action in _playerActions)
            {
                action.Install(_playerServiceLocator);
            }
        }

        private void InstallOtherServicesConsumers()
        {
            _playerMaths.Install(_playerServiceLocator);
        }

        private void ReparentToRoot()
        {
            transform.parent = null;
        }
    }
}