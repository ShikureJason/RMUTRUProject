using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "StartDialogueEvent", menuName = "Events/StartDialogue")]
public class StartDialogueEvent : ScriptableObject
{
    public UnityAction<LocalizedString, ActorSO> OnEventRaised;

    public void RaiseEvent(LocalizedString dialogue, ActorSO actor)
    {
        OnEventRaised?.Invoke(dialogue, actor);
    }
}
