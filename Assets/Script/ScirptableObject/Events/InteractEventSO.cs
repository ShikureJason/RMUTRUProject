using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InteractEvent", menuName = "Events/InteractEvent")]
public class InteractEventSO : ScriptableObject
{
    public UnityAction<InteractType, bool> OnEventRaised;

    public void RiaseEvent(InteractType type, bool isInteract)
    {
        OnEventRaised?.Invoke(type, isInteract);
    }
}
