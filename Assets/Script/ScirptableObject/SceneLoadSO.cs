using UnityEngine;
using UnityEngine.AddressableAssets;

public enum SceneType
{
    Menu,
    ChangeScene,
    Manage,
}
[CreateAssetMenu(fileName = "SceneName", menuName = "Scenes/LoadScene")]
public class SceneLoadSO : ScriptableObject
{
    [TextArea] public string description;
    [SerializeField] private AssetReference _sceneLoad = default;
    [SerializeField] private SceneType _sceneType = default;

    public AssetReference Scene => _sceneLoad;
    public SceneType SceneType => _sceneType;
}
