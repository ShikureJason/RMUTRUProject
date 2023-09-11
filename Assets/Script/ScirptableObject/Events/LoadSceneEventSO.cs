using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LoadSceneEvent", menuName = "Events/LoadScene")]
public class LoadSceneEventSO : ScriptableObject
{
    public UnityAction<SceneLoadSO> OnEventRaised;

    public void RaiseEvent(SceneLoadSO scene)
    {
        OnEventRaised?.Invoke(scene);
    }
}
