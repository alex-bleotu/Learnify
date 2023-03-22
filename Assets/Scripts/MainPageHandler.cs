using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainPageHandler : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text levelText;

    private User user;

    public AudioListener audioListener;
    public Slider audioSlider;

    public RecomendedGamesHandler recomendedGamesHandler;
    public AudioButtonHandler audioButtonHandler;
    public DailyGameHandler dailyGameHandler;

    private void Start() {
        user = SaveSystem.LoadData();

        usernameText.text = user.GetUsername();
        levelText.text = "Nivel " + user.GetLevel();

        audioSlider.value = user.GetSound();

        dailyGameHandler.StartClock(user);
    }

    public void ToggleMuteButton() {
        audioButtonHandler.MuteButton(user, audioSlider);
    }

    public void UpdateSound() {
        if (GameObject.Find("SettingsPage") != null)
        user.SetSound(audioSlider.value);
    }

    private void Update() {
        audioListener.enabled = user.GetMute();
    }
}
