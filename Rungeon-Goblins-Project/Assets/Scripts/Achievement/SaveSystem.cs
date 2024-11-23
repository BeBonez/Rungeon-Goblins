using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }
}