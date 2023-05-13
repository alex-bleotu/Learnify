using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class TestPageHandler : MonoBehaviour
{
    public PowerUpsHandler powerUpsHandler;
    public FriendsPageHandler friendsPageHandler;
    public TTSHandler ttsHandler;

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

    public GameObject ttsButton;

    public GameObject timeBar;

    public AudioSource wrongAnswer;
    public AudioSource correctAnswer;
    public AudioSource testFinished;
    public AudioSource countUp;

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

    private void UpdateTest()
    {
        if (TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) == 0)
            return;

        questionText.text = (questionIndex + 1) + ". " + TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].question;

        for (int i = 0; i < 4; i++)
            answerButtons[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[i];
    }

    private void ResetTest()
    {
        for (int i = 0; i < 4; i++)
        {
            answerButtons[i].GetComponent<Button>().interactable = true;
            answerButtons[i].GetComponent<Image>().color = Color.white;
            answerButtons[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = new Color32(42, 47, 104, 255);
        }

        UpdateTest();
    }

    private void OpenWinInterface(bool time)
    {
        // correctAnswers.text = GameList.gameList[gameIndex]
        correctAnswersText.text = correctAnswers.ToString();
        wrongAnswersText.text = (TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) - correctAnswers).ToString();

        score = correctAnswers * 100 / TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel);

        if (time)
            titleText.text = "Timpul a expirat";
        else if (score < 70)
            titleText.text = "Test picat";
        else if (score >= 70)
        {
            testFinished.Play();

            TemporaryData.user.SetStreak(1);
            titleText.text = "Test trecut";
            TemporaryData.rewardedExperience = TemporaryData.gameList[TemporaryData.currentGameIndex].GetXP(currentLevel);
        }

        if (!time)
        {
            rewardedCrowns = 0;
            rewardedGems = TemporaryData.gameList[gameIndex].GetGemReward(currentLevel, score);
            if (score >= 70)
            {
                rewardedCrowns++;
                rewardedGems += 5;

                if (score >= 80)
                {
                    rewardedCrowns++;
                    rewardedGems += 5;

                    if (score >= 90)
                    {
                        rewardedGems += 5;
                        rewardedCrowns++;
                    }
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

        var timerTime = TimerSystem.GetTime();
        timerTime += TimeSpan.FromSeconds(TemporaryData.user.GetTimePotionEffect() * powerUpsHandler.timeOuts);
        timeText.text = timerTime.ToString(@"m\:ss");

        if (score == 0)
            scoreText.text = "0%";

        testPage.SetActive(false);
        winPage.SetActive(true);
        friendsPage.SetActive(true);

        TemporaryData.rewardedCrowns = rewardedCrowns;
        TemporaryData.rewardedGems = rewardedGems;

        if (!time)
            TimerSystem.TimerStart(500, () =>
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => { try { animator.SetInteger("Score", score); } catch { } });
                UnityMainThreadDispatcher.Instance().Destroy();
            });

        if (score >= 70)
            TemporaryData.gameList[TemporaryData.currentGameIndex].SetCurrentLevel(currentLevel + 1);

        if (score != 0)
            TimerSystem.TimerStart(250, () => { TimerSystem.CountUpText(0, score, 1000, scoreText, "{0}%", countUp); });

        // scoreText.text = score + "%";

        SaveSystem.SaveData();
    }

    public void CloseWinInterface()
    {
        winPage.SetActive(false);
        friendsPage.SetActive(false);

        SceneManager.LoadScene("MainPage");

        SaveSystem.SaveData();
    }

    public void OnNextClick()
    {
        wrongAnswer.Stop();
        correctAnswer.Stop();

        questionIndex++;

        ttsHandler.StopTTS();

        previuosButton.SetActive(true);

        if (choosenAnswers[questionIndex] == -1)
        {
            hintPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetHintToken() > 0;

            if (hintPowerUp.GetComponent<Button>().interactable)
                hintPowerUp.GetComponent<Image>().color = powerUpsHandler.red;

            nextButton.GetComponent<Button>().interactable = false;
            previuosButton.SetActive(true);

            if (questionIndex < TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel))
                ResetTest();
            else
            {
                OpenWinInterface(false);
                TimerSystem.TimerStop();
            }
        }
        else
            UpdateSavedAsnwers();
    }

    private void UpdateSavedAsnwers()
    {
        ResetTest();

        for (int i = 0; i < 4; i++)
            answerButtons[i].GetComponent<Button>().interactable = false;

        if (choosenAnswers[questionIndex] == TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct)
        {
            answerButtons[choosenAnswers[questionIndex]].GetComponent<Image>().color = green;
            answerButtons[choosenAnswers[questionIndex]].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
        else
        {
            answerButtons[choosenAnswers[questionIndex]].GetComponent<Image>().color = red;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct].GetComponent<Image>().color = green;
            answerButtons[choosenAnswers[questionIndex]].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
            answerButtons[TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
    }

    public void OnPreviousClick()
    {
        questionIndex = questionIndex == 0 ? 0 : questionIndex - 1;

        nextButton.GetComponent<Button>().interactable = true;

        powerUpsHandler.hintPowerUp.GetComponent<Button>().interactable = false;

        if (questionIndex == 0)
            previuosButton.SetActive(false);

        if (questionIndex < TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel))
            UpdateSavedAsnwers();
    }

    public void OnTTSButton()
    {
        try
        {
            // string text = "Întrebarea numărul " + NumberToWord.Convert(questionIndex + 1) + ", " +
            //     NumberToWord.ReplaceNumbers(TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].question) +
            //     ". Răspunsul A, " + NumberToWord.Convert(Int32.Parse(TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[0])) +
            //     ". Răspunsul B, " + NumberToWord.Convert(Int32.Parse(TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[1])) +
            //     ". Răspunsul C, " + NumberToWord.Convert(Int32.Parse(TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[2])) +
            //     ". Răspunsul D, " + NumberToWord.Convert(Int32.Parse(TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[3]));

            string text = "Întrebarea numărul " + (questionIndex + 1) + ". " +
                TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].question +
                ". Răspunsul A, " + TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[0] +
                ". Răspunsul B, " + TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[1] +
                ". Răspunsul C, " + TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[2] +
                ". Răspunsul D, " + TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].answers[3];

            ttsHandler.PlayTTS(text);
        }
        catch
        {
            ttsHandler.PlayTTS("Nu am putut citi întrebarea");
        }
    }

    public void OnAnswerClick(GameObject thisGameObject)
    {
        hintPowerUp.GetComponent<Button>().interactable = false;

        hintPowerUp.GetComponent<Image>().color = powerUpsHandler.grey;

        if (questionIndex == TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) - 1)
            TimerSystem.StopStopWatch();

        if (TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) == 0)
            return;

        int buttonIndex = Game.GetIndex(thisGameObject.name);
        if (buttonIndex == TemporaryData.gameList[gameIndex].GetQuestions(currentLevel)[questionIndex].correct)
        {
            answerButtons[buttonIndex].GetComponent<Image>().color = green;
            answerButtons[buttonIndex].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;

            correctAnswers++;

            correctAnswer.Play();

            if (powerUpsHandler.gemRush)
                rewardedGems += TemporaryData.user.GetGemRushReward();
        }
        else
        {
            wrongAnswer.Play();

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

    public void CloseInterface()
    {
        SceneManager.LoadScene("MainPage");
    }

    private void TimerBar()
    {
        TimerSystem.StartStopWatch();
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            try
            {
                timeBar.SetActive(true);
                timeBar.transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
                TimeSpan time = TimeSpan.FromSeconds(TemporaryData.gameList[gameIndex].GetTimer(currentLevel));
                timeBar.transform.GetChild(1).GetComponent<TMP_Text>().text = time.ToString(@"m\:ss");
            }
            catch { }
        });

        bool action = true;

        while (TimerSystem.GetStopWatchTime() < TemporaryData.gameList[gameIndex].GetTimer(currentLevel) * 1000)
            if (TimerSystem.GetStopWatchTime() % 1000 == 0)
            {
                try
                {
                    if (powerUpsHandler.timeOut && action)
                    {
                        TimerSystem.StopStopWatch();
                        action = false;
                    }
                    else if (!powerUpsHandler.timeOut && !action)
                    {
                        TimerSystem.ResumeStopWatch();
                        action = true;
                    }
                }
                catch { }
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    try
                    {
                        timeBar.transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().fillAmount = 1f - 1f *
                            (((float)TimerSystem.GetStopWatchTime() / 1000) / TemporaryData.gameList[gameIndex].GetTimer(currentLevel));
                        TimeSpan time = TimeSpan.FromSeconds(TemporaryData.gameList[gameIndex].GetTimer(currentLevel) - TimerSystem.GetStopWatchTime() / 1000);
                        timeBar.transform.GetChild(1).GetComponent<TMP_Text>().text = time.ToString(@"m\:ss");
                    }
                    catch { }
                });
            }

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            try
            {
                timeBar.transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
                timeBar.transform.GetChild(1).GetComponent<TMP_Text>().text = "0:00";
                OpenWinInterface(true);
            }
            catch { }
        });

        TimerSystem.StopStopWatch();

        UnityMainThreadDispatcher.Instance().Destroy();
    }

    private void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            ttsButton.GetComponent<Button>().interactable = false;

        timeBar = testPage.transform.GetChild(6).gameObject;

        correctAnswer.volume = TemporaryData.user.GetVolume();
        wrongAnswer.volume = TemporaryData.user.GetVolume();
        testFinished.volume = TemporaryData.user.GetVolume();
        countUp.volume = TemporaryData.user.GetVolume();

        correctAnswers = 0;
        questionIndex = 0;

        gameIndex = TemporaryData.currentGameIndex;

        currentLevel = TemporaryData.gameList[gameIndex].GetCurrentLevel();

        TimerSystem.StartStopWatch();

        choosenAnswers = new int[TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel) + 1];

        for (int i = 0; i <= TemporaryData.gameList[gameIndex].GetQuestionsCount(currentLevel); i++)
            choosenAnswers[i] = -1;

        // timer = TemporaryData.gameList[gameIndex].GetTimer();

        ResetTest();
        UpdateTest();

        previuosButton.SetActive(false);
        nextButton.GetComponent<Button>().interactable = false;

        if (TemporaryData.gameList[gameIndex].GetTimer(currentLevel) != 0)
            TimerSystem.TimerStartPublic(50, TimerBar);

        testPage.SetActive(true);
    }
}
