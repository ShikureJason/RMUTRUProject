using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] private LocalizeStringEvent _lineText = default;
    [SerializeField] private LocalizeStringEvent _actorNameText = default;
    [SerializeField] private LocalizeStringEvent _playerNameText = default;
    [SerializeField] private GameObject _actorNamePanel = default;
    [SerializeField] private GameObject _playerNamePanel = default;

    public void SetDialogue(LocalizedString dialogue, ActorSO actor, bool isActorLine)
    {
        _lineText.StringReference = dialogue;
        _actorNamePanel.SetActive(!isActorLine);
        _playerNamePanel.SetActive(isActorLine);

        if (isActorLine)
            _playerNameText.StringReference = actor.ActorName;
        else
            _actorNameText.StringReference = actor.ActorName;
           
    }

}
