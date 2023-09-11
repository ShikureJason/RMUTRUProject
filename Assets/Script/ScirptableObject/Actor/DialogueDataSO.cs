using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public enum ChioceState
{
    Cancle,
    NextDialogue,
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Actors/Dialogue/Dialogue Data")]
public class DialogueDataSO : ScriptableObject
{   
    [SerializeField] private List<Line> _lines = default;
    [SerializeField] private List<Chioce> _chioce = default;
    [SerializeField] private bool _isDone = false;
    public List<Line> Lines => _lines;
    public List<Chioce> Chioces => _chioce;
    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }

}
[Serializable]
public class Line
{
    [SerializeField] private ActorID _actorID = default;
	[SerializeField] private List<LocalizedString> _textList = default;
	
	public ActorID ActorID => _actorID;
	public List<LocalizedString> TextList => _textList;
}

[Serializable]
public class Chioce
{
    [SerializeField] private LocalizedString _chioceName = default;
    [SerializeField] private DialogueStepSO _nextDialogue = default;
    [SerializeField] private ChioceState _choiceState = default;

    public LocalizedString ChioceName => _chioceName;
    public DialogueStepSO NextDialogue => _nextDialogue;
    public ChioceState ChoiceState => _choiceState;
}