using UniRx;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Utilities.ScriptableProperties
{
    [CreateAssetMenu(menuName = "SampleJorgeTorrent/ScriptableProperties/String")]
    public class StringProperty : ScriptableObject
    {
        [SerializeField] private StringReactiveProperty _property;
        public StringReactiveProperty Property => _property;
    }
}