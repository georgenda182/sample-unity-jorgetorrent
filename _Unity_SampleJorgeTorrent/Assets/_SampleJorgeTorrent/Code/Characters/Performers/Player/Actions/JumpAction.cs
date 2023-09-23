using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class JumpAction : PerformerAction
    {
        [SerializeField] private float _forceMagnitude = 250;

        private GameInputControls _playerInputControls;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        protected override void StorePlayerServices(ServiceLocator playerServiceLocator)
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