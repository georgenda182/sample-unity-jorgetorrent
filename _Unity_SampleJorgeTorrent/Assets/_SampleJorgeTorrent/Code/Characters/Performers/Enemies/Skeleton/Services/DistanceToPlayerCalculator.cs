using _SampleJorgeTorrent.Code.Characters.Performers.Player.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services
{
    public class DistanceToPlayerCalculator : MonoBehaviour, ServicesConsumer
    {
        private Transform _playerTransform;

        private FloatReactiveProperty _distanceToPlayer;
        public FloatReactiveProperty DistanceToPlayer => _distanceToPlayer;

        public void Install(ServiceLocator enemyServiceLocator)
        {
            _playerTransform = enemyServiceLocator.GetService<PlayerGlobalServices>().Transform;
            _distanceToPlayer = new FloatReactiveProperty();
        }

        private void Update()
        {
            SetDistanceToPlayer();
        }

        private void SetDistanceToPlayer()
        {
            _distanceToPlayer.Value = Vector3.Distance(_playerTransform.position, transform.position);
        }
    }
}