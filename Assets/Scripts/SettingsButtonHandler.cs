using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonHandler : MonoBehaviour
{
    private bool showSettingsInterface = false;

    private GameObject menuPage;
    private GameObject settingsPage;

    private void Start() {
        menuPage = GameObject.Find("MainPage");
        settingsPage = GameObject.Find("SettingsPage");

        menuPage.SetActive(!showSettingsInterface);
        settingsPage.SetActive(showSettingsInterface);
    }

    public void SettingsButton() {
        showSettingsInterface = !showSettingsInterface;

        changeView();
    }

    private void changeView() {
        menuPage.SetActive(!showSettingsInterface);
        settingsPage.SetActive(showSettingsInterface);
    }
}
