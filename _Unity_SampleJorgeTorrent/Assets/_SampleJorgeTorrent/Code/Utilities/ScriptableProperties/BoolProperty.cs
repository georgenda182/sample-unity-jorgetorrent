using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Utilities.ScriptableProperties
{
    [CreateAssetMenu(menuName = "SampleJorgeTorrent/ScriptableProperties/Bool")]
    public class BoolProperty : ScriptableObject
    {
        [SerializeField] private BoolReactiveProperty _property;
        public BoolReactiveProperty Property => _property;
    }
}