using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPageHandler : MonoBehaviour
{
    public GameObject infoPage;
    public TMP_Text infoTitleText;
    public TMP_Text infoDescriptionText;

    public void OpenInterface(GameObject thisGameObject) {
        infoPage.SetActive(true);

        int index = GameList.GetIndex(thisGameObject.name);

        infoTitleText.text = GameList.gameList[index].GetTitle();
    }

    public void CloseInterface() {
        infoPage.SetActive(false);
    }
}
