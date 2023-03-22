using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingPage : MonoBehaviour
{
    void Start() {
        SaveSystem.LoadData();

        SceneManager.LoadScene("MainPage");
    }
}
