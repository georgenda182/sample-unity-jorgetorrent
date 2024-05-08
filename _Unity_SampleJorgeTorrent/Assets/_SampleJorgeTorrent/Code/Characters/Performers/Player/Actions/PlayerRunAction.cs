using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using UniRx;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class PlayerRunAction : PerformerAction
    {
        [SerializeField] private float _velocity = 4;

        private GameInputControls _playerInputControls;
        private Transform _playerTransform;
        private PlayerMaths _playerMaths;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        protected override void StorePerformerServices(ServiceLocator playerServiceLocator)
        {
            _playerInputControls = playerServiceLocator.GetService<GameInputControls>();
            _playerTransform = playerServiceLocator.GetService<Transform>();
            _playerMaths = playerServiceLocator.GetService<PlayerMaths>();
            _playerRigidbody = playerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DefinePerformanceConditions()
        {
            _playerInputControls.Player.Move.started += OnMoveInputActionStarted;
            _playerInputControls.Player.Move.canceled += OnMoveInputActionCanceled;
            _playerInputControls.Player.Move.performed += OnMoveInputActionPerformed;

            DefineReactivation();
        }

        private void OnMoveInputActionStarted(CallbackContext context)
        {
            PerformIfAllowed();
        }

        private void OnMoveInputActionCanceled(CallbackContext context)
        {
            CancelIfActive();
        }

        private void OnMoveInputActionPerformed(CallbackContext context)
        {
            Run();
        }

        private void DefineReactivation()
        {
            foreach (BoolProperty prohibitorState in _prohibitorStates)
            {
                prohibitorState.Property.Subscribe(TryReactivation);
            }
        }

        private void TryReactivation(bool prohibitorStateIsActive)
        {
            if (prohibitorStateIsActive)
            {
                return;
            }

            if (IsMoveInputInProgress())
            {
                PerformIfAllowed();
            }
        }

        private bool IsMoveInputInProgress()
        {
            float inputActionAbsValue = Mathf.Abs(_playerInputControls.Player.Move.ReadValue<Vector2>().magnitude);
            return inputActionAbsValue > 0;
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsRunning", true);
            Run();
        }

        protected override void Cancel()
        {
            _playerAnimator.SetBool("IsRunning", false);
            StopMovement();
        }

        private void StopMovement()
        {
            float verticalVelocity = _playerRigidbody.velocity.y;
            Vector3 newVelocity = _playerTransform.up * verticalVelocity;
            _playerRigidbody.velocity = newVelocity;
        }

        private void Run()
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
            Vector3 newVelocity = _playerTransform.forward * _velocity;
            newVelocity.y = verticalVelocity;
            _playerRigidbody.velocity = newVelocity;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _playerInputControls.Player.Move.started -= OnMoveInputActionStarted;
            _playerInputControls.Player.Move.canceled -= OnMoveInputActionCanceled;
            _playerInputControls.Player.Move.performed -= OnMoveInputActionPerformed;
        }
    }
}