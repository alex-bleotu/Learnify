using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;
using TMPro;
using UnityEngine.UI;

public class TestPageHandler : MonoBehaviour
{
    public PowerUpsHandler powerUpsHandler;

    public GameObject testPage;
    public GameObject winPage;

    public GameObject nextButton;
    public GameObject previuosButton;

    public GameObject gemsLabel;

    public TMP_Text questionText;
    public GameObject[] answerButtons;

    public GameObject hintPowerUp;
    public GameObject timePowerUp;
    public GameObject gemPowerUp;

    public TMP_Text correctAnswersText;
    public TMP_Text wrongAnswersText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text titleText;
    public TMP_Text gemsText;

    public Animator animator;

    private int[] choosenAnswers;

    private int correctAnswers;

    public int questionIndex;
    public int gameIndex;

    private float time;
    private bool stopTimer;
    private bool startTimer;

    private int rewardedCrowns;
    private int rewardedGems;

    private int score;
    private float auxScore;

    private bool gameOver;

    public float timer;

    public int currentLevel;

    Color32 green = new Color32(75, 180, 75, 255);
    Color32 red = new Color32(180, 55, 75, 255);

    private void UpdateTest() {
        if (TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) == 0)
            return;

        questionText.text = (questionIndex + 1) + ". " + TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].question;

        for (int i = 0; i < 4; i++)
            answerButtons[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[i];
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
        wrongAnswersText.text = (TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) - correctAnswers).ToString();

        score = correctAnswers  * 100 / TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel);

        if (score < 70)
            titleText.text = "Test picat";
        else
            titleText.text = "Test trecut";

        rewardedCrowns = 0;
        rewardedGems = (score >= 70) ? TemporaryData.gameList[gameIndex].GetGemReward(currentLevel) : 0;
        if (score >= 70) {
            rewardedCrowns++;
            rewardedGems += 5;

            if (score >= 80) {
                rewardedCrowns++;
                rewardedGems += 5;

                if (score >= 90) {
                    rewardedGems += 5;
                    rewardedCrowns++;
                }
            }
        }

        // if (addedGems == 0)
        //     gemsLabel.SetActive(false);
        // else
        //     gemsLabel.SetActive(true);

        gemsText.text = "+" + rewardedGems;

        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        if (seconds >= 10)
            timeText.text = minutes + ":" + seconds;
        else
            timeText.text = minutes + ":0" + seconds;

        TemporaryData.gameList[TemporaryData.currentGameIndex].SetHighestScore(score);

        if (score == 0)
            scoreText.text = "0%";

        testPage.SetActive(false);
        winPage.SetActive(true);

        TemporaryData.rewardedCrowns = rewardedCrowns;
        TemporaryData.rewardedGems = rewardedGems;

        animator.SetInteger("Score", score);

        if (score >= 70) {
            TemporaryData.gameList[TemporaryData.currentGameIndex].SetCurrentLevel(currentLevel + 1);
            TemporaryData.user.AddExperience(TemporaryData.gameList[TemporaryData.currentGameIndex].GetExperience(currentLevel));
        }

        if (score != 0) {
            time = 0;
            startTimer = true;
        }
    }

    public void CloseWinInterface() {
        winPage.SetActive(false);

        TemporaryData.user.AddCrowns(rewardedCrowns);
        TemporaryData.user.AddGems(rewardedGems);


        SceneManager.LoadScene("MainPage");
    }

    public void OnNextClick() {
        questionIndex++;

        previuosButton.SetActive(true);

        if (choosenAnswers[questionIndex] == -1) {
            hintPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetHintToken() > 0;

            nextButton.GetComponent<Button>().interactable = false;
            previuosButton.SetActive(true);

            if (questionIndex < TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel)) {
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

        if (choosenAnswers[questionIndex] == TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct) {
            answerButtons[choosenAnswers[questionIndex]].GetComponent<Image>().color = green;
            answerButtons[choosenAnswers[questionIndex]].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
        else {
            answerButtons[choosenAnswers[questionIndex]].GetComponent<Image>().color = red;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct].GetComponent<Image>().color = green;
            answerButtons[choosenAnswers[questionIndex]].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
    }

    public void OnPreviousClick() {
        questionIndex = questionIndex == 0 ? 0 : questionIndex - 1;

        nextButton.GetComponent<Button>().interactable = true;

        powerUpsHandler.hintPowerUp.GetComponent<Button>().interactable = false;

        if (questionIndex == 0)
            previuosButton.SetActive(false);

        if (questionIndex < TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel))
            UpdateSavedAsnwers();
    }

    public void OnAnswerClick(GameObject thisGameObject) {
        hintPowerUp.GetComponent<Button>().interactable = false;

        if (questionIndex == TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) - 1)
            stopTimer = true;

        if (TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) == 0)
            return;

        int buttonIndex = Game.GetIndex(thisGameObject.name);
        if (buttonIndex == TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct) {
            answerButtons[buttonIndex].GetComponent<Image>().color = green;
            answerButtons[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;

            correctAnswers++;

            if (powerUpsHandler.gemRush)
                rewardedGems += TemporaryData.user.GetGemRushReward();
        }
        else {
            answerButtons[buttonIndex].GetComponent<Image>().color = red;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct].GetComponent<Image>().color = green;
            answerButtons[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
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

        currentLevel = TemporaryData.gameList[gameIndex].GetCurrentLevel();

        time = 0f;
        stopTimer = false;

        choosenAnswers = new int[TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) + 1];

        for (int i = 0; i <= TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel); i++)
            choosenAnswers[i] = -1;

        gameIndex = TemporaryData.currentGameIndex;

        // timer = TemporaryData.gameList[gameIndex].GetTimer();

        ResetTest();
        UpdateTest();

        previuosButton.SetActive(false);
        nextButton.GetComponent<Button>().interactable = false;

        testPage.SetActive(true);
    }

    private void Update() {
        if (!stopTimer)
            time += Time.deltaTime;
        if (startTimer) {
            time += Time.deltaTime;
            if (time > 0.5) {
                startTimer = false;

                auxScore = 0;
                gameOver = true;
            }
        }

        if (gameOver && auxScore <= score) {
            auxScore += (1f * score / (0.5f * (1f / Time.deltaTime)));

            if (auxScore > score)
                auxScore = score;

            scoreText.text = (int)auxScore + "%";
        }
    }
}
