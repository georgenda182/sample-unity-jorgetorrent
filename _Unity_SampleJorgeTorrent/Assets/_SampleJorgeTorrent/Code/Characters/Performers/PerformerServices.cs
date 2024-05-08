using _SampleJorgeTorrent.Code.HealthSystem;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers
{
    public class PerformerServices<T> where T : Performer
    {
        public Transform Transform { get; set; }

        public Health Health { get; set; }
    }
}