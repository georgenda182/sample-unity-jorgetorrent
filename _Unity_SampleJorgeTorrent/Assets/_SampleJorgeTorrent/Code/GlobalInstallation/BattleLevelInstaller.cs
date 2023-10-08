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
    public class BattleLevelInstaller : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Health _playerHealth;
        [SerializeField] private HealthVisualizer _playerHealthVisualizer;
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private Health _enemyHealth;
        [SerializeField] private HealthVisualizer _enemyHealthVisualizer;
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

            PlayerGlobalServices playerGlobalServices = new PlayerGlobalServices();
            playerGlobalServices.Transform = _playerTransform;
            _playerHealth.Initialize();
            playerGlobalServices.Health = _playerHealth;
            _playerHealthVisualizer.Initialize();

            EnemyGlobalServices enemyGlobalServices = new EnemyGlobalServices();
            enemyGlobalServices.Transform = _enemyTransform;
            _enemyHealth.Initialize();
            enemyGlobalServices.Health = _enemyHealth;
            _enemyHealthVisualizer.Initialize();

            _gameInputControls = new GameInputControls();
            _gameInputControls.Menus.Enable();

            _globalServiceLocator.RegisterService(_camera);
            _globalServiceLocator.RegisterService(playerGlobalServices);
            _globalServiceLocator.RegisterService(enemyGlobalServices);
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