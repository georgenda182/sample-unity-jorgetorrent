using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class PlayerJumpAction : PerformerAction
    {
        [SerializeField] private float _forceMagnitude = 250;

        private GameInputControls _playerInputControls;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        protected override void StorePerformerServices(ServiceLocator playerServiceLocator)
        {
            _playerInputControls = playerServiceLocator.GetService<GameInputControls>();
            _playerRigidbody = playerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DefinePerformanceConditions()
        {
            _playerInputControls.Player.Jump.performed += OnJumpInputActionPerformed;
        }

        private void OnJumpInputActionPerformed(CallbackContext context)
        {
            PerformIfAllowed();
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsJumping", true);
            _playerRigidbody.AddForce(transform.up * _forceMagnitude);
        }

        protected override void Cancel()
        {
            _playerAnimator.SetBool("IsJumping", false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _playerInputControls.Player.Jump.performed -= OnJumpInputActionPerformed;
        }
    }
}