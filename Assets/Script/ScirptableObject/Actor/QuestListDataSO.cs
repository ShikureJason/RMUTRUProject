using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestListData", menuName = "QuestList")]
public class QuestListDataSO : ScriptableObject
{
    [ScriptableObjectUUID] public string UUID;
    [SerializeField] private List<ActorQuestDataSO> _actorQuestList = default;

    public List<ActorQuestDataSO> ActorQuestList => _actorQuestList;
}
