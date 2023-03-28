using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateProfilePageHandler : MonoBehaviour
{
    private User user;

    public GameObject usernameInput;
    public GameObject ageInput;

    private string username;
    private int age;

    private List<Game> gameList;

    public void ReadUsernameInput() { username = usernameInput.GetComponent<TMP_InputField>().text; }

    public void ReadAgeInput() { age = Int32.Parse(ageInput.GetComponent<TMP_InputField>().text); }

    public void OnSubmit()
    {
        if (username != null && age != 0)
        {
            User user = new User(username, age);

            TemporaryData.user = user;

            SaveSystem.SaveData();

            SceneManager.LoadScene("LoadingPage");
        }
    }
}
