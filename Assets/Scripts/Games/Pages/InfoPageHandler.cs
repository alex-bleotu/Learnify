using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPageHandler : MonoBehaviour
{
    public GameObject infoPage;
    public GameObject testPage;

    public TMP_Text infoTitleText;
    public TMP_Text infoDescriptionText;

    public void OpenInterface(GameObject thisGameObject) {
        testPage.SetActive(false);
        infoPage.SetActive(true);

        infoTitleText.text = TemporaryData.gameList[TemporaryData.currentGameIndex].GetTitle();
        infoDescriptionText.text = TemporaryData.gameList[TemporaryData.currentGameIndex].GetInfo();
    }

    public void CloseInterface() {
        infoPage.SetActive(false);
        testPage.SetActive(true);
    }
}
