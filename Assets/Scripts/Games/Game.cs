using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game
{
    public enum Difficulty { easy, medium, hard, veryHard };

    public enum Subject { math, romanian, science, english};

    private int id;

    private Sprite icon;
    private Sprite banner;

    private string title;
    private string description;


    private int recommendedLevel;
    private int experience;

    private Difficulty currentDifficulty;
    private Subject subject;

    private bool leasonState;

    private List<string> questions;
    private List<List<string>> answers;
    private List<int> correctAnswers;

    public Game(string title, string description, int recommendedLevel, int experience, Difficulty currentDifficulty, Subject subject, int id) {
        this.title = title;
        this.description = description;
        this.recommendedLevel = recommendedLevel;
        this.experience = experience;
        this.currentDifficulty = currentDifficulty;
        this.subject = subject;
        this.id = id;

        questions = new List<string>();
        answers = new List<List<string>>();
        correctAnswers = new List<int>();

        ReadData.Read(ref questions, ref answers, ref correctAnswers, Application.dataPath + "/Resources/Games/" + id + "/data.json");

        leasonState = false;

        icon = Resources.Load<Sprite>("Games/" + id + "/icon");
        banner = Resources.Load<Sprite>("Games/" + id + "/banner");

        if (icon == null)
            icon = Resources.Load<Sprite>("Games/Default/icon");
        if (banner == null)
            banner = Resources.Load<Sprite>("Games/Default/banner");
    } 

    public Sprite GetIcon() { return icon; }
    public Sprite GetBanner() { return banner; }
    public string GetTitle() { return title; }
    public string GetDescription() { return description; }
    public int GetRecommendedLevel() { return recommendedLevel; }
    public int GetExperience() { return experience; }
    public Difficulty GetDifficulty() { return currentDifficulty; }
    public void SetDifficulty(Difficulty currentDifficulty) { this.currentDifficulty = currentDifficulty; }
    public Subject GetSubject() { return subject; }
    public bool GetLeasonState() { return leasonState; }
    public void SetLeasonState(bool leasonState) { this.leasonState = leasonState; }
    public List<string> GetQuestions() { return questions; }
    public List<List<string>> GetAnwers() { return answers; }
    public List<int> GetCorrectAnswers() { return correctAnswers; }
}
