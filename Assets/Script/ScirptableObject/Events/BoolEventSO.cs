using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEvent", menuName = "Events/BoolEvent")]
public class BoolEventSO : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool expression)
    {
       OnEventRaised?.Invoke(expression);
    }
}
