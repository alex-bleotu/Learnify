using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPageHandler : MonoBehaviour
{
    private GameObject infoPage;
    private GameObject gamePage;

    private TMP_Text infoTitleText;
    private TMP_Text gameTitleText;

    public void OpenInterface(GameObject thisGameObject) {
        infoPage.SetActive(true);
        gamePage.SetActive(false);

        int index = GameList.GetIndex(thisGameObject.GetComponent<TMP_Text>().text);

        infoTitleText.text = "Info - " + GameList.gameList[index - 1].GetTitle();
    }

    public void CloseInterface(GameObject thisGameObject) {
        infoPage.SetActive(false);
        gamePage.SetActive(true);

        int index = GameList.GetIndex(thisGameObject.GetComponent<TMP_Text>().text);

        gameTitleText.text = GameList.gameList[index - 1].GetTitle();
    }

    private void Start() {
        infoPage = GameObject.Find("InfoPage");
        gamePage = GameObject.Find("GamePage");

        infoTitleText = GameObject.Find("InfoTitleText (TMP)").GetComponent<TMP_Text>();
        gameTitleText = GameObject.Find("GameTitleText (TMP)").GetComponent<TMP_Text>();

        infoPage.SetActive(false);
    } 
}
