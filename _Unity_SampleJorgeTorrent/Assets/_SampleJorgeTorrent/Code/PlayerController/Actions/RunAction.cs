using _SampleJorgeTorrent.Code.DesignPatterns;
using _SampleJorgeTorrent.Code.ScriptableProperties;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.Actions
{
    public class RunAction : PlayerAction
    {
        [SerializeField] private float _velocity = 4;

        private GameInputControls _playerInputControls;
        private Transform _playerTransform;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;

        protected override void Configure(ServiceLocator playerServiceLocator)
        {
            _playerInputControls = playerServiceLocator.GetService<GameInputControls>();
            _playerTransform = playerServiceLocator.GetService<Transform>();
            _playerRigidbody = playerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DefinePerformanceConditions()
        {
            _playerInputControls.Player.Move.started += context => PerformIfAllowed();
            _playerInputControls.Player.Move.canceled += context => CancelIfActive();
            _playerInputControls.Player.Move.performed += context => Run(context.ReadValue<Vector2>());

            DefineReactivation();
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

            if (IsInputActionInProgress())
            {
                PerformIfAllowed();
            }
        }

        private bool IsInputActionInProgress()
        {
            float inputActionAbsValue = Mathf.Abs(_playerInputControls.Player.Move.ReadValue<Vector2>().magnitude);
            return inputActionAbsValue > 0;
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsRunning", true);
            Run(_playerInputControls.Player.Move.ReadValue<Vector2>());
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

        private void Run(Vector2 direction)
        {
            if (IsInactive)
            {
                return;
            }

            SetBodyRotation(direction);
            SetBodyVelocity();
        }

        private void SetBodyRotation(Vector2 direction)
        {
            float angleOffset = direction.y < 0 ? 180 : 0;
            float rotation = Camera.main.transform.rotation.eulerAngles.y + angleOffset + Mathf.Rad2Deg * Mathf.Atan(direction.x / direction.y);
            Vector3 newRotation = new Vector3(0, rotation, 0);
            _playerTransform.rotation = Quaternion.Euler(newRotation);
        }

        private void SetBodyVelocity()
        {
            float verticalVelocity = _playerRigidbody.velocity.y;
            Vector3 newVelocity = _playerTransform.forward * _velocity;
            newVelocity.y = verticalVelocity;
            _playerRigidbody.velocity = newVelocity;
        }
    }
}