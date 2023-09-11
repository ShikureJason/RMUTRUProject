using UnityEngine;

public enum QuestState
{
    StartQuest,
    RunningQuest,
    EndQuest,
}

[CreateAssetMenu(fileName = "DialogueStep", menuName = "Actors/Dialogue/DialogueStep")]
public class DialogueStepSO : ScriptableObject
{
    [SerializeField] private QuestState _state = default;
    [SerializeField] private DialogueDataSO _startDialogue = default;
    [SerializeField] private DialogueDataSO _completeDialogue = default;
    [SerializeField] private DialogueDataSO _incompleteDialogue = default;
    [SerializeField] private bool _isDone = false;


    public QuestState State
    {
        get => _state;
        set => _state = value;
    }
    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }

    public DialogueDataSO StartDialogue => _startDialogue;
    public DialogueDataSO CompleteDialogue => _completeDialogue;
    public DialogueDataSO IncompleteDialogue => _incompleteDialogue;

}

