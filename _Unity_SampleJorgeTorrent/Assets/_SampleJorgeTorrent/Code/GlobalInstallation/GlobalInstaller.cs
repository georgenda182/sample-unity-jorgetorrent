using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using AYellowpaper;
using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.GlobalInstallation
{
    public class GlobalInstaller : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _enemyTransform;
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

            PlayerTransformWrapper playerTransformWrapper = new PlayerTransformWrapper();
            playerTransformWrapper.Value = _playerTransform;

            EnemyTransformWrapper enemyTransformWrapper = new EnemyTransformWrapper();
            enemyTransformWrapper.Value = _enemyTransform;

            _gameInputControls = new GameInputControls();
            _gameInputControls.Enable();

            _globalServiceLocator.RegisterService(_camera);
            _globalServiceLocator.RegisterService(playerTransformWrapper);
            _globalServiceLocator.RegisterService(enemyTransformWrapper);
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
            _camera.transform.parent = null;
            _playerTransform.parent = null;
            _enemyTransform.parent = null;

            Destroy(gameObject);
        }
    }
}