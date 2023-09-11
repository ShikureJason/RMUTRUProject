using UnityEngine;
using UnityEngine.UI;

public class ConfirmNewGame : MonoBehaviour
{
    [Header("Event Emitter")]
    [SerializeField] private VoidEventSO _startNextSceneEmitter = default;
    public void CancelButton()
    {
        GameObject[] distractButton = GameObject.FindGameObjectsWithTag("MainMenuButton");
        foreach (GameObject obj in distractButton)
        {
            Button button = obj.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = true;
            }
        }
        gameObject.SetActive(false);
    }

    public void ConfirmButton()
    {
        gameObject.SetActive(false);
        _startNextSceneEmitter.RaiseEvent();
    }
}
