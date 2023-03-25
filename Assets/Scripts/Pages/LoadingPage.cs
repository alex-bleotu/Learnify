using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingPage : MonoBehaviour
{
    void Start() {
        string path = Application.dataPath + "/Resources/Games/";
        
        int gameCount = 0;

        string[] directories = Directory.GetDirectories(path);
        foreach (string directory in directories) {
            if (!directory.EndsWith("Default")) {
                string filePath = directory + "/data.json";

                if (File.Exists(filePath))
                    gameCount++;
            }
        }

        for (int i = 0; i < gameCount; i++)
            TemporaryData.gameList.Add(new Game(i));
            
        SaveSystem.LoadData();

        if (TemporaryData.user != null && TemporaryData.gameList != null) 
            SceneManager.LoadScene("MainPage");
        else
            SceneManager.LoadScene("CreateProfilePage");
    }
}
