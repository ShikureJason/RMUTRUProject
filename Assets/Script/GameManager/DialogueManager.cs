using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader = default;
    [SerializeField] private QuestListDataSO _questList = default;
    [SerializeField] private List<ActorSO> _actorList = default;

    [Header("Event Emitter")]
    [SerializeField] private StartDialogueEvent _startDialogueEventEmitter = default;
    [SerializeField] private VoidEventSO _endDialogueEventEmitter = default;
    [SerializeField] private ChioceSendEventSO _chioceEventEmitter = default;
    
    [Header("Event Listener")]
    [SerializeField] private InteractDialogueEvent _interactDialogueEventListener = default;
    [SerializeField] private ChioceReceiveEventSO _choiceEventListener = default;
    [SerializeField] private VoidEventSO _resetAllDataListener = default;

    private DialogueDataSO currentDialogue;
    private DialogueStepSO currentStep;

    private int _countDialogue;
    private int _countLine;
    private int _currentQuestDataIndex;
    private int _currentDialogueIndex;
    private bool chioceState = false;

    private void OnEnable()
    {
        _inputReader.NextDialogueEvent += OnNextDialogue;
        _interactDialogueEventListener.OnEventRaised += InteractDialogue;
        _choiceEventListener.OnEventRiased += OnChioceSelect;
        _resetAllDataListener.OnEventRaised += ResetDataQuestList;
        
    }

    private void OnDisable()
    {
        _interactDialogueEventListener.OnEventRaised -= InteractDialogue;
        _inputReader.NextDialogueEvent -= OnNextDialogue;
        _choiceEventListener.OnEventRiased -= OnChioceSelect;
        _resetAllDataListener.OnEventRaised -= ResetDataQuestList;
    }

    private void InteractDialogue(ActorSO actor)
    {
        _inputReader.EnableDialogueInput();
        _currentQuestDataIndex = _questList.ActorQuestList.FindIndex(findactor => findactor.Actor == actor);
        _currentDialogueIndex = _questList.ActorQuestList[_currentQuestDataIndex].DialogueStep.FindIndex(dialogue => dialogue.IsDone == false);
        if (_currentDialogueIndex == -1)
        {
            _inputReader.DisableDialogueInput();
            return;
        }    
            
        switch (_questList.ActorQuestList[_currentQuestDataIndex].DialogueStep[_currentDialogueIndex].State)
        {
            case QuestState.StartQuest:
                DisplayDialogueData(_questList.ActorQuestList[_currentQuestDataIndex].DialogueStep[_currentDialogueIndex].StartDialogue);
                break;
            case QuestState.RunningQuest:
                DisplayDialogueData(_questList.ActorQuestList[_currentQuestDataIndex].DialogueStep[_currentDialogueIndex].CompleteDialogue);
                break;
            case QuestState.EndQuest:
                DisplayDialogueData(_questList.ActorQuestList[_currentQuestDataIndex].DialogueStep[_currentDialogueIndex].IncompleteDialogue);
                break;
        }
        currentStep = _questList.ActorQuestList[_currentQuestDataIndex].DialogueStep[_currentDialogueIndex];
    }

    private void DisplayDialogueData(DialogueDataSO dialogue)
    {
        _countDialogue = 0;
        _countLine = 0;
        currentDialogue = dialogue;
        _startDialogueEventEmitter.RaiseEvent(currentDialogue.Lines[_countDialogue].TextList[_countLine], _actorList.Find(actor => actor.ActorID == currentDialogue.Lines[_countDialogue].ActorID));
    }

    private void OnNextDialogue()
    {
        if (chioceState)
            return; 

        _countLine++;
        if (_countLine < currentDialogue.Lines[_countDialogue].TextList.Count)
        {
            _startDialogueEventEmitter.RaiseEvent(currentDialogue.Lines[_countDialogue].TextList[_countLine], _actorList.Find(actor => actor.ActorID == currentDialogue.Lines[_countDialogue].ActorID));
        }
        else if ((_countDialogue < currentDialogue.Lines.Count-1) && (currentDialogue.Lines.Count != 1))
        {
            _countDialogue++;
            _countLine = 0;
            _startDialogueEventEmitter.RaiseEvent(currentDialogue.Lines[_countDialogue].TextList[_countLine], _actorList.Find(actor => actor.ActorID == currentDialogue.Lines[_countDialogue].ActorID));
        }else
        {
            if (currentDialogue.Chioces.Count != 0)
            {
                chioceState = true;
                currentDialogue.IsDone = true;
                _chioceEventEmitter.RaiseEvent(currentDialogue.Chioces);
                _endDialogueEventEmitter.RaiseEvent();
            }
            else
            {
                currentStep.IsDone = true;
                _inputReader.DisableDialogueInput();
                _endDialogueEventEmitter.RaiseEvent();
            }
            
        }
    }

    private void OnChioceSelect(DialogueStepSO dialogueStep, ChioceState state)
    {
        chioceState = false;
        switch (state)
        {
            case ChioceState.NextDialogue:
                currentStep.IsDone = true;
                DisplayDialogueData(dialogueStep.StartDialogue);
                break;
            case ChioceState.Cancle:
                currentStep.IsDone = false;
                DisplayDialogueData(dialogueStep.StartDialogue);
                break;
        }
    }

    private void ResetDataQuestList()
    {
        foreach (ActorQuestDataSO questListData in _questList.ActorQuestList)
        {
            foreach (DialogueStepSO dialogueList in questListData.DialogueStep)
            {    
                dialogueList.IsDone = false;
                dialogueList.State = QuestState.StartQuest;
            }
            questListData.IsDone = false;
        }
    }

}
