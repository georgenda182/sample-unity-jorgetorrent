using System.Collections.Generic;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Services
{
    [RequireComponent(typeof(Collider))]
    public class GroundTriggerDetector : MonoBehaviour, GroundDetector
    {
        public event GroundDetector.GroundCallback OnJustGrounded;
        public event GroundDetector.GroundCallback OnJustUngrounded;

        private HashSet<int> currentGroundsDetected = new HashSet<int>();

        private void OnTriggerEnter(Collider other)
        {
            AddGroundAndCallEventIfConvenient(other.GetInstanceID());
        }

        private void AddGroundAndCallEventIfConvenient(int groundId)
        {
            bool wasNotGrounded = currentGroundsDetected.Count == 0;

            currentGroundsDetected.Add(groundId);

            if (wasNotGrounded && OnJustGrounded != null)
            {
                OnJustGrounded();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveGroundAndCallEventIfConvenient(other.GetInstanceID());
        }

        private void RemoveGroundAndCallEventIfConvenient(int groundId)
        {
            bool wasGrounded = currentGroundsDetected.Count == 1;

            currentGroundsDetected.Remove(groundId);

            if (wasGrounded && OnJustUngrounded != null)
            {
                OnJustUngrounded();
            }
        }
    }
}