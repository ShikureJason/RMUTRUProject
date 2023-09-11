using System;
using UnityEngine;

[Serializable]
public class Save
{
    public QuestListDataSO QuestListData;
    public SceneLoadSO CurrentScene;
    public SerializableTransform CurrentTransform;
    public Vector3 Position;
    public Quaternion Rotation;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}

[Serializable]
public class SerializableTransform
{
    public Vector3 Position;
    public Quaternion Rotation;

}
