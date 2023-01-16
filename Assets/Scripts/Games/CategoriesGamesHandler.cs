using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CategoriesGamesHandler : MonoBehaviour
{
    private GameObject categories; 

    void Start() {
        categories = GameObject.Find("Categories");

        int index = 0;
        foreach (Transform subject in categories.transform) {
            foreach (Transform game in subject.GetChild(1)) {
                game.GetChild(0).gameObject.GetComponent<TMP_Text>().text = GameList.gameList[index].GetTitle();
                index++;
            }
        }
    }
}