using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Services
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerJumpAvoiderOnInteractionAbility : MonoBehaviour
    {
        [SerializeField] private BoolProperty _canInteractWithEntity;

        private void OnTriggerEnter(Collider other)
        {
            bool hasTriggeredInteractiveEntity = other.gameObject.layer == LayerMask.NameToLayer("InteractiveEntity");

            if (hasTriggeredInteractiveEntity)
            {
                _canInteractWithEntity.Property.Value = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            bool hasExitInteractiveEntity = other.gameObject.layer == LayerMask.NameToLayer("InteractiveEntity");

            if (hasExitInteractiveEntity)
            {
                _canInteractWithEntity.Property.Value = false;
            }
        }
    }
}