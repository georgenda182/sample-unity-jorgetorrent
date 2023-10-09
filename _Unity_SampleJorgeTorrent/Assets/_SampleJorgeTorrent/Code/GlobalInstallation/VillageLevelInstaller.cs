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
        [SerializeField] private InteractionMark _interactionMark;
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
            _gameInputControls.Player.Enable();

            _globalServiceLocator.RegisterService(_camera);
            _globalServiceLocator.RegisterService(_interactionMark);
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