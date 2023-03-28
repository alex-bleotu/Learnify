using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class User {
    private string username;
    private int age;
    private string avatarPath;
    private int level = 1;
    private const int maxLevel = 100;
    private int xp = 0;
    private const int experiencePerLevel = 100;
    private int experienceMultiplier = 1;

    private int gems = 0;
    private const int maxGems = 100000;
    private int crowns = 0;
    private const int maxCrowns = 100000;

    private int dailyStreak = 1;
    private float volume = 10f;
    private bool mute = false;

    private int hintToken;
    private int timeToken;
    private int gemToken;
    private const int maxTokens = 100;

    private int gemRushReward;

    private int timePotionEffect = 15000; // milliseconds

    public User(string username, int age) {
        this.username = username;
        this.age = age;

        avatarPath = "Avatars/default";

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
    public int GetXP() { return xp; }
    public float GetVolume() { return volume; }
    public bool GetMute() { return mute; }
    public int GetDailyStreak() { return dailyStreak; }
    public float GetExperienceMultiplier() { return experienceMultiplier; }
    public int GetGems() { return gems; }
    public int GetCorwns() { return crowns; }
    public void SetLevel(int level) { this.level = (level < maxLevel) ? level : maxLevel; }
    public void SetXP(int experience) { this.xp = experience; }
    public void SetVolume(float volume) { this.volume = volume; }
    public void SetMute(bool mute) { this.mute = mute; }
    public void SetExperienceMultiplier(int experienceMultiplier) { this.experienceMultiplier = experienceMultiplier; }
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
    public int GetTimePotionEffect() { return timePotionEffect; }
    public string GetAvatarPath() { return avatarPath; }

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
    
    public void AddXP(int exp) {
        xp += exp;

        // while (xp >= level * experiencePerLevel) {
        //     Debug.Log("Level Up");
        //     xp -= level * experiencePerLevel;
        //     level++;
        // }
    }
}
