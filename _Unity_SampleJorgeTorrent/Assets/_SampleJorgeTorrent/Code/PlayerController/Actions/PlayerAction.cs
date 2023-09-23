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

        public void Install(ServiceLocator playerServiceLocator)
        {
            Configure(playerServiceLocator);
            DefinePerformanceConditions();
            DetermineCancellationWhenNotAllowed();
        }

        protected abstract void Configure(ServiceLocator playerServiceLocator);
        protected abstract void DefinePerformanceConditions();

        private void DetermineCancellationWhenNotAllowed()
        {
            foreach (BoolProperty prohibitorState in _prohibitorStates)
            {
                prohibitorState.Property.Subscribe(OnProhibitorStateJustChanged);
            }
        }

        private void OnProhibitorStateJustChanged(bool prohibitorStateIsActive)
        {
            if (prohibitorStateIsActive)
            {
                CancelIfActive();
            }
        }
        protected void CancelIfActive()
        {
            if (IsActive)
            {
                Cancel();
                DeactivateState();
            }
        }
        private void DeactivateState()
        {
            _state.Property.Value = false;
        }
        protected abstract void Cancel();

        protected void PerformIfAllowed()
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