using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCharacter : MonoBehaviour
{
    [SerializeField] private ActorSO _actor = default;

    [Header("Event Emitter")]
    [SerializeField] private InteractDialogueEvent _interactDialogueEventEmitter = default;

    public void Interact()
    {
        _interactDialogueEventEmitter.RaiseEvent(_actor);
    }
}
