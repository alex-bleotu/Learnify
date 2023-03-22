using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPageHandler : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text levelText;

    public AudioListener audioListener;

    public MuteButtonHandler muteButtonHandler;
    public DailyGameHandler dailyGameHandler;

    private void Start() {
        usernameText.text = TemporaryData.user.GetUsername();
        levelText.text = "Nivel " + TemporaryData.user.GetLevel();

        dailyGameHandler.StartClock();

        muteButtonHandler.UpdateButtonIcon();
    }

    public void ToggleMuteButton() {
        muteButtonHandler.MuteButton();
    }

    public void OpenSettingsInterface() {
        SceneManager.LoadScene("SettingsPage");
    }

    private void Update() {
        audioListener.enabled = TemporaryData.user.GetMute();
    }
}
