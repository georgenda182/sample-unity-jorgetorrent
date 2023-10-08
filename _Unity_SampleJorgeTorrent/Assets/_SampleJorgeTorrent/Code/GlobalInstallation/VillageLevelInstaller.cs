using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.UI;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using AYellowpaper;
using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.GlobalInstallation
{
    public class VillageLevelInstaller : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;
        private GameInputControls _gameInputControls;

        [Header("Consumers")]
        [SerializeField] private List<InterfaceReference<ServicesConsumer>> _servicesConsumers;

        private ServiceLocator _globalServiceLocator;

        private void Start()
        {
            ConfigureServiceLocator();
            InstallServicesConsumers();
            ReparentChildrenToRootAndDestroySelf();
        }

        private void ConfigureServiceLocator()
        {
            _globalServiceLocator = new ServiceLocator();

            _gameInputControls = new GameInputControls();
            _gameInputControls.Enable();

            _globalServiceLocator.RegisterService(_camera);
            _globalServiceLocator.RegisterService(_gameInputControls);
        }

        private void InstallServicesConsumers()
        {
            foreach (var servicesConsumer in _servicesConsumers)
            {
                servicesConsumer.Value.Install(_globalServiceLocator);
            }
        }

        private void ReparentChildrenToRootAndDestroySelf()
        {
            List<Transform> children = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                children.Add(transform.GetChild(i));
            }
            foreach (Transform child in children)
            {
                child.SetParent(null);
            }

            Destroy(gameObject);
        }
    }
}