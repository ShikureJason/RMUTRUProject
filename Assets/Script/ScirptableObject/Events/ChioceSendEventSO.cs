using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChioceSendEvent", menuName = "Events/Chioce/Send")]
public class ChioceSendEventSO : ScriptableObject
{
    public UnityAction<List<Chioce>> OnEventRaised;

    public void RaiseEvent(List<Chioce> list)
    {
        OnEventRaised?.Invoke(list);
    }
}
