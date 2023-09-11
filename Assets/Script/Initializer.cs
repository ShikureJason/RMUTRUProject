using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    [SerializeField] private SceneLoadSO _mainMenuScene = default;
    [SerializeField] private SceneLoadSO _gameManager = default;

    [Header("Event Emitter")]
    [SerializeField] private LoadSceneEventSO _loadSceneEventEmitter = default;

    private void Start()
    {
        _gameManager.Scene.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadSceneEvent;
    }

    private void LoadSceneEvent(AsyncOperationHandle<SceneInstance> obj)
    {
        _loadSceneEventEmitter.RaiseEvent(_mainMenuScene);

        //unload InitializationScene when GamePlayScnen has loaded
        SceneManager.UnloadSceneAsync(0);
    }
}
