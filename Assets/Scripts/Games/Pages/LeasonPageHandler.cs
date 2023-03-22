using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeasonPageHandler : MonoBehaviour
{
    public TMP_Text leasonText;
    public TMP_Text leasonTitleText;

    private int gameIndex;

    private void Start() {
        gameIndex = TemporaryData.currentGameIndex;

        leasonTitleText.text = TemporaryData.gameList[gameIndex].GetTitle();
        leasonText.text = TemporaryData.gameList[gameIndex].GetLeason();
    }

    public void CloseInterface() {
        SceneManager.LoadScene("MainPage");
    }
}
