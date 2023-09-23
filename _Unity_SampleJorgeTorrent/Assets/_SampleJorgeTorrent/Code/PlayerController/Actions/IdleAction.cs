using _SampleJorgeTorrent.Code.DesignPatterns.ServiceLocatorPattern;
using _SampleJorgeTorrent.Code.ScriptableProperties;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.Actions
{
    public class IdleAction : PlayerAction
    {
        private Animator _playerAnimator;

        protected override void StorePlayerServices(ServiceLocator playerServiceLocator)
        {
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DefinePerformanceConditions()
        {
            foreach (BoolProperty prohibitorState in _prohibitorStates)
            {
                prohibitorState.Property.Subscribe(ActivatePerformanceWhenNoProhibitorStatesActive);
            }
        }

        private void ActivatePerformanceWhenNoProhibitorStatesActive(bool prohibitorStateIsActive)
        {
            if (prohibitorStateIsActive)
            {
                return;
            }
            PerformIfAllowed();
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsIdle", true);
        }

        protected override void Cancel()
        {
            _playerAnimator.SetBool("IsIdle", false);
        }
    }
}