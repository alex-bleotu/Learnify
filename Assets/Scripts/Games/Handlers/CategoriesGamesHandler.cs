using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoriesGamesHandler : MonoBehaviour
{
    public GameObject categories;

    public GameObject mathCategory;
    public GameObject scienceCategory;
    public GameObject romanianCategory;
    public GameObject englishCategory;

    public GameObject gameTemplate;

    private int coordX = -475;
    private int coordY = 0;
    private int spacingCoord = 175;

    private void fillGames(GameObject category, Game.Subject subject) {
        int index = 0;
        for (int i = 0; i < TemporaryData.gameList.Count; i++)
            if (TemporaryData.gameList[i].GetSubject() == subject) {
                GameObject copyGame = Instantiate(gameTemplate, new Vector3(0, 0, 0), Quaternion.identity);

                copyGame.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = TemporaryData.gameList[i].GetTitle();
                copyGame.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = TemporaryData.gameList[i].GetIcon();
                copyGame.transform.parent = category.transform;
                copyGame.transform.position = new Vector3(copyGame.transform.parent.position.x + coordX + spacingCoord * index, copyGame.transform.parent.position.y + coordY, 0);
                copyGame.name = "Game" + i;
                copyGame.SetActive(true);

                index++;
            }

        if (index <= 6)
            category.transform.parent.GetComponent<ScrollRect>().enabled = false;
    }

    void Start() {
        // int index = 0;
        // foreach (Transform subject in categories.transform) {
        //     foreach (Transform game in subject.GetChild(1)) {
        //         game.GetChild(1).gameObject.GetComponent<TMP_Text>().text = TemporaryData.gameList[index].GetTitle();
        //         game.GetChild(0).gameObject.GetComponent<Image>().sprite = TemporaryData.gameList[index].GetIcon();
        //         index++;
        //     }
        // }
        
        fillGames(mathCategory, Game.Subject.math);
        fillGames(scienceCategory, Game.Subject.science);
        fillGames(romanianCategory, Game.Subject.romanian);
        fillGames(englishCategory, Game.Subject.english);
    }
}