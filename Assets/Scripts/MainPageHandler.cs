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

    private LevelSystem levelSystem;
    private AudioButtonHandler audioButtonHandler;
    private AudioListener audioListener;
    private RecomendedGamesHandler recomendedGamesHandler;

    private void Start() {
        user = SaveSystem.LoadData();
        
        usernameText = GameObject.Find("UsernameText (TMP)").GetComponent<TMP_Text>();
        usernameText.text = user.GetUsername();

        levelText = GameObject.Find("LevelText (TMP)").GetComponent<TMP_Text>();
        levelText.text = "Level " + user.GetLevel();

        levelSystem = GameObject.Find("ScriptsComponent").GetComponent<LevelSystem>();
        audioButtonHandler = GameObject.Find("ScriptsComponent").GetComponent<AudioButtonHandler>();
        audioListener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        recomendedGamesHandler = GameObject.Find("ScriptsComponent").GetComponent<RecomendedGamesHandler>();
    }

    public void AddExp() {
        int currentLevel = user.GetLevel();

        levelSystem.AddExperience(user, 50f);

        if (user.GetLevel() != currentLevel)
            levelText.text = "Level " + user.GetLevel();
    }

    public void ToggleMuteButton() {
        audioButtonHandler.ToggleMuteButton(user);
    }

    private void Update() {
        audioListener.enabled = user.GetSound();
    }
}
