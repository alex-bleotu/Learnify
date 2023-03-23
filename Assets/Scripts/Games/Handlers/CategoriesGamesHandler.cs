using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoriesGamesHandler : MonoBehaviour
{
    public GameObject categories; 

    void Start() {
        int index = 0;
        foreach (Transform subject in categories.transform) {
            foreach (Transform game in subject.GetChild(1)) {
                game.GetChild(1).gameObject.GetComponent<TMP_Text>().text = TemporaryData.gameList[index].GetTitle();
                game.GetChild(0).gameObject.GetComponent<Image>().sprite = TemporaryData.gameList[index].GetIcon();
                index++;
            }
        }
    }
}