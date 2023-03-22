using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsPageHandler : MonoBehaviour
{
    public Slider audioSlider;

    public void Start() {
        if (TemporaryData.user.GetMute())
            audioSlider.value = 0f;
        else
            audioSlider.value = TemporaryData.user.GetSound();
    }

    public void CloseInterface() {
        SceneManager.LoadScene("MainPage");
    }

    public void UpdateSlider() {
        if (audioSlider.value != 0f) {
            TemporaryData.user.SetSound(audioSlider.value);

            if (audioSlider.value == 0f)
                TemporaryData.user.SetMute(true);
            else
                TemporaryData.user.SetMute(false);
        }
    }
}
