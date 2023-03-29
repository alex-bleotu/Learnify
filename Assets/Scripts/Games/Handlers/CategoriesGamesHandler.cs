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

    private float coordX;
    private float coordY;
    private float spacingCoord;
    private float spacing = 20;

    private void fillGames(GameObject category, Game.Subject subject)
    {
        coordX = gameTemplate.transform.position.x + 25;
        coordY = gameTemplate.transform.position.y;
        spacingCoord = spacing + gameTemplate.GetComponent<RectTransform>().sizeDelta.x;

        int index = 0;
        for (int i = 0; i < TemporaryData.gameList.Count; i++)
            if (TemporaryData.gameList[i].GetSubject() == subject)
            {
                GameObject copyGame = Instantiate(gameTemplate, new Vector3(0, 0, 0), Quaternion.identity);

                copyGame.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = TemporaryData.gameList[i].GetTitle();
                copyGame.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = TemporaryData.gameList[i].GetIcon();
                copyGame.transform.SetParent(category.transform);

                copyGame.transform.position = new Vector3(coordX + spacingCoord * index, category.transform.parent.position.y, 0);
                copyGame.name = "Game" + i;
                copyGame.SetActive(true);

                index++;
            }

        if (index < 6)
            category.transform.parent.GetComponent<ScrollRect>().enabled = false;

        if (index == 1)
            index++;

        category.GetComponent<RectTransform>().sizeDelta = new Vector2((index * spacingCoord - spacing) * 1.42f,
            category.GetComponent<RectTransform>().sizeDelta.y);
        category.transform.position = new Vector3(category.GetComponent<RectTransform>().sizeDelta.x / 2 - spacing / 2,
            category.transform.position.y, 0);
    }

    void Start()
    {
        // fillGames(mathCategory, Game.Subject.math);
        // fillGames(scienceCategory, Game.Subject.science);
        // fillGames(romanianCategory, Game.Subject.romanian);
        // fillGames(englishCategory, Game.Subject.english);
    }
}