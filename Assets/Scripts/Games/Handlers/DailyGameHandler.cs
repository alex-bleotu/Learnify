using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DailyGameHandler : MonoBehaviour
{
    public GameHandler gameHandler;
    public GameObject gamePage;

    public TMP_Text timeText;
    public TMP_Text dayText;

    private string Time()
    {
        DateTime localTime = DateTime.Now;

        return (23 - localTime.Hour < 10 ? "0" : "") +
            (23 - localTime.Hour) + ":" +
            (59 - localTime.Minute + (localTime.Minute == 0 ? 0 : 1) < 10 ? "0" : "") +
            (59 - localTime.Minute + (localTime.Minute == 0 ? 0 : 1));
    }

    public void StartClock()
    {
        timeText.text = Time();

        dayText.text = TemporaryData.user.GetDailyStreak() + " zile";
    }

    private void Update()
    {
        timeText.text = Time();
    }

    public void OnButton()
    {
        gamePage.name = "GamePage" + UnityEngine.Random.Range(0, TemporaryData.gameList.Count);
        gameHandler.OpenGameInterface(gamePage);
    }
}
