using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecomendedGamesHandler : MonoBehaviour
{
    private int[] GetRandomValues() {
        System.Random random = new System.Random();

        int[] randomValues = new int[3];

        randomValues[0] = random.Next(0, GameList.gameList.Length);
        randomValues[1] = random.Next(0, GameList.gameList.Length);
        while (randomValues[1] == randomValues[0])
            randomValues[1] = random.Next(0, GameList.gameList.Length);
        randomValues[2] = random.Next(0, GameList.gameList.Length);
        while (randomValues[2] == randomValues[0] || randomValues[2] == randomValues[1])
            randomValues[2] = random.Next(0, GameList.gameList.Length);

        return randomValues;
    }

    private void buttonClick() {
        Debug.Log("lol");
    }

    void Start()
    {
        System.Random random = new System.Random();

        int[] randomValues = GetRandomValues();

        Debug.Log(randomValues[0] + ", " + randomValues[1] + ", " + randomValues[2]);


        Button[] games = new Button[3];
        for (int i = 0; i < 3; i++) {
            games[i] = GameObject.Find("Game" + (i + 1)).GetComponent<Button>();
            games[i].onClick.AddListener(buttonClick);

            TMP_Text buttonText = games[i].transform.GetChild(0).GetComponent<TMP_Text>();
            buttonText.text = "Game " + (randomValues[i] + 1).ToString();
        }
    }
}
