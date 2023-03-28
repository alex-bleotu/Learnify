using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPageHandler : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text levelText;

    public Image expBar;

    public AudioListener audioListener;

    public MuteButtonHandler muteButtonHandler;
    public DailyGameHandler dailyGameHandler;

    public GameObject streakLabel;

    public Animator animator;

    public TMP_Text gemsText;
    public TMP_Text crownsText;

    public Image avatar;

    private void Start()
    {
        usernameText.text = TemporaryData.user.GetUsername();
        levelText.text = "Nivel " + TemporaryData.user.GetLevel();

        avatar.sprite = TemporaryData.avatar;

        dailyGameHandler.StartClock();

        muteButtonHandler.UpdateButtonIcon();

        if (TemporaryData.user.GetStreak() != 0)
            streakLabel.SetActive(true);

        // animator.SetInteger("Gems", TemporaryData.rewardedGems);
        // animator.SetInteger("Crowns", TemporaryData.rewardedCrowns);

        gemsText.text = TemporaryData.user.GetGems().ToString();
        crownsText.text = TemporaryData.user.GetCorwns().ToString();
        // expBar.fillAmount = (float)TemporaryData.user.GetExperience() / (TemporaryData.user.GetLevel() * 100);

        TemporaryData.user.AddCrowns(TemporaryData.rewardedCrowns);
        TemporaryData.user.AddGems(TemporaryData.rewardedGems);

        if (TemporaryData.rewardedGems != 0)
            TimerSystem.TimerStart(750, () => { TimerSystem.CountUpText(Int32.Parse(gemsText.text), TemporaryData.user.GetGems(), 1000, gemsText, "{0}"); });

        if (TemporaryData.rewardedCrowns != 0)
            TimerSystem.TimerStart(750, () => { TimerSystem.CountUpText(Int32.Parse(crownsText.text), TemporaryData.user.GetCorwns(), 1000, crownsText, "{0}"); });

        // if (TemporaryData.rewardedExperience != 0)
        //     TimerSystem.TimerStart(250, () => { TimerSystem.FillUpImage(expBar.fillAmount, (float)TemporaryData.user.GetExperience() / (TemporaryData.user.GetLevel() * 100), 1000, expBar); });

        TemporaryData.rewardedCrowns = 0;
        TemporaryData.rewardedGems = 0;
        TemporaryData.rewardedExperience = 0;
    }

    public void ToggleMuteButton()
    {
        muteButtonHandler.MuteButton();
    }

    public void OpenSettingsInterface()
    {
        SceneManager.LoadScene("SettingsPage");
    }
}
