using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfilePageHandler : MonoBehaviour {
    private User user;
    private string username;
    private int age;
    
    public void ReadUsernameInput(string username) { this.username = username; }

    public void ReadAgeInput(string age) { this.age = Int32.Parse(age); }

    public void OnSubmit() {
        Debug.Log(username);
        Debug.Log(age);
        if (username != null && age != 0) {
            User user = new User(username, age);
            SaveSystem.SaveData(user);
            SceneManager.LoadScene("MainPage");
        }
    }
}
