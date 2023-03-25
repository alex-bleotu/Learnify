using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingPage : MonoBehaviour
{
    void Start() {
        string path = Application.dataPath + "/Resources/Games/";

        string[] directories = Directory.GetDirectories(path);
        foreach (string directory in directories) {
            if (!directory.EndsWith("Default")) {
                string filePath = directory + "/data.json";

                if (File.Exists(filePath))
                    TemporaryData.gameList.Add(new Game(filePath, directory.Split('/').Last()));
            }
        }

        TemporaryData.gameList = TemporaryData.gameList.OrderBy(x => x.GetId()).ToList();
            
        SaveSystem.LoadData();

        if (TemporaryData.user != null && TemporaryData.gameList != null) 
            SceneManager.LoadScene("MainPage");
        else
            SceneManager.LoadScene("CreateProfilePage");
    }
}
