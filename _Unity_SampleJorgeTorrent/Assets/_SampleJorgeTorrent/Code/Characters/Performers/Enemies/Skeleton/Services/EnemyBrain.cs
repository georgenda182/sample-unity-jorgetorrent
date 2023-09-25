using _SampleJorgeTorrent.Code.Utilities;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services
{
    public class EnemyBrain : MonoBehaviour, ServicesConsumer
    {
        public delegate void EnemyCallback();

        public event EnemyCallback OnPlayerAtLargeDistance;
        public event EnemyCallback OnPlayerAtMidDistance;
        public event EnemyCallback OnPlayerAtLowDistance;

        private Transform _playerTransform;

        [SerializeField] private float _lowDistance = 1.5f;
        [SerializeField] private float _midDistance = 10f;

        private FloatReactiveProperty _distanceToPlayer;
        public FloatReactiveProperty DistanceToPlayer => _distanceToPlayer;

        private EnemyCallback _lastEventCalled;

        public void Install(ServiceLocator enemyServiceLocator)
        {
            _playerTransform = enemyServiceLocator.GetService<PlayerTransformWrapper>().Value;
            _distanceToPlayer = new FloatReactiveProperty();
        }

        private void Update()
        {
            SetDistanceToPlayer();
            ManageEventsCallsByDistance();
        }

        private void SetDistanceToPlayer()
        {
            _distanceToPlayer.Value = Vector3.Distance(_playerTransform.position, transform.position);
        }

        private void ManageEventsCallsByDistance()
        {
            EnemyCallback eventToCall = ReturnCallbackDependingOnDistanceToPlayer();
            if (_lastEventCalled != eventToCall)
            {
                if (eventToCall != null)
                {
                    eventToCall();
                }
                _lastEventCalled = eventToCall;
            }
        }

        private EnemyCallback ReturnCallbackDependingOnDistanceToPlayer()
        {
            float distanceToPlayer = _distanceToPlayer.Value;

            bool playerIsAtLargeDistance = distanceToPlayer > _midDistance;
            bool playerIsAtMidDistance = distanceToPlayer > _lowDistance && distanceToPlayer <= _midDistance;

            if (playerIsAtLargeDistance)
            {
                return OnPlayerAtLargeDistance;
            }
            else if (playerIsAtMidDistance)
            {
                return OnPlayerAtMidDistance;
            }
            else
            {
                return OnPlayerAtLowDistance;
            }
        }
    }
}