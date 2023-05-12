using UnityEngine;
using System;
using TMPro;

public class TimeHandler : MonoBehaviour
{
    private TMP_Text timeText;

    private string Time()
    {
        DateTime localTime = DateTime.Now;

        return (23 - localTime.Hour < 10 ? "0" : "") +
            (23 - localTime.Hour) + ":" +
            (59 - localTime.Minute + (localTime.Minute == 0 ? 0 : 1) < 10 ? "0" : "") +
            (59 - localTime.Minute + (localTime.Minute == 0 ? 0 : 1));
    }

    private void Start()
    {
        timeText = GameObject.Find("TimeText (TMP)").GetComponent<TMP_Text>();

        timeText.text = Time();
    }

    private void Update()
    {
        timeText.text = Time();
    }
}
