using _SampleJorgeTorrent.Code.DesignPatterns;
using _SampleJorgeTorrent.Code.ScriptableProperties;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.Actions
{
    public abstract class PlayerAction : MonoBehaviour
    {
        [SerializeField] protected BoolProperty _state;
        [SerializeField] protected List<BoolProperty> _prohibitorStates;

        protected bool IsActive
        {
            get => _state.Property.Value;
        }

        protected bool IsInactive
        {
            get => !IsActive;
        }

        protected bool IsAllowedToBePerformed
        {
            get
            {
                foreach (BoolProperty prohibitorState in _prohibitorStates)
                {
                    bool actionIsNotAllowed = prohibitorState.Property.Value;
                    if (actionIsNotAllowed)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        protected bool IsNotAllowedToBePerformed
        {
            get => !IsAllowedToBePerformed;
        }

        public void Install(ServiceLocator playerServiceLocator)
        {
            Configure(playerServiceLocator);
            DeterminePerformanceConditions();
            DetermineCancellationWhenNotAllowed();
        }

        protected abstract void Configure(ServiceLocator playerServiceLocator);
        protected abstract void DeterminePerformanceConditions();

        private void DetermineCancellationWhenNotAllowed()
        {
            foreach (BoolProperty prohibitorState in _prohibitorStates)
            {
                prohibitorState.Property.Subscribe(OnProhibitorStateJustChanged);
            }
        }

        private void OnProhibitorStateJustChanged(bool prohibitorStateIsActive)
        {
            if (IsNotAllowedToBePerformed)
            {
                TryCancellation();
            }
        }
        protected void TryCancellation()
        {
            if (IsActive)
            {
                DeactivateState();
                Cancel();
            }
        }
        private void DeactivateState()
        {
            _state.Property.Value = false;
        }
        protected abstract void Cancel();

        protected void TryPerformance()
        {
            if (IsAllowedToBePerformed && IsInactive)
            {
                ActivateState();
                Perform();
            }
        }
        private void ActivateState()
        {
            _state.Property.Value = true;
        }
        protected abstract void Perform();

        private void OnDestroy()
        {
            DeactivateState();
        }
    }
}