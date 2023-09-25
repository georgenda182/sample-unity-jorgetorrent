using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UniRx;
using UnityEngine;

namespace Assets._SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services
{
    public class EnemyAnimationController : ServicesConsumer
    {
        private EnemyBrain _enemyBrain;
        private Animator _enemyAnimator;

        public void Install(ServiceLocator enemyServiceLocator)
        {
            StoreServices(enemyServiceLocator);
            SubscribeAnimationSwitchToDistanceToPlayerChange();
        }

        private void StoreServices(ServiceLocator enemyServiceLocator)
        {
            _enemyBrain = enemyServiceLocator.GetService<EnemyBrain>();
            _enemyAnimator = enemyServiceLocator.GetService<Animator>();
        }

        private void SubscribeAnimationSwitchToDistanceToPlayerChange()
        {
            _enemyBrain.DistanceToPlayer.Subscribe(SetAnimatorDistanceToPlayer);
        }

        private void SetAnimatorDistanceToPlayer(int distanceToPlayer)
        {
            _enemyAnimator.SetInteger("DistanceToPlayer", distanceToPlayer);
        }
    }
}