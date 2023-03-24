using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingPage : MonoBehaviour
{
    void Start() {
        // string path = Application.dataPath + "/Resources/Games/";
        // int directoriesCount = System.IO.Directory.GetDirectories(path).Length - 1;

        // TemporaryData.gameList = new List<Game>();
        // for (int i = 0; i < directoriesCount; i++)
        //     TemporaryData.gameList.Add(new Game(i));

        SaveSystem.LoadData();

        if (TemporaryData.user != null && TemporaryData.gameList != null) 
            SceneManager.LoadScene("MainPage");
        else
            SceneManager.LoadScene("CreateProfilePage");
    }
}
