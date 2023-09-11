using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class InitializerStartScene : MonoBehaviour
{

    [SerializeField] private InputReaderSO _inputReader = default;
    [SerializeField] private SceneLoadSO _currentScene = default;
#if UNITY_EDITOR
    [SerializeField] private SceneLoadSO _gameManager = default;

    [Header("Event Emitter")]
    [SerializeField] private LoadSceneEventSO _loadSceneEventEditorEmitter = default;

#endif
    [Header("Event Listener")]
    [SerializeField] private VoidEventSO _sceneHasLoadedEventListener = default;

#if UNITY_EDITOR

    private bool _sceneManageNotLoaded = false;
    private void Awake()
    {
        if (!SceneManager.GetSceneByName(_gameManager.Scene.editorAsset.name).isLoaded)
        {
            _sceneManageNotLoaded = true;
        }
    }
#endif

    private void Start()
    {
#if UNITY_EDITOR
        if (_sceneManageNotLoaded)
            _gameManager.Scene.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadSceneEvent;
#endif  
            _sceneHasLoadedEventListener.OnEventRaised += onStart;
    }
#if UNITY_EDITOR
    private void LoadSceneEvent(AsyncOperationHandle<SceneInstance> obj)
    {
        _loadSceneEventEditorEmitter.RaiseEvent(_currentScene);
    }

#endif

    private void onStart()
    {
        switch (_currentScene.SceneType)
        {
            case SceneType.ChangeScene:
                _inputReader.EnableGameplay();
                break;
            default:
                _inputReader.DisableAllInput();
                break;
        }
    }
}
