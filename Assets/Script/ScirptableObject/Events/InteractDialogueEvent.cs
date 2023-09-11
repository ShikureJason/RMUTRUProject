using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InteractDialogueEvent", menuName = "Events/InteractDialogue")]
public class InteractDialogueEvent : ScriptableObject
{
    public UnityAction<ActorSO> OnEventRaised;

    public void RaiseEvent(ActorSO actor)
    {
        OnEventRaised?.Invoke(actor);
    }
}
