using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers
{
    public abstract class Performer : MonoBehaviour
    {
        [Header("Performer actions")]
        [SerializeField] private List<PerformerAction> _actions;

        protected ServiceLocator _performerServiceLocator = new ServiceLocator();

        protected void InstallActions()
        {
            foreach (PerformerAction action in _actions)
            {
                action.Install(_performerServiceLocator);
            }
        }
    }
}