using UnityEngine;
using UnityEngine.Localization;
public enum ActorID
{
    T, //Test
	P, //Player
}

[CreateAssetMenu(fileName = "ActorName", menuName = "Actors/Create Actor")]
public class ActorSO : ScriptableObject
{
	[SerializeField] private ActorID _actorID = default;
	[SerializeField] private LocalizedString _actorName = default;

	public ActorID ActorID => _actorID; 
	public LocalizedString ActorName => _actorName;
}
