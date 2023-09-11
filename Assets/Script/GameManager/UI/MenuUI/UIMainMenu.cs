using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] SaveSystemSO _saveSystem = default;
    [SerializeField] GameObject _buttonNewGame = default;
    [SerializeField] GameObject _buttonResumeGame = default;
    private bool isNewGame = false;

    private void Awake()
    {
        isNewGame = _saveSystem.LoadData();
    }

    private void Start()
    {
        _buttonResumeGame.SetActive(isNewGame);
    }
}
