using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;
using TMPro;
using UnityEngine.UI;

public class TestPageHandler : MonoBehaviour
{
    public PowerUpsHandler powerUpsHandler;
    public FriendsPageHandler friendsPageHandler;

    public GameObject testPage;
    public GameObject winPage;
    public GameObject friendsPage;

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

    private int rewardedCrowns;
    private int rewardedGems;

    private int score;

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
        else {
            titleText.text = "Test trecut";
            TemporaryData.rewardedExperience = TemporaryData.gameList[TemporaryData.currentGameIndex].GetExperience(currentLevel);
        }

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

        TemporaryData.user.AddXP(TemporaryData.rewardedExperience);
        TemporaryData.gameList[TemporaryData.currentGameIndex].AddExperience(TemporaryData.rewardedExperience);
        
        friendsPageHandler.FriensListHandler();

        gemsText.text = "+" + rewardedGems;

        timeText.text = TimerSystem.GetTime();

        if (score == 0)
            scoreText.text = "0%";

        testPage.SetActive(false);
        winPage.SetActive(true);
        friendsPage.SetActive(true);

        TemporaryData.rewardedCrowns = rewardedCrowns;
        TemporaryData.rewardedGems = rewardedGems;

        TimerSystem.TimerStart(1000, () => { 
            UnityMainThreadDispatcher.Instance().Enqueue(() => {animator.SetInteger("Score", score); });
            UnityMainThreadDispatcher.Instance().Destroy(); 
        });

        if (score >= 70)
            TemporaryData.gameList[TemporaryData.currentGameIndex].SetCurrentLevel(currentLevel + 1);

        if (score != 0)
            TimerSystem.TimerStart(250, () => {TimerSystem.CountUpText(0, score, 1000, scoreText, "{0}%");});
    }

    public void CloseWinInterface() {
        winPage.SetActive(false);
        friendsPage.SetActive(false);

        SceneManager.LoadScene("MainPage");
    }

    public void OnNextClick() {
        questionIndex++;

        previuosButton.SetActive(true);

        if (choosenAnswers[questionIndex] == -1) {
            hintPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetHintToken() > 0;

            nextButton.GetComponent<Button>().interactable = false;
            previuosButton.SetActive(true);

            if (questionIndex < TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel))
                ResetTest();
            else
                OpenWinInterface();
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
            TimerSystem.StopStopWatch();

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

        TimerSystem.StartStopWatch();

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
}
