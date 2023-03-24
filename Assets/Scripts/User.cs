using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User {
    private string username;
    private int age;
    private int level = 1;
    private const int maxLevel = 100;
    private float experience = 0f;
    private float experienceMultiplier = 1f;

    private int gems = 0;
    private const int maxGems = 100000;
    private int crowns = 0;
    private const int maxCrowns = 100000;

    private int dailyStreak = 1;
    private float sound = 10f;
    private bool mute = false;

    private int hintToken;
    private int timeToken;
    private int gemToken;
    private const int maxTokens = 100;

    private int gemRushReward;

    public User(string username, int age) {
        this.username = username;
        this.age = age;

        gems = 100;
        crowns = 0;

        hintToken = 5;
        timeToken = 2;
        gemToken = 2;

        gemRushReward = 2;
    } 

    public string GetUsername() { return username; }
    public int GetAge() { return age; }
    public int GetLevel() { return level; }
    public float GetExperience() { return experience; }
    public float GetSound() { return sound; }
    public bool GetMute() { return mute; }
    public int GetDailyStreak() { return dailyStreak; }
    public float GetExperienceMultiplier() { return experienceMultiplier; }
    public int GetGems() { return gems; }
    public int GetCorwns() { return crowns; }
    public void SetLevel(int level) { this.level = (level < maxLevel) ? level : maxLevel; }
    public void SetExperience(float experience) { this.experience = experience; }
    public void SetSound(float sound) { this.sound = sound; }
    public void SetMute(bool mute) { this.mute = mute; }
    public void SetExperienceMultiplier(float experienceMultiplier) { this.experienceMultiplier = experienceMultiplier; }
    public void SetDailyStreak(int dailyStreak) { this.dailyStreak = dailyStreak; }
    public void SetGems(int gems) { this.gems = (gems < maxGems) ? gems : maxGems; }
    public void SetCrowns(int crowns) { this.crowns = (crowns < maxCrowns) ? crowns : maxCrowns; }
    public int GetHintToken() { return hintToken; }
    public int GetTimeToken() { return timeToken; }
    public int GetGemToken() { return gemToken; }
    public void SetHintToken(int hintToken) { this.hintToken = (hintToken < maxTokens) ? hintToken : maxTokens; }
    public void SetTimeToken(int timeToken) { this.timeToken = (timeToken < maxTokens) ? timeToken : maxTokens; }
    public void SetGemToken(int gemToken) { this.gemToken = (gemToken < maxTokens) ? gemToken : maxTokens; }
    public int GetGemRushReward() { return gemRushReward; }
    public void SetGemRushReward(int gemRushReward) { this.gemRushReward = gemRushReward; }

    public void AddGems(int gems) { 
        this.gems += gems;
        if (this.gems > maxGems)
            this.gems = maxGems;
    }
    public void SubstractGems(int gems) { 
        this.gems -= gems;
        if (this.gems < 0)
            this.gems = 0;
    }

    public void AddCrowns(int crowns) { 
        this.crowns += crowns;
        if (this.crowns > maxCrowns)
            this.crowns = maxCrowns; 
    }
    public void SubstractCrowns(int crowns) { 
        this.crowns -= crowns; 
        if (this.crowns < 0)
            this.crowns = 0;
    }
    
    public void AddExperience(float exp) {
        experience += exp;

        while (experience >= level * experienceMultiplier) {
            Debug.Log("Level Up");
            experience -= level * experienceMultiplier;
            level++;
        }
    }
}
