using _SampleJorgeTorrent.Code.DesignPatterns;
using _SampleJorgeTorrent.Code.PlayerController.Actions;
using _SampleJorgeTorrent.Code.PlayerController.Services;
using UnityEngine;

namespace Assets._SampleJorgeTorrent.Code.PlayerController.Actions
{
    public class FallAction : PlayerAction
    {
        private GroundDetector _groundEventsDispatcher;
        private Animator _playerAnimator;

        protected override void Configure(ServiceLocator playerServiceLocator)
        {
            _groundEventsDispatcher = playerServiceLocator.GetService<GroundDetector>();
            _playerAnimator = playerServiceLocator.GetService<Animator>();
        }

        protected override void DeterminePerformanceConditions()
        {
            _groundEventsDispatcher.OnJustGrounded += TryCancellation;
            _groundEventsDispatcher.OnJustUngrounded += TryPerformance;
        }

        protected override void Perform()
        {
            _playerAnimator.SetBool("IsFalling", true);
        }

        protected override void Cancel()
        {
            _playerAnimator.SetBool("IsFalling", false);
        }
    }
}