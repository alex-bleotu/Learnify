using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject gamePage;

    public GameObject playButton;

    public GameObject leasonButton;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text subjectText;

    public Image icon;
    public Image banner;

    int index;

    private string setSubjectText(int index) {
        Game.Subject subject = TemporaryData.gameList[index].GetSubject();
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

        Game.Difficulty difficulty = TemporaryData.gameList[index].GetDifficulty(TemporaryData.gameList[index].GetCurrentLevel());

        Color32 green = new Color32(50, 205, 50, 255);
        Color32 orange = new Color32(255, 140, 0, 255);
        Color32 red = new Color32(211, 26, 56, 255);
        Color32 purple = new Color32(138, 43, 226, 255);
        Color32 grey = new Color32(0, 0, 0, 30);

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

        int index = Game.GetIndex(thisGameObject.name);

        gamePage.name = "GamePage" + index;

        if (TemporaryData.gameList[index].GetTitle() != null) {
            if (TemporaryData.gameList[TemporaryData.currentGameIndex].GetLeasonState() == false)
                titleText.text = TemporaryData.gameList[index].GetTitle();
            else
                titleText.text = TemporaryData.gameList[index].GetTitle() + " (" +  
                    (TemporaryData.gameList[index].GetCurrentLevel() + 1) + "/" + TemporaryData.gameList[index].GetLevelCount() + ") ";
                    
            descriptionText.text = TemporaryData.gameList[index].GetDescription();
            
            subjectText.text = setSubjectText(index);

            SetDifficultyImages(index);

            leasonButton.SetActive(TemporaryData.gameList[index].GetLeasonState());

            icon.sprite = TemporaryData.gameList[index].GetIcon();
            banner.sprite = TemporaryData.gameList[index].GetBanner();

            TemporaryData.currentGameIndex = index;
            
            playButton.SetActive(true);
        } else {
            titleText.text = "Not Found :(";
            playButton.SetActive(false);
            descriptionText.text = "";
            subjectText.text = "";
            leasonButton.SetActive(false);

            icon.sprite = null;
            banner.sprite = null;
        }
    }

    public void CloseGameInterface() {
        gamePage.name = "GamePage";

        mainPage.SetActive(true);
        gamePage.SetActive(false);
    }

    public void OpenLeasonInterface() {
        SceneManager.LoadScene("LeasonPage");
    }

    public void OpenTestInterface() {
        SceneManager.LoadScene("TestPage");
    }

    public void OpenPlayInterface(GameObject thisGameObject) {
        index = Game.GetIndex(thisGameObject.name);
        
        gamePage = GameObject.Find(thisGameObject.name);

        if (!TemporaryData.gameList[index].GetLeasonState()) {
            TemporaryData.gameList[index].SetLeasonState(true);

            OpenLeasonInterface();
        }
        else
            OpenTestInterface();

        gamePage.SetActive(false);
    }
}
