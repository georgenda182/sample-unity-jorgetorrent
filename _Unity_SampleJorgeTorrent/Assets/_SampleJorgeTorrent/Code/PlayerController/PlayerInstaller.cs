using _SampleJorgeTorrent.Code.DesignPatterns;
using _SampleJorgeTorrent.Code.PlayerController.Actions;
using _SampleJorgeTorrent.Code.PlayerController.Services;
using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController
{
    public class PlayerInstaller : MonoBehaviour
    {
        private GameInputControls _playerInputControls;

        [Header("Services")]
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private GroundTriggerDetector _groundDetector;

        [Header("Actions")]
        [SerializeField] private List<PlayerAction> _playerActions;

        private ServiceLocator _playerServiceLocator;

        private void Start()
        {
            ConfigureServiceLocator();
            InstallActions();
        }

        private void ConfigureServiceLocator()
        {
            _playerServiceLocator = new ServiceLocator();

            _playerInputControls = new GameInputControls();
            _playerInputControls.Enable();

            _playerServiceLocator.RegisterService(_playerInputControls);
            _playerServiceLocator.RegisterService(transform);
            _playerServiceLocator.RegisterService(_playerRigidbody);
            _playerServiceLocator.RegisterService(_playerAnimator);
            _playerServiceLocator.RegisterService<GroundDetector>(_groundDetector);
        }

        private void InstallActions()
        {
            foreach (PlayerAction action in _playerActions)
            {
                action.Install(_playerServiceLocator);
            }
        }
    }
}