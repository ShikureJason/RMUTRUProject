using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "ActorQuestData", menuName = "Actors/QuestData")]
public class ActorQuestDataSO : ScriptableObject
{
    [SerializeField] private ActorSO _actor = default;
    [SerializeField] private List<DialogueStepSO> _dialogueStep = default;
    [SerializeField] private bool _isDone = false;

    public List<DialogueStepSO> DialogueStep => _dialogueStep;
    public ActorSO Actor => _actor;
    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }

}
