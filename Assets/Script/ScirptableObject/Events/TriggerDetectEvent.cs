using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InterractObjectEvent", menuName = "Events/InterractObjectEvent")]
public class TriggerDetectEvent : ScriptableObject
{
    public UnityAction<bool, GameObject> OnEventRaised;

    public void RaiseEvent(bool isTrigger, GameObject obj)
    {
        OnEventRaised?.Invoke(isTrigger, obj);
    }
}
