using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestPageHandler : MonoBehaviour
{
    private GameObject testPage;
    private GameObject leasonPage;
    private GameObject gamePage;
    private GameObject mainPage;

    private int currentIndex;
    private int index;

    private TMP_Text questionText;
    private GameObject[] answers;

    public void OpenTestInterface() {
        testPage.SetActive(true);
    }

    public void OpenLeasonInterface() {
        leasonPage.SetActive(true);
        gamePage.SetActive(false);
    }

    private void updateTest() {
        if (GameList.gameList[index].GetQuestions().Count == 0)
            return;

        questionText.text = (currentIndex + 1) + ". " + GameList.gameList[index].GetQuestions()[currentIndex];

        for (int i = 0; i < 4; i++)
            answers[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = GameList.gameList[index].GetAnwers()[currentIndex][i];
    }

    private void OpenTestFinishedInterface() {
        
    }

    private void resetTest() {
        for (int i = 0; i < 4; i++) {
            answers[i].GetComponent<Button>().interactable = true;
            answers[i].GetComponent<Image>().color = Color.white;
            answers[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = new Color32(42, 47, 104, 255);
        }

        updateTest();
    }

    public void OnNextClick() {
        currentIndex++;

        if (currentIndex < GameList.gameList[index].GetQuestions().Count)
            resetTest();
        else OpenTestFinishedInterface();
    }

    public void OnAnswerClick(GameObject thisGameObject) {
        if (GameList.gameList[index].GetQuestions().Count == 0)
            return;

        Color32 green = new Color32(75, 180, 75, 255);
        Color32 red = new Color32(180, 55, 75, 255);

        int buttonIndex = GameList.GetIndex(thisGameObject.name);
        if (buttonIndex == GameList.gameList[index].GetCorrectAnswers()[currentIndex]) {
            answers[buttonIndex].GetComponent<Image>().color = green;
            answers[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
        else {
            answers[buttonIndex].GetComponent<Image>().color = red;
            answers[GameList.gameList[index].GetCorrectAnswers()[currentIndex]].GetComponent<Image>().color = green;
            answers[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
            answers[GameList.gameList[index].GetCorrectAnswers()[currentIndex]].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }

        for (int i = 0; i < 4; i++)
            answers[i].GetComponent<Button>().interactable = false;
    }

    public void OpenInterface(GameObject thisGameObject) {
        currentIndex = 0;

        index = GameList.GetIndex(thisGameObject.name);
        
        gamePage = GameObject.Find(thisGameObject.name);

        if (!GameList.gameList[index].GetLeasonState()) {
            GameList.gameList[index].SetLeasonState(true);
            OpenLeasonInterface();
        }
        else
            OpenTestInterface();

        gamePage.SetActive(false);

        updateTest();
    }

    public void CloseInterface() {
        testPage.SetActive(false);
        gamePage.SetActive(false);
        leasonPage.SetActive(false);
        mainPage.SetActive(true);
    }
    
    void Start() {
        mainPage = GameObject.Find("MainPage");
        testPage = GameObject.Find("TestPage");
        leasonPage = GameObject.Find("LeasonPage");

        questionText = GameObject.Find("QuestionText (TMP)").GetComponent<TMP_Text>();

        answers = new GameObject[4];
        for (int i = 0; i < 4; i++)
            answers[i] = GameObject.Find("Answer" + i);

        testPage.SetActive(false);
        leasonPage.SetActive(false);
    }
}
