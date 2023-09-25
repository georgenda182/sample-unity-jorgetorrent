using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class PlayerFallAction : PerformerAction
    {
        [SerializeField] private float _redirectionVelocity = 4;

        private GameInputControls _playerInputControls;
        private GroundDetector _groundEventsDispatcher;
        private Transform _playerTransform;
        private PlayerMaths _playerMaths;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        protected override void StorePerformerServices(ServiceLocator playerServiceLocator)
        {
            _playerInputControls = playerServiceLocator.GetService<GameInputControls>();
            _groundEventsDispatcher = playerServiceLocator.GetService<GroundDetector>();
            _playerTransform = playerServiceLocator.GetService<Transform>();
            _playerMaths = playerServiceLocator.GetService<PlayerMaths>();
            _playerRigidbody = playerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DefinePerformanceConditions()
        {
            _groundEventsDispatcher.OnJustUngrounded += PerformIfAllowed;
            _groundEventsDispatcher.OnJustGrounded += CancelIfActive;
            _playerInputControls.Player.Move.performed += context => Redirect();
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsFalling", true);
            if (IsMoveInputInProgress())
            {
                Redirect();
            }
        }

        private bool IsMoveInputInProgress()
        {
            float inputActionAbsValue = Mathf.Abs(_playerInputControls.Player.Move.ReadValue<Vector2>().magnitude);
            return inputActionAbsValue > 0;
        }

        protected override void Cancel()
        {
            _playerAnimator.SetBool("IsFalling", false);
            StopRedirection();
        }

        private void StopRedirection()
        {
            float verticalVelocity = _playerRigidbody.velocity.y;
            Vector3 newVelocity = _playerTransform.up * verticalVelocity;
            _playerRigidbody.velocity = newVelocity;
        }

        private void Redirect()
        {
            if (IsInactive)
            {
                return;
            }

            OrientBody();
            SetBodyVelocity();
        }

        private void OrientBody()
        {
            _playerTransform.rotation = Quaternion.Euler(_playerMaths.EulerRotation);
        }

        private void SetBodyVelocity()
        {
            float verticalVelocity = _playerRigidbody.velocity.y;
            Vector3 newVelocity = _playerTransform.forward * _redirectionVelocity;
            newVelocity.y = verticalVelocity;
            _playerRigidbody.velocity = newVelocity;
        }
    }
}