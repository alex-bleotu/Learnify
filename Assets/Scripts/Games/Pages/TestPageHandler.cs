using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TestPageHandler : MonoBehaviour
{
    public GameObject testPage;
    public GameObject winPage;
    public GameObject infoPage;

    public GameObject nextButton;
    public GameObject previuosButton;

    public GameObject gemsLabel;

    public TMP_Text questionText;
    public GameObject[] answerButtons;

    public TMP_Text correctAnswersText;
    public TMP_Text wrongAnswersText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text titleText;
    public TMP_Text gemsText;

    public GameObject[] crowns;

    private int[] choosenAnswers;

    private int correctAnswers;

    private int questionIndex;
    private int gameIndex;

    private float time;
    private bool stopTimer;
    private bool startAnimation = false;

    private int addedCrowns;
    private int addedGems;

    private int score;

    Color32 green = new Color32(75, 180, 75, 255);
    Color32 red = new Color32(180, 55, 75, 255);

    private void UpdateTest() {
        if (TemporaryData.gameList[gameIndex].GetQuestionsCount() == 0)
            return;

        questionText.text = (questionIndex + 1) + ". " + TemporaryData.gameList[gameIndex].GetQuestions()[questionIndex].question;

        for (int i = 0; i < 4; i++)
            answerButtons[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = TemporaryData.gameList[gameIndex].GetQuestions()[questionIndex].answers[i];
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
        // correctAnswers.text = GameList.gameList[gameIndex]
        correctAnswersText.text = correctAnswers.ToString();
        wrongAnswersText.text = (TemporaryData.gameList[gameIndex].GetQuestionsCount() - correctAnswers).ToString();

         score = correctAnswers  * 100 / TemporaryData.gameList[gameIndex].GetQuestionsCount();

        if (score < 50)
            titleText.text = "Test picat";
        else
            titleText.text = "Test trecut";

        addedCrowns = 0;
        addedGems = 0;
        crowns[0].SetActive(false);
        crowns[1].SetActive(false);
        crowns[2].SetActive(false);
        if (score >= 50) {
            crowns[0].SetActive(true);
            addedCrowns++;
            addedGems += 5;

            if (score >= 75) {
                crowns[1].SetActive(true);
                addedGems += 5;

                if (score >= 90) {
                    crowns[2].SetActive(true);
                    addedGems += 5;
                    addedCrowns++;
                }
            }
        }

        if (addedGems == 0)
            gemsLabel.SetActive(false);
        else
            gemsLabel.SetActive(true);

        gemsText.text = "+" + addedGems;

        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        if (seconds >= 10)
            timeText.text = minutes + ":" + seconds;
        else
            timeText.text = minutes + ":0" + seconds;

        TemporaryData.gameList[TemporaryData.currentGameIndex].SetHighestScore(score);

        testPage.SetActive(false);
        winPage.SetActive(true);

        time = 0;
        stopTimer = false;
        startAnimation = true;
    }

    public void CloseWinInterface() {
        winPage.SetActive(false);

        TemporaryData.user.AddCrowns(addedCrowns);
        TemporaryData.user.AddExperience(TemporaryData.gameList[TemporaryData.currentGameIndex].GetExperience());
        TemporaryData.user.AddGems(addedGems);


        SceneManager.LoadScene("MainPage");

        // do the magic
    }

    public void OnNextClick() {
        questionIndex++;

        if (choosenAnswers[questionIndex] == -1) {
            nextButton.GetComponent<Button>().interactable = false;
            previuosButton.SetActive(true);

            if (questionIndex < TemporaryData.gameList[gameIndex].GetQuestionsCount()) {
                ResetTest();
            }
            else OpenWinInterface();
        } else
            UpdateSavedAsnwers();
    }

    private void UpdateSavedAsnwers() {
        ResetTest();

        for (int i = 0; i < 4; i++)
            answerButtons[i].GetComponent<Button>().interactable = false;

        if (choosenAnswers[questionIndex] == TemporaryData.gameList[gameIndex].GetQuestions()[questionIndex].correct) {
            answerButtons[choosenAnswers[questionIndex]].GetComponent<Image>().color = green;
            answerButtons[choosenAnswers[questionIndex]].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
        else {
            answerButtons[choosenAnswers[questionIndex]].GetComponent<Image>().color = red;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions()[questionIndex].correct].GetComponent<Image>().color = green;
            answerButtons[choosenAnswers[questionIndex]].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions()[questionIndex].correct].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
    }

    public void OnPreviousClick() {
        questionIndex = questionIndex == 0 ? 0 : questionIndex - 1;

        nextButton.GetComponent<Button>().interactable = true;

        if (questionIndex == 0)
            previuosButton.SetActive(false);

        if (questionIndex < TemporaryData.gameList[gameIndex].GetQuestionsCount())
            UpdateSavedAsnwers();
    }

    public void OnAnswerClick(GameObject thisGameObject) {
        if (questionIndex == TemporaryData.gameList[gameIndex].GetQuestionsCount() - 1)
            stopTimer = true;

        if (TemporaryData.gameList[gameIndex].GetQuestionsCount() == 0)
            return;

        int buttonIndex = Game.GetIndex(thisGameObject.name);
        if (buttonIndex == TemporaryData.gameList[gameIndex].GetQuestions()[questionIndex].correct) {
            answerButtons[buttonIndex].GetComponent<Image>().color = green;
            answerButtons[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;

            correctAnswers++;
        }
        else {
            answerButtons[buttonIndex].GetComponent<Image>().color = red;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions()[questionIndex].correct].GetComponent<Image>().color = green;
            answerButtons[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions()[questionIndex].correct].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }

        choosenAnswers[questionIndex] = buttonIndex;

        for (int i = 0; i < 4; i++)
            answerButtons[i].GetComponent<Button>().interactable = false;
            
        nextButton.GetComponent<Button>().interactable = true;
    }

    public void CloseInterface() {
        SceneManager.LoadScene("MainPage");
    }

    private void Start() {
        correctAnswers = 0;
        questionIndex = 0;

        time = 0f;
        stopTimer = false;

        choosenAnswers = new int[TemporaryData.gameList[gameIndex].GetQuestionsCount() + 1];

        for (int i = 0; i <= TemporaryData.gameList[gameIndex].GetQuestionsCount(); i++)
            choosenAnswers[i] = -1;

        gameIndex = TemporaryData.currentGameIndex;

        ResetTest();
        UpdateTest();

        previuosButton.SetActive(false);
        nextButton.GetComponent<Button>().interactable = false;

        testPage.SetActive(true);
    }

    private void Update() {
        if (!stopTimer) {
            time += Time.deltaTime;

            if (startAnimation)
                scoreText.text = (int)(score / (4 - time)) + "%";

            if (time > 4) {
                startAnimation = false;
                scoreText.text = score + "%";
            }
        }
    }
}
