using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/VoidEvent")]
public class VoidEventSO : ScriptableObject
{
	public UnityAction OnEventRaised;

	public void RaiseEvent()
	{
			OnEventRaised?.Invoke();
	}
}
