using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChioceReceiveEvent", menuName = "Events/Chioce/Recive")]
public class ChioceReceiveEventSO : ScriptableObject
{
    public UnityAction<DialogueStepSO, ChioceState> OnEventRiased;

    public void RaiseEvent(DialogueStepSO dialogue, ChioceState state)
    {
        OnEventRiased?.Invoke(dialogue, state);
    }
}
