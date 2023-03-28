using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveSystem
{
    private static readonly string FileName = "user.data";

    private List<Game.Data> gameData;

    public static void SaveData()
    {
        List<Game.Data> gameData = new List<Game.Data>();
        for (int i = 0; i < TemporaryData.gameList.Count; i++)
            gameData.Add(TemporaryData.gameList[i].GetData());

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + FileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        // Debug.Log(path);

        formatter.Serialize(stream, TemporaryData.user);
        formatter.Serialize(stream, gameData);
        stream.Close();

        Debug.Log("Saved");
    }

    public static void LoadData()
    {
        string path = Application.persistentDataPath + "/" + FileName;

        if (File.Exists(path))
        {
            // Debug.Log(path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            User user = formatter.Deserialize(stream) as User;

            List<Game.Data> gameData = formatter.Deserialize(stream) as List<Game.Data>;
            stream.Close();

            TemporaryData.user = user;

            if (gameData.Count != 0)
                for (int i = 0; i < TemporaryData.gameList.Count; i++)
                    TemporaryData.gameList[i].SetData(gameData[i]);

            TemporaryData.avatar = Resources.Load<Sprite>(TemporaryData.user.GetAvatarPath());

            Debug.Log("Loaded");
        }
        else
            SceneManager.LoadScene("CreateProfilePage");
        // Debug.LogError("Save file not found in" + path);
    }
}
