using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    private GameObject mainPage;
    private GameObject gamePage;

    private GameObject leasonButton;

    private TMP_Text titleText;
    private TMP_Text descriptionText;
    private TMP_Text subjectText;

    private Image icon;
    private Image banner;

    private TestPageHandler testPage;
    private LeasonPageHandler leasonPage;

    int index;

    private string setSubjectText(int index) {
        Game.Subject subject = GameList.gameList[index].GetSubject();
        if (subject == Game.Subject.math)
            return "Matematică";
        else if (subject == Game.Subject.romanian)
            return "Română";
        else if (subject == Game.Subject.science)
            return "Științe";
        else if (subject == Game.Subject.english)
            return "Engleză";
        return "";
    }

    private void SetDifficultyImages(int index) {
        Image image1 = GameObject.Find("Image1").GetComponent<Image>();
        Image image2 = GameObject.Find("Image2").GetComponent<Image>();
        Image image3 = GameObject.Find("Image3").GetComponent<Image>();

        Game.Difficulty difficulty = GameList.gameList[index].GetDifficulty();

        Color32 green = new Color32(50, 205, 50, 255);
        Color32 orange = new Color32(255, 140, 0, 255);
        Color32 red = new Color32(211, 26, 56, 255);
        Color32 purple = new Color32(138, 43, 226, 255);
        Color32 grey = new Color32(242, 242, 242, 255);

        if (difficulty == Game.Difficulty.easy) {
            image1.color = green;
            image2.color = grey;
            image3.color = grey;
        } else if (difficulty == Game.Difficulty.medium) {
            image1.color = orange;
            image2.color = orange;
            image3.color = grey;
        } else if (difficulty == Game.Difficulty.hard) {
            image1.color = red;
            image2.color = red;
            image3.color = red;
        } else if (difficulty == Game.Difficulty.veryHard) {
            image1.color = purple;
            image2.color = purple;
            image3.color = purple;
        }
    }

    public void OpenGameInterface(GameObject thisGameObject) {
        mainPage.SetActive(false);
        gamePage.SetActive(true);

        int index = GameList.GetIndex(thisGameObject.name);

        gamePage.name = "GamePage" + index;

        titleText.text = GameList.gameList[index].GetTitle();
        descriptionText.text = GameList.gameList[index].GetDescription();

        subjectText.text = setSubjectText(index);

        SetDifficultyImages(index);

        leasonButton.SetActive(GameList.gameList[index].GetLeasonState());

        icon.sprite = GameList.gameList[index].GetIcon();
        banner.sprite = GameList.gameList[index].GetBanner();
    }

    public void CloseGameInterface() {
        gamePage.name = "GamePage";

        mainPage.SetActive(true);
        gamePage.SetActive(false);
    }

    public void OpenLeasonInterface() {
        leasonPage.OpenInterface(index);
    }

    public void OpenTestInterface() {
        testPage.OpenInterface(index);
    }

    public void OpenPlayInterface(GameObject thisGameObject) {
        index = GameList.GetIndex(thisGameObject.name);
        
        gamePage = GameObject.Find(thisGameObject.name);

        if (!GameList.gameList[index].GetLeasonState()) {
            GameList.gameList[index].SetLeasonState(true);

            OpenLeasonInterface();
        }
        else
            OpenTestInterface();

        gamePage.SetActive(false);
    }

    private void Start() {
        leasonPage = GameObject.Find("ScriptsComponent").GetComponent<LeasonPageHandler>();
        testPage = GameObject.Find("ScriptsComponent").GetComponent<TestPageHandler>();

        titleText = GameObject.Find("GameTitleText (TMP)").GetComponent<TMP_Text>();
        descriptionText = GameObject.Find("GameDescriptionText (TMP)").GetComponent<TMP_Text>();
        subjectText = GameObject.Find("SubjectText (TMP)").GetComponent<TMP_Text>();

        leasonButton = GameObject.Find("LeasonButton");

        icon = GameObject.Find("IconImage").GetComponent<Image>();
        banner = GameObject.Find("BannerImage").GetComponent<Image>();

        mainPage = GameObject.Find("MainPage");
        gamePage = GameObject.Find("GamePage");

        gamePage.SetActive(false);
    }
}
