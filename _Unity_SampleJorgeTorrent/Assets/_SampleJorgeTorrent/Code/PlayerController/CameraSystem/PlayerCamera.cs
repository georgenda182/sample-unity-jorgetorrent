using System.Collections;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.CameraSystem
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _maximumAngleAroundHorizontalAxis = 45;

        [SerializeField] private float _offsetFromPlayer = 12;

        [SerializeField] private float _followSpeed = 0.125f;
        [SerializeField] private float _rotationVelocity = 36;

        public GameInputControls inputPlayer;

        private bool _isRotating;

        private Vector3 offsetFromPlayerVector => transform.forward * _offsetFromPlayer;

        private IEnumerator Start()
        {
            yield return null;
            inputPlayer.Player.Camera.started += context => { _isRotating = true; Debug.Log("Rotating"); };
            inputPlayer.Player.Camera.canceled += context => { _isRotating = false; Debug.Log("Cancel rotation"); };
        }

        private void LateUpdate()
        {
            if (_isRotating)
            {
                RotateAroundPlayer();
            }
            Vector3 desiredPosition = _target.position - offsetFromPlayerVector;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _followSpeed);
            transform.position = smoothedPosition;
        }

        private void RotateAroundPlayer()
        {
            Vector2 rotation = inputPlayer.Player.Camera.ReadValue<Vector2>();

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