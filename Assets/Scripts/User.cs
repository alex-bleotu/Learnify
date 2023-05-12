using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User {
    private string username;
    private int age;
    private int level = 1;
    private float experience = 0f;
    
    private bool sound = true;

    public User(string username, int age) {
        this.username = username;
        this.age = age;
    } 

    public string GetUsername() { return username; }

    public int GetAge() { return age; }

    public int GetLevel() { return level; }

    public float GetExperience() { return experience; }

    public void SetLevel(int level) { this.level = level; }

    public void SetExperience(float experience) { this.experience = experience; }

    public bool GetSound() { return sound; }

    public void SetSound(bool sound) { this.sound = sound;}
}
