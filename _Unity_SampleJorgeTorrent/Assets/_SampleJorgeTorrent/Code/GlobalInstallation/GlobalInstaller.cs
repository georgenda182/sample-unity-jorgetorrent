using _SampleJorgeTorrent.Code.DesignPatterns.ServiceLocatorPattern;
using AYellowpaper;
using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.GlobalInstallation
{
    public class GlobalInstaller : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private Camera _camera;
        private GameInputControls _gameInputControls;

        [Header("Consumers")]
        [SerializeField] private List<InterfaceReference<ServicesConsumer>> _servicesConsumers;

        private ServiceLocator _globalServiceLocator;

        private void Start()
        {
            ConfigureServiceLocator();
            InstallServicesConsumers();
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
    }
}