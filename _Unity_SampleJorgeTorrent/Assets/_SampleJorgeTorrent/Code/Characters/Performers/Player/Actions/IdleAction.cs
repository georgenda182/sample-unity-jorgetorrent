using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class IdleAction : PerformerAction
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