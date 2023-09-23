using _SampleJorgeTorrent.Code.DesignPatterns;
using _SampleJorgeTorrent.Code.PlayerController.Actions;
using _SampleJorgeTorrent.Code.PlayerController.Services;
using UnityEngine;

namespace Assets._SampleJorgeTorrent.Code.PlayerController.Actions
{
    public class FallAction : PlayerAction
    {
        [SerializeField] private float _redirectionVelocity = 4;

        private GameInputControls _playerInputControls;
        private GroundDetector _groundEventsDispatcher;
        private Transform _playerTransform;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        protected override void Configure(ServiceLocator playerServiceLocator)
        {
            _playerInputControls = playerServiceLocator.GetService<GameInputControls>();
            _groundEventsDispatcher = playerServiceLocator.GetService<GroundDetector>();
            _playerTransform = playerServiceLocator.GetService<Transform>();
            _playerRigidbody = playerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DefinePerformanceConditions()
        {
            _groundEventsDispatcher.OnJustUngrounded += PerformIfAllowed;
            _groundEventsDispatcher.OnJustGrounded += CancelIfActive;
            _playerInputControls.Player.Move.performed += context => Redirect(context.ReadValue<Vector2>());
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsFalling", true);
            Redirect(_playerInputControls.Player.Move.ReadValue<Vector2>());
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

        private void Redirect(Vector2 direction)
        {
            if (IsInactive)
            {
                return;
            }

            float angleOffset = direction.y < 0 ? 180 : 0;
            float rotation = Camera.main.transform.rotation.eulerAngles.y + angleOffset + Mathf.Rad2Deg * Mathf.Atan(direction.x / direction.y);
            Vector3 newRotation = new Vector3(0, rotation, 0);
            _playerTransform.rotation = Quaternion.Euler(newRotation);

            float verticalVelocity = _playerRigidbody.velocity.y;
            Vector3 newVelocity = _playerTransform.forward * _redirectionVelocity;
            newVelocity.y = verticalVelocity;
            _playerRigidbody.velocity = newVelocity;
        }
    }
}