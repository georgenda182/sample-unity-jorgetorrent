using UnityEngine;
using UnityEngine.SceneManagement;

namespace _SampleJorgeTorrent.Code.LoadingProcess
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader _instance;
        public static SceneLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject sceneLoaderGameObject = new GameObject("SceneLoader");
                    SceneLoader sceneLoader = sceneLoaderGameObject.AddComponent<SceneLoader>();
                    _instance = sceneLoader;
                    DontDestroyOnLoad(sceneLoaderGameObject);
                }
                return _instance;
            }
        }

        public string NextSceneToLoad
        {
            get;
            private set;
        }

        public void LoadScene(string sceneToLoad)
        {
            NextSceneToLoad = sceneToLoad;
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}