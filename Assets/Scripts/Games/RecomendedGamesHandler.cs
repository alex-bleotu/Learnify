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

    // private void buttonClick() {
    //     Debug.Log("lol");
    // }

    private void Start()
    {
        System.Random random = new System.Random();

        int[] randomValues = GetRandomValues();

        // Debug.Log(randomValues[0] + ", " + randomValues[1] + ", " + randomValues[2]);

        int index = 0;
        GameObject buttonParent = GameObject.Find("RecommendedGames");
        
        foreach (Transform child in buttonParent.transform) {
            if (child.name == "TitleText (TMP)")
                continue;

            GameObject game = child.gameObject;

            game.name = "RecommendedGame" + randomValues[index]; 

            // games[i].onClick.AddListener(buttonClick);

            // TMP_Text buttonText = game.transform.GetChild(1).GetComponent<TMP_Text>();
            // buttonText.text = GameList.gameList[randomValues[index]].GetTitle();

            game.transform.GetChild(0).GetComponent<Image>().sprite = GameList.gameList[randomValues[index]].GetIcon();
            
            index++;
        }
    }
}
