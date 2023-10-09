using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Collectables
{
    public class Gold : Collectable
    {
        [SerializeField] private IntProperty _playerGold;
        [SerializeField] private int _goldToAdd = 25;

        protected override void Collect()
        {
            _playerGold.Property.Value += _goldToAdd;
        }
    }
}