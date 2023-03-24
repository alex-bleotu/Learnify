using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsHandler : MonoBehaviour
{
    public TestPageHandler testPageHandler;

    public GameObject hintPowerUp;
    public GameObject timePowerUp;
    public GameObject gemPowerUp;

    public bool timeOut;
    private int timeOutLength = 15;
    private float time;

    public bool gemRush;
    public int gemRushReward = 2;

    Color32 yellow = new Color32(245, 189, 31, 255);

    public void OnHintPowerUp() {
        testPageHandler.answerButtons[TemporaryData.gameList[testPageHandler.gameIndex]
            .GetQuestions(testPageHandler.currentLevel)[testPageHandler.questionIndex].correct].GetComponent<Image>().color = yellow;
        hintPowerUp.GetComponent<Button>().interactable = false;
    }

    public void OnTimePowerUp() {
        timeOut = true;
        time = 0;

        timePowerUp.GetComponent<Button>().interactable = false;
    }

    public void OnGemPowerUp() {
        gemRush = true;

        gemPowerUp.GetComponent<Button>().interactable = false;
    }


    private void Start() {
        hintPowerUp.GetComponent<Button>().interactable = true;
        timePowerUp.GetComponent<Button>().interactable = true;
        gemPowerUp.GetComponent<Button>().interactable = true;

        gemRush = false;
    }

    private void Update() {
        if (timeOut) {
            time += Time.deltaTime;

            if (time > timeOutLength) {
                timeOut = false;
                timePowerUp.GetComponent<Button>().interactable = true;
            }
        }
    }
}
