using UnityEngine;

namespace _SampleJorgeTorrent.Code.Collectables
{
    [RequireComponent(typeof(Collider))]
    public abstract class Collectable : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            bool hasCollidedWithPlayer = other.gameObject.layer == LayerMask.NameToLayer("Player");
            if (hasCollidedWithPlayer)
            {
                Collect();
                Destroy(gameObject);
            }
        }

        protected abstract void Collect();
    }
}