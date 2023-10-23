using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.LoadingProcess
{
    [RequireComponent(typeof(Collider))]
    public class TriggerLoading : MonoBehaviour, ServicesConsumer
    {
        [SerializeField] private string _sceneToLoad = "BattleLevel";
        private GameInputControls _inputControls;

        public void Install(ServiceLocator serviceLocator)
        {
            _inputControls = serviceLocator.GetService<GameInputControls>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SceneLoader.Instance.LoadScene(_sceneToLoad);
                _inputControls.Player.Disable();
                gameObject.SetActive(false);
            }
        }
    }
}