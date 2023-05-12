using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpsHandler : MonoBehaviour
{
    public TestPageHandler testPageHandler;

    public GameObject hintPowerUp;
    public GameObject timePowerUp;
    public GameObject gemPowerUp;

    public TMP_Text hintText;
    public TMP_Text timeText;
    public TMP_Text gemText;

    public bool timeOut = false;

    public int timeOuts = 0;

    public bool gemRush;

    public Color32 yellow = new Color32(245, 189, 31, 255);
    public Color32 red = new Color32(196, 69, 95, 255);
    public Color32 orange = new Color32(239, 169, 74, 255);
    public Color32 green = new Color32(76, 145, 65, 255);
    public Color32 grey = new Color32(128, 128, 128, 255);

    public void OnHintPowerUp()
    {
        testPageHandler.answerButtons[TemporaryData.gameList[testPageHandler.gameIndex]
            .GetQuestions(testPageHandler.currentLevel)[testPageHandler.questionIndex].correct].GetComponent<Image>().color = yellow;
        hintPowerUp.GetComponent<Button>().interactable = false;

        TemporaryData.user.SetHintToken(TemporaryData.user.GetHintToken() - 1);

        hintPowerUp.GetComponent<Image>().color = grey;

        hintText.text = TemporaryData.user.GetHintToken().ToString();
    }

    public void OnTimePowerUp()
    {
        timeOut = true;

        timeOuts++;

        timePowerUp.GetComponent<Button>().interactable = false;

        timePowerUp.GetComponent<Image>().color = grey;

        TemporaryData.user.SetTimeToken(TemporaryData.user.GetTimeToken() - 1);

        timeText.text = TemporaryData.user.GetTimeToken().ToString();

        timePowerUp.transform.GetChild(1).GetComponent<Image>().color = orange;

        TimerSystem.TimerStart(TemporaryData.user.GetTimePotionEffect() * 1000, () =>
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                timePowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetTimeToken() > 0;

                timePowerUp.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);

                if (timePowerUp.GetComponent<Button>().interactable == true)
                    timePowerUp.GetComponent<Image>().color = green;

                timeOut = false;
            });
        });
    }

    public void OnGemPowerUp()
    {
        gemRush = true;

        gemPowerUp.GetComponent<Button>().interactable = false;

        gemPowerUp.GetComponent<Image>().color = grey;

        TemporaryData.user.SetGemToken(TemporaryData.user.GetGemToken() - 1);

        gemText.text = TemporaryData.user.GetGemToken().ToString();

        gemPowerUp.transform.GetChild(1).GetComponent<Image>().color = orange;
    }


    private void Start()
    {
        hintPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetHintToken() > 0;
        timePowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetTimeToken() > 0 &&
            TemporaryData.gameList[TemporaryData.currentGameIndex].GetTimer(TemporaryData.gameList[TemporaryData.currentGameIndex].GetCurrentLevel()) > 0;
        gemPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetGemToken() > 0;
        gemPowerUp.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        if (hintPowerUp.GetComponent<Button>().interactable)
            hintPowerUp.GetComponent<Image>().color = red;
        else
            hintPowerUp.GetComponent<Image>().color = grey;

        if (timePowerUp.GetComponent<Button>().interactable)
            timePowerUp.GetComponent<Image>().color = green;
        else
            timePowerUp.GetComponent<Image>().color = grey;

        if (gemPowerUp.GetComponent<Button>().interactable)
            gemPowerUp.GetComponent<Image>().color = orange;
        else
            gemPowerUp.GetComponent<Image>().color = grey;

        hintText.text = TemporaryData.user.GetHintToken().ToString();
        timeText.text = TemporaryData.user.GetTimeToken().ToString();
        gemText.text = TemporaryData.user.GetGemToken().ToString();

        gemRush = false;
    }
}
