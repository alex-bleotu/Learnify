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
        timePowerUp.GetComponent<Button>().interactable = false;

        TemporaryData.user.SetTimeToken(TemporaryData.user.GetTimeToken() - 1);

        timeText.text = TemporaryData.user.GetTimeToken().ToString();

        TimerSystem.TimerStart(TemporaryData.user.GetTimePotionEffect(), () => {
            UnityMainThreadDispatcher.Instance().Enqueue(() => { timePowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetTimeToken() > 0; });
        });
    }

    public void OnGemPowerUp() {
        gemRush = true;

        gemPowerUp.GetComponent<Button>().interactable = false;

        TemporaryData.user.SetGemToken(TemporaryData.user.GetGemToken() - 1);

        gemText.text = TemporaryData.user.GetGemToken().ToString();
    }


    private void Start() {
        hintPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetHintToken() > 0;
        timePowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetTimeToken() > 0 && 
            TemporaryData.gameList[TemporaryData.currentGameIndex].GetTimer(TemporaryData.gameList[TemporaryData.currentGameIndex].GetCurrentLevel()) > 0;
        gemPowerUp.GetComponent<Button>().interactable = TemporaryData.user.GetGemToken() > 0;

        hintText.text = TemporaryData.user.GetHintToken().ToString();
        timeText.text = TemporaryData.user.GetTimeToken().ToString();
        gemText.text = TemporaryData.user.GetGemToken().ToString();
        
        gemRush = false;
    }
}
