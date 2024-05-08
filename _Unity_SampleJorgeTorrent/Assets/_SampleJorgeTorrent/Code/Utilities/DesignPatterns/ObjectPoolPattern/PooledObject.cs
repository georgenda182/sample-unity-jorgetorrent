using UnityEngine;

namespace _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ObjectPoolPattern
{
    public interface PooledObject
    {
        internal void StorePool<T>(ObjectPool<T> pool) where T : MonoBehaviour, PooledObject;

        internal void OnTakenFromPool();
        internal void OnLeftOnPool();
    }
}