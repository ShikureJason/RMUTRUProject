using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;


public class UIManager : MonoBehaviour
{

    [Header("Setting")]
    [SerializeField] private ActorSO _mainPlayerName = default;
    [SerializeField] private InputReaderSO _inputReader = default;
    [SerializeField] private SaveSystemSO _saveSystem = default;

    [Header("UI")]
    [SerializeField] private GameObject _uiDialogue = default;
    [SerializeField] private GameObject _uiPlayerTalk = default;
    [SerializeField] private GameObject _uiChoice = default;
    [SerializeField] private GameObject _uiMenu = default;
    [SerializeField] private GameObject _uiExitconfirm = default;

    [Header("Event Emitter")]
    [SerializeField] private VoidEventSO _startNextSceneMenu = default;

    [Header("Event Lintener")]
    [SerializeField] private StartDialogueEvent _startDialogueEventListener = default;
    [SerializeField] private VoidEventSO _endDialogueEventListener = default;
    [SerializeField] private InteractEventSO _interactEventListener = default;
    [SerializeField] private ChioceSendEventSO _chioceEventListener = default;


    private void OnEnable()
    {
        _startDialogueEventListener.OnEventRaised += StartUIDialogue;
        _endDialogueEventListener.OnEventRaised += StopDialogue;
        _interactEventListener.OnEventRaised += OnInteract;
        _chioceEventListener.OnEventRaised += OnChioce;
        _inputReader.EscapeGameEvent += OnMenu;
        _inputReader.EscapeMenuEvent += OnEscapeMenu;
    }

    private void OnDisable()
    {
        _startDialogueEventListener.OnEventRaised -= StartUIDialogue;
        _endDialogueEventListener.OnEventRaised -= StopDialogue;
        _interactEventListener.OnEventRaised -= OnInteract;
        _chioceEventListener.OnEventRaised -= OnChioce;
        _inputReader.EscapeGameEvent -= OnMenu;
        _inputReader.EscapeMenuEvent -= OnEscapeMenu;
    }

    private void StartUIDialogue(LocalizedString dialogue, ActorSO actor)
    {
        bool isActor = (actor == _mainPlayerName);
        _uiDialogue.GetComponent<UIDialogue>().SetDialogue(dialogue, actor, isActor);
        _uiDialogue.SetActive(true);
    }

    private void StopDialogue()
    {
        _uiDialogue.SetActive(false);
    }

    private void OnInteract(InteractType type, bool isInteract)
    {
        switch (type)
        {
            case InteractType.Talk:
                _uiPlayerTalk.SetActive(isInteract);
                break;
        }
        
    }
    private void OnChioce(List<Chioce> chioceData)
    {
        _uiChoice.SetActive(true);
        _uiChoice.GetComponent<UIChioce>().SetChioce(chioceData);
    }

    private void OnMenu()
    {
        _inputReader.EnableMenu();
        _uiMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void OnEscapeMenu()
    {
        _inputReader.DisableMenu();
        _uiMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExit()
    {
        _uiMenu.SetActive(false);
        _uiExitconfirm.SetActive(true);
    }

    public void OnExitConfirm() 
    {
        Debug.Log("Save....");
        if(_saveSystem.SaveData())
        {
            Debug.Log("Sucess");
            _startNextSceneMenu.RaiseEvent();
        }
        else 
        {
            Debug.Log("Fail");
        }
        
    }

    public void OnExitCancle()
    {
        _uiExitconfirm.SetActive(false);
        _uiMenu.SetActive(true);
    }
}
