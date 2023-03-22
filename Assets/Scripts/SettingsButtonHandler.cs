using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonHandler : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject settingsPage;

    public void OpenInterface() {
        mainPage.SetActive(false);
        settingsPage.SetActive(true);
    }

    public void CloseInterface() {
        mainPage.SetActive(true);
        settingsPage.SetActive(false);
    }
}
