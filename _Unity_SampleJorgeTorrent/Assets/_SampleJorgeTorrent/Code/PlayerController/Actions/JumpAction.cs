using _SampleJorgeTorrent.Code.DesignPatterns;
using _SampleJorgeTorrent.Code.ScriptableProperties;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.Actions
{
    public class JumpAction : PlayerAction
    {
        [SerializeField] private float _forceMagnitude = 250;
        private GameInputControls _playerInputControls;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        protected override void Configure(ServiceLocator playerServiceLocator)
        {
            _playerInputControls = playerServiceLocator.GetService<GameInputControls>();
            _playerRigidbody = playerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DefinePerformanceConditions()
        {
            _playerInputControls.Player.Jump.performed += context => PerformIfAllowed();
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
    }
}