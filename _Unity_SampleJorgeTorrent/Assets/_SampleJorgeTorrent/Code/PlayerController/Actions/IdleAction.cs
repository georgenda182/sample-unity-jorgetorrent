using _SampleJorgeTorrent.Code.DesignPatterns;
using _SampleJorgeTorrent.Code.ScriptableProperties;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.Actions
{
    public class IdleAction : PlayerAction
    {
        private Animator _playerAnimator;

        protected override void Configure(ServiceLocator playerServiceLocator)
        {
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DeterminePerformanceConditions()
        {
            foreach (BoolProperty prohibitorState in _prohibitorStates)
            {
                prohibitorState.Property.Subscribe(ActivatePerformanceWhenNoProhibitorStatesActive);
            }
        }

        private void ActivatePerformanceWhenNoProhibitorStatesActive(bool prohibitorStateIsActive)
        {
            TryPerformance();
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