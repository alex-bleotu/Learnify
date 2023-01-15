using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game
{
    public enum difficulty { easy, medium, hard, veryHard };

    private Image icon;
    private Image banner;

    private string title;
    private string description;


    private int recommendedLevel;
    private int experience;

    private difficulty currentDifficulty;

    private int sceneIndex;

    public Game(string title, string description, int recommendedLevel, int experience, difficulty currentDifficulty) {
        this.title = title;
        this.description = description;
        this.recommendedLevel = recommendedLevel;
        this.experience = experience;
        this.currentDifficulty = currentDifficulty;

        // icon.sprite = Resources.Load<Sprite>("Games/" + title + "/icon");
        // banner.sprite = Resources.Load<Sprite>("Games/" + title + "/banner");
    } 

    public Image GetIcon() { return icon; }
    public Image GetBanner() { return banner; }
    public string GetTitle() { return title; }
    public string GetDescription() { return description; }
    public int GetRecommendedLevel() { return recommendedLevel; }
    public int GetExperience() { return experience; }
    public difficulty GetDifficulty() { return currentDifficulty; }
    public void SetDifficulty(difficulty currentDifficulty) { this.currentDifficulty = currentDifficulty; }
    public int GetSceneIndex() { return sceneIndex; }
}
