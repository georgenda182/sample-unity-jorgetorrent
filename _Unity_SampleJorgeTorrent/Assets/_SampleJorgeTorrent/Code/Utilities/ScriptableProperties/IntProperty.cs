using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Utilities.ScriptableProperties
{
    [CreateAssetMenu(menuName = "SampleJorgeTorrent/ScriptableProperties/Int")]
    public class IntProperty : ScriptableObject
    {
        [SerializeField] private IntReactiveProperty _property;
        public IntReactiveProperty Property => _property;
    }
}