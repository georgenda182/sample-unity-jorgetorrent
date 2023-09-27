using _SampleJorgeTorrent.Code.Characters.Performers;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.HealthSystem
{
    public class EnemyWeapon : Weapon, ServicesConsumer
    {
        private AnimationEventsDispatcher _enemyAnimationEventsDispatcher;

        public void Install(ServiceLocator enemyServiceLocator)
        {
            _enemyAnimationEventsDispatcher = enemyServiceLocator.GetService<AnimationEventsDispatcher>();
            SubscribeVolumeEnablingToAttackAnimation();
        }

        private void SubscribeVolumeEnablingToAttackAnimation()
        {
            _enemyAnimationEventsDispatcher.SubscribeEventCallbackToAnimation("OnAttackStarted", EnableHitVolume);
            _enemyAnimationEventsDispatcher.SubscribeEventCallbackToAnimation("OnAttackFinished", DisableHitVolume);
        }

        public void EnableHitVolume()
        {
            _hitVolume.enabled = true;
        }

        public void DisableHitVolume()
        {
            _hitVolume.enabled = false;
        }
    }
}