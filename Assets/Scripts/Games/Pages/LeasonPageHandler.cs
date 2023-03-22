using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeasonPageHandler : MonoBehaviour
{
    public TMP_Text leasonText;
    public TMP_Text leasonTitleText;

    private int index = 0;

    private void Start() {
        leasonTitleText.text = GameList.gameList[index].GetTitle();
        leasonText.text = GameList.gameList[index].GetLeason();
    }

    public void CloseInterface() {
        SceneManager.LoadScene("MainPage");
    }
}
