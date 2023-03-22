using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeasonPageHandler : MonoBehaviour
{
    private TMP_Text leasonText;
    private TMP_Text leasonTitleText;

    private int index = 0;

    private void UpdateText(int index) {
        if (SceneManager.GetActiveScene().name == "LeasonPage") {
            leasonText = GameObject.Find("LeasonText (TMP)").GetComponent<TMP_Text>();
            leasonTitleText = GameObject.Find("LeasonTitleText (TMP)").GetComponent<TMP_Text>();

            leasonTitleText.text = GameList.gameList[index].GetTitle();
            leasonText.text = GameList.gameList[index].GetLeason();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded :)");
        UpdateText(index);
    }

    public void OpenInterface(int index) {
        this.index = index;

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("LeasonPage");
    }

    public void CloseInterface() {
        SceneManager.LoadScene("MainPage");
    }
}
