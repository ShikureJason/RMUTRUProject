using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIChioce : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab = default;
    [SerializeField] private GameObject _buttonParent = default;

    [Header("Event Emitter")]
    [SerializeField] private ChioceReceiveEventSO _receiveEventEmitter = default;


    public void SetChioce(List<Chioce> chioceData)
    {
        Debug.Log(chioceData.Count);
        foreach (Chioce chioce in chioceData)
        {
            GameObject newChoiceButton = Instantiate(_buttonPrefab, _buttonParent.transform);
            newChoiceButton.GetComponentInChildren<LocalizeStringEvent>().StringReference = chioce.ChioceName;
            newChoiceButton.GetComponent<Button>().onClick.AddListener(() => SelectChoice(chioce.NextDialogue, chioce.ChoiceState));
        }
    }

    private void SelectChoice(DialogueStepSO nextDialogue, ChioceState state)
    {
        gameObject.SetActive(false);
        _receiveEventEmitter.RaiseEvent(nextDialogue, state);

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
