using UnityEngine;

public class StartNextScene : MonoBehaviour
{
    [SerializeField] private SceneLoadSO _startNextScene = default;

    [Header("Event Emitter")]
    [SerializeField] private LoadSceneEventSO _loadSceneEventEmitter = default;

    [Header("Event Listener")]
    [SerializeField] private VoidEventSO _startNextSceneEmitter = default;

    private void OnEnable()
    {
        _startNextSceneEmitter.OnEventRaised += LoadSceneEvent;
    }
    private void LoadSceneEvent()
    {
        _loadSceneEventEmitter.RaiseEvent(_startNextScene);
    }
}
