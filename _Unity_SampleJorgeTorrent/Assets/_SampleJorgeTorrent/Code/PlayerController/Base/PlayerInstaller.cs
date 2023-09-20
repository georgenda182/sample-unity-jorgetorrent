using _SampleJorgeTorrent.Code.DesignPatterns;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.Base
{
    public class PlayerInstaller : MonoBehaviour
    {
        [Header("Player services")]
        [SerializeField] private Rigidbody _playerRigidbody;

        private ServiceLocator _playerServiceLocator;

        private void Start()
        {
            ConfigureServiceLocator();
        }

        private void ConfigureServiceLocator()
        {
            _playerServiceLocator = new ServiceLocator();

            _playerServiceLocator.RegisterService(_playerRigidbody);
        }
    }
}