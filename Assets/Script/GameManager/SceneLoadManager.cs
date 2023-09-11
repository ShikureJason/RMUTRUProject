using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader = default;
    [SerializeField] private SceneLoadSO _gameManager = default;

    [Header("Event Emitter")]
    [SerializeField] private BoolEventSO _loadingScreenEventEmitter = default;
    [SerializeField] private VoidEventSO _sceneHasLoadedEventEmitter = default;

    [Header("Event Listener")]
    [SerializeField] private LoadSceneEventSO _loadSceneEventListener = default;
#if UNITY_EDITOR
    [SerializeField] private LoadSceneEventSO _loadSceneEditEventListener = default;
#endif

    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
    private AsyncOperationHandle<SceneInstance> _gameManagerOpearationHandle;
    private SceneInstance _gameManagerInstance = new SceneInstance();
    private SceneLoadSO _currentLoadedScene;
    private SceneLoadSO _loadScene;
    private bool _isLoading = false;
    private float _waitTime = 0.5f;

    private void OnEnable()
    {
        _loadSceneEventListener.OnEventRaised += LoadScene;
#if UNITY_EDITOR
        _loadSceneEditEventListener.OnEventRaised += LoadSceneEdit;
#endif

    }

#if UNITY_EDITOR
    private void LoadSceneEdit(SceneLoadSO sceneRef)
    {
        _currentLoadedScene = sceneRef;
        /*if (_currentLoadedScene.SceneType == SceneType.ChangeScene)
        {
            _gameManagerOpearationHandle = _gameManager.Scene.LoadSceneAsync(LoadSceneMode.Additive, true);
            _gameManagerOpearationHandle.WaitForCompletion();
            _gameManagerInstance = _gameManagerOpearationHandle.Result;
            _sceneHasLoadedEventEmitter.RaiseEvent();
        }*/
        _sceneHasLoadedEventEmitter.RaiseEvent();
    }
#endif

    private void LoadScene(SceneLoadSO sceneRef)
    {
        if (_isLoading)
            return;
        _isLoading = true;
        inputReader.DisableAllInput();
        _loadScene = sceneRef;
        if (_currentLoadedScene != null) //would be null if the game was started in Initialisation
        {
            if (_currentLoadedScene.Scene.OperationHandle.IsValid())
            {
                //Unload the scene through its AssetReference, i.e. through the Addressable system
                _currentLoadedScene.Scene.UnLoadScene();
            }
#if UNITY_EDITOR
            else
            {
                //Only used when, after a "cold start", the player moves to a new scene
                //Since the AsyncOperationHandle has not been used (the scene was already open in the editor),
                //the scene needs to be unloaded using regular SceneManager instead of as an Addressable
                SceneManager.UnloadSceneAsync(_currentLoadedScene.Scene.editorAsset.name);
            }
#endif
        }
        _loadingScreenEventEmitter.RaiseEvent(true);
        _loadingOperationHandle = _loadScene.Scene.LoadSceneAsync(LoadSceneMode.Additive, true, 5);
        _loadingOperationHandle.Completed += OnNewSceneLoaded;
    }

    private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _currentLoadedScene = _loadScene;

        Scene s = obj.Result.Scene;
        SceneManager.SetActiveScene(s);
        LightProbes.TetrahedralizeAsync();

        _isLoading = false;
        StartCoroutine(DelayLoadScene());


    }

    IEnumerator DelayLoadScene()
    {
        yield return new WaitForSeconds(_waitTime);
        _loadingScreenEventEmitter.RaiseEvent(false);
        _sceneHasLoadedEventEmitter.RaiseEvent();
    }
}
