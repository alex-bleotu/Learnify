using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPageHandler : MonoBehaviour
{
    private GameObject infoPage;
    private TMP_Text infoTitleText;
    private TMP_Text infoDescriptionText;

    public void OpenInterface(GameObject thisGameObject) {
        infoPage.SetActive(true);

        int index = GameList.GetIndex(thisGameObject.name);

        infoTitleText.text = GameList.gameList[index].GetTitle();
    }

    public void CloseInterface() {
        infoPage.SetActive(false);
    }

    private void Start() {
        infoPage = GameObject.Find("InfoPage");
        // gamePage = GameObject.Find("GamePage");

        infoTitleText = GameObject.Find("InfoTitleText (TMP)").GetComponent<TMP_Text>();
        infoDescriptionText = GameObject.Find("InfoDescriptionText (TMP)").GetComponent<TMP_Text>();

        infoPage.SetActive(false);
    } 
}
