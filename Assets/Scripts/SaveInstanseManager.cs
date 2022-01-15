using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveInstanseManager : MonoBehaviour
{
    public static SaveInstanseManager Instance;

    public string currentPlayerName;
    public string playerName;
    public int playerScore;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveBestScore(string name, int score)
    {
        SaveData data = new SaveData();
        data.bestPlayerName = name;
        data.bestScore = score;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.bestPlayerName;
            playerScore = data.bestScore;
        }
    }

}
