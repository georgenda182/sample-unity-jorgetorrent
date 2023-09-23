using UnityEngine;

namespace _SampleJorgeTorrent.Code.PlayerController.Services
{
    public class GroundTriggerDetector : MonoBehaviour, GroundDetector
    {
        public event GroundDetector.GroundCallback OnJustGrounded;
        public event GroundDetector.GroundCallback OnJustUngrounded;

        private void OnTriggerEnter(Collider other)
        {
            if (OnJustGrounded != null)
            {
                OnJustGrounded();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (OnJustUngrounded != null)
            {
                OnJustUngrounded();
            }
        }
    }
}