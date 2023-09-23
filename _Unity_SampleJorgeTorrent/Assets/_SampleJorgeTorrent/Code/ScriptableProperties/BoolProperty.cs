using UniRx;
using Unity.Collections;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.ScriptableProperties
{
    [CreateAssetMenu(menuName = "SampleJorgeTorrent/ScriptableProperties/Bool")]
    public class BoolProperty : ScriptableObject
    {
        [SerializeField] private BoolReactiveProperty _property;
        public BoolReactiveProperty Property => _property;
    }
}