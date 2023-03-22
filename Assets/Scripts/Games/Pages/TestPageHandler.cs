using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TestPageHandler : MonoBehaviour
{
    private GameObject testPage;
    private GameObject winPage;
    private GameObject infoPage;

    private GameObject nextButton;
    private GameObject previuosButton;

    private int currentIndex;
    private int index;

    private TMP_Text questionText;
    private GameObject[] answerButtons;

    private TMP_Text correctAnswersText;
    private TMP_Text wrongAnswersText;
    private TMP_Text scoreText;

    private int correctAnswers;

    private void UpdateTest() {
        if (GameList.gameList[index].GetQuestionsCount() == 0)
            return;

        questionText.text = (currentIndex + 1) + ". " + GameList.gameList[index].GetQuestions()[currentIndex];

        for (int i = 0; i < 4; i++)
            answerButtons[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = GameList.gameList[index].GetAnwers()[currentIndex][i];
    }

    private void ResetTest() {
        for (int i = 0; i < 4; i++) {
            answerButtons[i].GetComponent<Button>().interactable = true;
            answerButtons[i].GetComponent<Image>().color = Color.white;
            answerButtons[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = new Color32(42, 47, 104, 255);
        }

        UpdateTest();
    }

    private void OpenWinInterface() {
        Debug.Log(correctAnswers);

        // correctAnswers.text = GameList.gameList[index]
        correctAnswersText.text = "Răspunsuri corecte: " + correctAnswers;
        wrongAnswersText.text = "Răspunsuri greşite: " + (GameList.gameList[index].GetQuestionsCount() - correctAnswers);
        scoreText.text = "Scor total: " + ((1.0 * correctAnswers / GameList.gameList[index].GetQuestionsCount()) * 100) + "%";

        winPage.SetActive(true);
        testPage.SetActive(false);
    }

    public void CloseWinInterface() {
        testPage.SetActive(true);

        SceneManager.LoadScene("MainPage");
    }

    public void OnNextClick() {
        currentIndex++;

        nextButton.GetComponent<Button>().interactable = false;
        previuosButton.SetActive(true);

        if (currentIndex < GameList.gameList[index].GetQuestionsCount())
            ResetTest();
        else OpenWinInterface();
    }

    public void OnPreviousClick() {
        currentIndex = currentIndex == 0 ? 0 : currentIndex - 1;

        if (currentIndex < GameList.gameList[index].GetQuestionsCount())
            ResetTest();
    }

    public void OnAnswerClick(GameObject thisGameObject) {
        if (GameList.gameList[index].GetQuestionsCount() == 0)
            return;

        Color32 green = new Color32(75, 180, 75, 255);
        Color32 red = new Color32(180, 55, 75, 255);

        int buttonIndex = GameList.GetIndex(thisGameObject.name);
        if (buttonIndex == GameList.gameList[index].GetCorrectAnswers()[currentIndex]) {
            answerButtons[buttonIndex].GetComponent<Image>().color = green;
            answerButtons[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;

            correctAnswers++;
        }
        else {
            answerButtons[buttonIndex].GetComponent<Image>().color = red;
            answerButtons[GameList.gameList[index].GetCorrectAnswers()[currentIndex]].GetComponent<Image>().color = green;
            answerButtons[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
            answerButtons[GameList.gameList[index].GetCorrectAnswers()[currentIndex]].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }

        for (int i = 0; i < 4; i++)
            answerButtons[i].GetComponent<Button>().interactable = false;
            
        nextButton.GetComponent<Button>().interactable = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded 2 :)");

        if (SceneManager.GetActiveScene().name == "TestPage") { 
            LoadComponents();

            ResetTest();
            UpdateTest();

            previuosButton.SetActive(false);
            nextButton.GetComponent<Button>().interactable = false;
        }
    }

    public void OpenInterface(int index) {
        correctAnswers = 0;
        currentIndex = 0;

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("TestPage");
    }

    public void CloseInterface() {
        SceneManager.LoadScene("MainPage");
    }
    
    private void LoadComponents() {
        testPage = GameObject.Find("TestPage");
        winPage = GameObject.Find("WinPage");
        infoPage = GameObject.Find("InfoPage");

        nextButton = GameObject.Find("NextButton");
        previuosButton = GameObject.Find("PreviousButton");

        correctAnswersText = GameObject.Find("CorrectText (TMP)").GetComponent<TMP_Text>();
        wrongAnswersText = GameObject.Find("WrongText (TMP)").GetComponent<TMP_Text>();
        scoreText = GameObject.Find("ScoreText (TMP)").GetComponent<TMP_Text>();

        questionText = GameObject.Find("QuestionText (TMP)").GetComponent<TMP_Text>();

        answerButtons = new GameObject[4];
        for (int i = 0; i < 4; i++)
            answerButtons[i] = GameObject.Find("Answer" + i);

        winPage.SetActive(false);
        infoPage.SetActive(false);
    }
}
