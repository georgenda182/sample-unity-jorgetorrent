using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.CameraSystem
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour, ServicesConsumer
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _maximumAngleAroundHorizontalAxis = 45;

        [SerializeField] private float _offsetFromPlayer = 12;

        [SerializeField] private float _followSpeed = 0.125f;
        [SerializeField] private float _rotationVelocity = 36;

        private GameInputControls _playerInputControls;

        private bool _isReorienting;

        private Vector3 OffsetFromPlayerVector => transform.forward * _offsetFromPlayer;

        public void Install(ServiceLocator globalServiceLocator)
        {
            StoreGlobalServices(globalServiceLocator);
            SubscribeReorientationToInputControls();
            ReparentToRoot();
        }

        private void StoreGlobalServices(ServiceLocator globalServiceLocator)
        {
            _playerInputControls = globalServiceLocator.GetService<GameInputControls>();
        }

        private void SubscribeReorientationToInputControls()
        {
            _playerInputControls.Player.Camera.started += context => StartReorientation();
            _playerInputControls.Player.Camera.canceled += context => EndReorientation();
        }

        private void ReparentToRoot()
        {
            transform.parent = null;
        }

        private void StartReorientation()
        {
            _isReorienting = true;
        }

        private void EndReorientation()
        {
            _isReorienting = false;
        }

        private void Update()
        {
            if (_isReorienting)
            {
                RotateAroundPlayer();
            }
            Vector3 desiredPosition = _target.position - OffsetFromPlayerVector;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _followSpeed);
            transform.position = smoothedPosition;
        }

        private void RotateAroundPlayer()
        {
            Vector2 rotation = _playerInputControls.Player.Camera.ReadValue<Vector2>();

            RotateAroundVerticalAxis(rotation.x);
            RotateAroundHorizontalAxis(rotation.y);
        }

        private void RotateAroundVerticalAxis(float verticalRotationAxis)
        {
            transform.RotateAround(_target.position, Vector3.up,
                verticalRotationAxis * _rotationVelocity * Time.deltaTime);
        }

        private void RotateAroundHorizontalAxis(float horizontalRotationAxis)
        {
            float currentAngle = transform.eulerAngles.x;
            currentAngle = (currentAngle > 180) ? currentAngle - 360 : currentAngle;
            float deltaAngle = horizontalRotationAxis * _rotationVelocity * Time.deltaTime;
            float targetAngle = Mathf.Abs(currentAngle + deltaAngle);
            if (targetAngle > _maximumAngleAroundHorizontalAxis)
            {
                deltaAngle = Mathf.Sign(deltaAngle) * (_maximumAngleAroundHorizontalAxis - Mathf.Abs(currentAngle));
            }

            transform.RotateAround(_target.position, transform.right, deltaAngle);
        }
    }
}