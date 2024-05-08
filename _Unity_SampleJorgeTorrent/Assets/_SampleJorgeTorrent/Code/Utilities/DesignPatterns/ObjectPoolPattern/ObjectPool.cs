using UnityEngine;
using UnityEngine.Pool;

namespace _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ObjectPoolPattern
{
    public class ObjectPool<T> where T : MonoBehaviour, PooledObject
    {
        private T _prefab;
        private IObjectPool<T> _pool;

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
            _pool = new UnityEngine.Pool.ObjectPool<T>(CreateInstance, OnObjectTaken, OnObjectRecycled);
        }

        public T GetObject()
        {
            return _pool.Get();
        }

        public void RecycleObject(T pooledObject)
        {
            _pool.Release(pooledObject);
        }

        private T CreateInstance()
        {
            T instance = Object.Instantiate(_prefab);
            instance.StorePool(this);
            return instance;
        }

        private void OnObjectTaken(T pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
            pooledObject.OnTakenFromPool();
        }

        private void OnObjectRecycled(T pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            pooledObject.OnLeftOnPool();
        }
    }
}