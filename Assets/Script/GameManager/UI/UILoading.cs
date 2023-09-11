using UnityEngine;

public class UILoading : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen = default;
    [Header("Event Listener")]
    [SerializeField] private BoolEventSO _loadingScreenEventListener = default;

    private void Start()
    {
        _loadingScreenEventListener.OnEventRaised += OnLoadScreen;
    }

    private void OnLoadScreen(bool loadingScreen)
    {
        _loadingScreen.SetActive(loadingScreen);
    }
}
