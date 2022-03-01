using System;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Singleton
    public static MainManager Instance { get; private set; }

    public Color TeamColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    [Serializable]
    private class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        var saveData = new SaveData();
        saveData.TeamColor = TeamColor;

        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        var path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var saveData = JsonUtility.FromJson<SaveData>(json);

            TeamColor = saveData.TeamColor;
        }
    }
}