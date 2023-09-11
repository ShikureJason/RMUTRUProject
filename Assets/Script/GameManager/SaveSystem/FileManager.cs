using System;
using System.IO;
using UnityEngine;

public class FileManager 
{
    public static bool WriteFile(string fileName, string dataFile)
    {
        var fullPath = Path.Combine(Application.dataPath, fileName);

        try
        {
            File.WriteAllText(fullPath, dataFile);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {ex}");
            return false;
        }
    }

    public static bool LoadFromFile(string fileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(fullPath))
        {
            File.WriteAllText(fullPath, "");
        }
        try
        {
            result = File.ReadAllText(fullPath);
            return !string.IsNullOrEmpty(result); // ตรวจสอบว่า result ไม่ใช่ null และไม่ว่างเปล่า
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }

    public static bool MoveFile(string fileName, string newFileName)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);
        var newFullPath = Path.Combine(Application.persistentDataPath, newFileName);

        try
        {
            if (File.Exists(newFullPath))
            {
                File.Delete(newFullPath);
            }

            if (!File.Exists(fullPath))
            {
                Debug.Log(fullPath);
                Debug.Log("Not Found");
                return false;
            }

            File.Move(fullPath, newFullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to move file from {fullPath} to {newFullPath} with exception {e}");
            return false;
        }
    }
}
