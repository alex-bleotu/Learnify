using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainPageHandler : MonoBehaviour
{
    private TMP_Text usernameText;
    private TMP_Text levelText;

    private User user;

    private AudioButtonHandler audioButtonHandler;
    private AudioListener audioListener;
    private RecomendedGamesHandler recomendedGamesHandler;
    private Slider audioSlider;

    private void Start() {
        user = SaveSystem.LoadData();
        
        usernameText = GameObject.Find("UsernameText (TMP)").GetComponent<TMP_Text>();
        usernameText.text = user.GetUsername();

        levelText = GameObject.Find("LevelText (TMP)").GetComponent<TMP_Text>();
        levelText.text = "Level " + user.GetLevel();

        audioButtonHandler = GameObject.Find("ScriptsComponent").GetComponent<AudioButtonHandler>();
        audioListener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        recomendedGamesHandler = GameObject.Find("ScriptsComponent").GetComponent<RecomendedGamesHandler>();
        audioSlider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    public void ToggleMuteButton() {
        audioButtonHandler.MuteButton(user, audioSlider);
    }

    private void Update() {
        audioListener.enabled = user.GetSound();
    }
}
