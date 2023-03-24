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

    public bool timeOut;
    private int timeOutLength = 15;
    private float time;

    public bool gemRush;

    Color32 yellow = new Color32(245, 189, 31, 255);

    public void OnHintPowerUp() {
        testPageHandler.answerButtons[TemporaryData.gameList[testPageHandler.gameIndex]
            .GetQuestions(testPageHandler.currentLevel)[testPageHandler.questionIndex].correct].GetComponent<Image>().color = yellow;
        hintPowerUp.GetComponent<Button>().interactable = false;

        TemporaryData.user.SetHintToken(TemporaryData.user.GetHintToken() - 1);

        hintText.text = TemporaryData.user.GetHintToken().ToString();
    }

    public void OnTimePowerUp() {
        timeOut = true;
        time = 0;

        timePowerUp.GetComponent<Button>().interactable = false;

        TemporaryData.user.SetTimeToken(TemporaryData.user.GetTimeToken() - 1);

        timeText.text = TemporaryData.user.GetTimeToken().ToString();
    }

    public void OnGemPowerUp() {
        gemRush = true;

        gemPowerUp.GetComponent<Button>().interactable = false;

        TemporaryData.user.SetGemToken(TemporaryData.user.GetGemToken() - 1);

        gemText.text = TemporaryData.user.GetGemToken().ToString();
    }


    private void Start() {
        hintPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetHintToken() > 0;
        timePowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetTimeToken() > 0;
        gemPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetGemToken() > 0;

        hintText.text = TemporaryData.user.GetHintToken().ToString();
        timeText.text = TemporaryData.user.GetTimeToken().ToString();
        gemText.text = TemporaryData.user.GetGemToken().ToString();
        
        gemRush = false;
    }

    private void Update() {
        if (timeOut) {
            time += Time.deltaTime;

            if (time > timeOutLength) {
                timeOut = false;
                timePowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetTimeToken() > 0;
            }
        }
    }
}
