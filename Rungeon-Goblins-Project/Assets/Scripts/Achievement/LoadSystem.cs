using System.IO;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    public PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);

            return null;
        }
    }
}