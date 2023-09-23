using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Services
{
    public class PlayerMaths : ServicesConsumer
    {
        private GameInputControls _playerInputControls;
        private Camera _playerCamera;

        public Vector3 EulerRotation
        {
            get;
            private set;
        }

        public void Install(ServiceLocator playerServiceLocator)
        {
            _playerInputControls = playerServiceLocator.GetService<GameInputControls>();
            _playerCamera = playerServiceLocator.GetService<Camera>();

            _playerInputControls.Player.Move.performed += context => ConvertMoveInputValueToEulerRotation(context.ReadValue<Vector2>());
        }

        private void ConvertMoveInputValueToEulerRotation(Vector2 inputValue)
        {
            float angleOffset = inputValue.y < 0 ? 180 : 0;
            angleOffset += _playerCamera.transform.rotation.eulerAngles.y;
            float angleByDirection;

            if (inputValue.y != 0)
            {
                angleByDirection = Mathf.Rad2Deg * Mathf.Atan(inputValue.x / inputValue.y);
            }
            else
            {
                if (inputValue.x > 0)
                {
                    angleByDirection = 90f;
                }
                else if (inputValue.x < 0)
                {
                    angleByDirection = -90f;
                }
                else
                {
                    angleByDirection = 0f;
                }
            }

            EulerRotation = Vector3.up * (angleOffset + angleByDirection);
        }
    }
}