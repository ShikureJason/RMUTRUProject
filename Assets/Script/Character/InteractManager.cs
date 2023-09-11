using UnityEngine;
using System.Collections.Generic;

public enum InteractType
{
    None,
    Talk,
}

public class Interact
{
	public InteractType type;
	public GameObject interactableObject;

	public Interact(InteractType t, GameObject obj)
	{
		type = t;
		interactableObject = obj;
	}
}

public class InteractManager : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader = default;
    [Header("Event Emitter")]
    [SerializeField] private InteractEventSO _interactEventEmitter = default;
    [Header("Event Listener")]
    [SerializeField] private TriggerDetectEvent _triggerDetectEventListener = default;

    private List<Interact> _interact = new List<Interact>();
    
    private void OnEnable()
    {
        _triggerDetectEventListener.OnEventRaised += OnDetectChange;
        _inputReader.InteractEvent += OnInteractButtonPress;
    }

    private void OnDisable()
    {
        _triggerDetectEventListener.OnEventRaised -= OnDetectChange;
        _inputReader.InteractEvent -= OnInteractButtonPress;
    }

    private void OnInteractButtonPress()
    {
        if (_interact.Count == 0)
        {
            return;
        }

        if (_interact[0].type == InteractType.Talk)
        {
            _interact[0].interactableObject.GetComponent<NonCharacter>().Interact();
        }
    }

    private void OnDetectChange(bool isDetect, GameObject obj)
    {
        if (isDetect)
        {
            AddInteraction(obj);
            Debug.Log("obj = " + obj);
        }else
        {
            RemoveInteraction(obj);
        }
    }

    private void AddInteraction(GameObject obj)
    {
        Interact newInteract = new Interact(InteractType.None, obj);

        switch (obj.tag)
        {
            case "NPC":
                newInteract.type = InteractType.Talk;
                break;
            default:
                newInteract.type = InteractType.None;
                break;
        }   

        if (newInteract.type != InteractType.None)
        {
            _interact.Add(newInteract);
            _interactEventEmitter.OnEventRaised(_interact[0].type, true);
        }
    }

    private void RemoveInteraction(GameObject obj)
    {
        if (_interact.Count == 0)
            return;
        _interactEventEmitter.OnEventRaised(_interact[0].type, false);
        _interact.RemoveAll(interact => interact.interactableObject == obj);
    }
}
