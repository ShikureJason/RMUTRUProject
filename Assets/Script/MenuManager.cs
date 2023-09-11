using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private SaveSystemSO _saveSystem = default;
    [SerializeField] private GameObject _confirmNewGamePanel = default;
    [SerializeField] private GameObject _mainMenu = default;

    [Header("Event Emitter")]
    [SerializeField] private VoidEventSO _startNextSceneEmitter = default;

    private void Start()
    {
        _mainMenu.SetActive(true);
    }
    public void OnStartNewGame()
    {
        if (_saveSystem.LoadData())
        {
            GameObject[] distractButton = GameObject.FindGameObjectsWithTag("MainMenuButton");
            foreach (GameObject obj in distractButton)
            {
                Button button = obj.GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = false;
                }
            }
            _confirmNewGamePanel.SetActive(true);
        }
        else
            _startNextSceneEmitter.RaiseEvent();
    }

    public void OnContinueGame()
    {
        Debug.Log("Con");
    }

    public void OnSetting()
    {
        Debug.Log("setting");
    }

    public void OnExit()
    {
        Application.Quit();
    }

}
