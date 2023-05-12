using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonHandler : MonoBehaviour
{
    public GameObject settingsPage;
    public GameObject modalPage;
    
    public void OnPress() {
        settingsPage.SetActive(false);
        modalPage.SetActive(true);
    }

    public void ModalOnYesClick() {
        SaveSystem.DeleteData();

        modalPage.SetActive(false);
        settingsPage.SetActive(true);

        Application.Quit();
    }

    public void ModalOnNoClick() {
        modalPage.SetActive(false);
        settingsPage.SetActive(true);
    }
}
