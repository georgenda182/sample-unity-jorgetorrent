using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ObjectPoolPattern;
using System.Collections;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Collectables
{
    public class PooledCollectable : PooledObject
    {
        /*internal override void OnTakenFromPool()
        {

        }*/

        void PooledObject.OnLeftOnPool()
        {
            throw new System.NotImplementedException();
        }

        void PooledObject.OnTakenFromPool()
        {
            throw new System.NotImplementedException();
        }

        void PooledObject.StorePool<T>(ObjectPool<T> pool)
        {
            throw new System.NotImplementedException();
        }
    }
}