using UnityEngine;

[CreateAssetMenu(fileName = "SaveSystem", menuName = "GameManager/SaveSystem")]
public class SaveSystemSO : ScriptableObject
{
    [SerializeField] private string _saveFileName = default;
    [SerializeField] private QuestListDataSO _questListData = default;

    [Header("Event Emitter")]
    [SerializeField] private VoidEventSO _resetAllDataEmitter = default;

    [Header("Evnet Listener")]
    [SerializeField] private VoidEventSO _startNewGameDataListener = default;

    private Save _saveData = new Save();

    private void OnEnable()
    {
        _startNewGameDataListener.OnEventRaised += SetNewData;
    }

    private void OnDisable()
    {
        _startNewGameDataListener.OnEventRaised -= SetNewData;
    }
    private void WriteEmtyFile()
    {
        FileManager.WriteFile(_saveFileName + ".jt", "");
    }

    public bool SaveData()
    {
        _saveData.QuestListData = _questListData;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Debug.Log(playerObject.transform.position);
            // Access the character's position and rotation
            _saveData.Rotation = playerObject.transform.rotation;
            _saveData.Position = playerObject.transform.position;
            
        }

        
        if (FileManager.MoveFile(_saveFileName + ".jt", _saveFileName + ".jt.bak"))
        {
            Debug.Log("Data Move");
            if (FileManager.WriteFile(_saveFileName + ".jt", _saveData.ToJson()))
            {
                Debug.Log("Save");
                return true;
            }
            return false;
        }
        return false;
    }

    public bool LoadData()
    {
        if (FileManager.LoadFromFile(_saveFileName + ".jt", out var json))
        {
            _saveData.LoadFromJson(json);
            Debug.Log("true");
            return true;
        }
        Debug.Log("false");
        return false;
    }

    public void SetNewData()
    {
        WriteEmtyFile();
        _resetAllDataEmitter.RaiseEvent();
        SaveData();
    }

}
