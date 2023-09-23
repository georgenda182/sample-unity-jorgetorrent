using System;
using _SampleJorgeTorrent.Code.DesignPatterns;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.Actions
{
    public class RunAction : PlayerAction
    {
        private GameInputControls _playerInputControls;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        protected override void Configure(ServiceLocator playerServiceLocator)
        {
            _playerInputControls = playerServiceLocator.GetService<GameInputControls>();
            _playerRigidbody = playerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DeterminePerformanceConditions()
        {
            _playerInputControls.Player.Move.started += context => TryPerformance();
            _playerInputControls.Player.Move.canceled += context => TryCancellation();
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsRunning", true);
            Debug.Log("Perform run");
        }

        protected override void Cancel()
        {
            _playerAnimator.SetBool("IsRunning", false);
            Debug.Log("Cancel run");
        }

        private void Update()
        {
            if (IsInactive)
            {
                if (_playerInputControls.Player.Move.IsInProgress())
                {
                    TryPerformance();
                }
                return;
            }
            Run(_playerInputControls.Player.Move.ReadValue<Vector2>());
        }

        private void Run(Vector2 direction)
        {
            Debug.Log("qqq");
        }
    }
}