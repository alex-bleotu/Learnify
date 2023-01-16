using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    private GameObject mainPage;
    private GameObject gamePage;

    private TMP_Text titleText;
    private TMP_Text descriptionText;
    private TMP_Text recommendedLevelText;

    public void OpenGameInterface(GameObject thisGameObject) {
        mainPage.SetActive(false);
        gamePage.SetActive(true); 

        int index = GameList.GetIndex(thisGameObject.name);

        titleText.text = GameList.gameList[index].GetTitle();
        descriptionText.text = GameList.gameList[index].GetDescription();           
        recommendedLevelText.text = "Recommended Level: " + GameList.gameList[index].GetRecommendedLevel();
    }

    public void CloseGameInterface() {  
        mainPage.SetActive(true);
        gamePage.SetActive(false); 
    }

    private void Start() {
        titleText = GameObject.Find("GameTitleText (TMP)").GetComponent<TMP_Text>();
        descriptionText = GameObject.Find("GameDescriptionText (TMP)").GetComponent<TMP_Text>();
        recommendedLevelText = GameObject.Find("GameRecommendedText (TMP)").GetComponent<TMP_Text>();

        mainPage = GameObject.Find("MainPage");
        gamePage = GameObject.Find("GamePage");

        gamePage.SetActive(false);
    }
}
