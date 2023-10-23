using UnityEngine;

namespace _SampleJorgeTorrent.Code.LoadingProcess
{
    public class PreloadLevelLogics : MonoBehaviour
    {
        [SerializeField] private string _defaultSceneToLoad = "VillageLevel";

        private void Start()
        {
            SceneLoader.Instance.LoadScene(_defaultSceneToLoad);
        }
    }
}