using _SampleJorgeTorrent.Code.Utilities.ScriptableProperties;
using AYellowpaper;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace _SampleJorgeTorrent.Code.LoadingProcess
{
    public class LoadingScreenLogics : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<LoadingProgressVisualizer> _progressVisualizer;

        private string _sceneToLoad;

        private void Start()
        {
            _sceneToLoad = SceneLoader.Instance.NextSceneToLoad;
            LoadScene();
        }

        private void LoadScene()
        {
            CheckPreconditions();
            ShowProgressVisualization();
            StartLoadingProcess();
        }

        private void ShowProgressVisualization()
        {
            _progressVisualizer.Value.Show();
        }

        private void StartLoadingProcess()
        {
            _progressVisualizer.Value.OnShown += () => StartCoroutine(LoadSceneAsync());
        }

        private void CheckPreconditions()
        {
            int sceneBuildIndex = SceneUtility.GetBuildIndexByScenePath(_sceneToLoad);

            Assert.AreNotEqual(-1, sceneBuildIndex, $"Scene { _sceneToLoad } not registered in build settings");
        }

        private IEnumerator LoadSceneAsync()
        {
            AsyncOperation loadingOperation;
            loadingOperation = SceneManager.LoadSceneAsync(_sceneToLoad);
            loadingOperation.priority = (int) ThreadPriority.Low;
            loadingOperation.allowSceneActivation = false;

            while (loadingOperation.progress < 0.9f)
            {
                _progressVisualizer.Value.SetProgress(loadingOperation.progress);
                yield return null;
            }

            _progressVisualizer.Value.SetProgress(1);
            _progressVisualizer.Value.Hide();
            _progressVisualizer.Value.OnHidden += () => loadingOperation.allowSceneActivation = true;

            GC.Collect();
        }
    }
}