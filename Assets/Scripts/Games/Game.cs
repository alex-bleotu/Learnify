using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game
{
    public enum Difficulty { easy, medium, hard, veryHard };

    public enum Subject { math, romanian, science, english};

    public struct Questions {
        public string id;
        public string question;
        public List<string> answers;
        public int correct;
    }

    [System.Serializable]
    public struct Data {
        public bool leasonState;
        public int highestScore;
    }

    public struct Level {
        public int id;
        public int timer;
        public int gems;
        public int experience;
        public Difficulty difficulty;
        public List<Questions> questions;
        public int questionsCount;
    }

    private int id;

    private Sprite icon;
    private Sprite banner;

    private string title;
    private string description;

    private Subject subject;

    private string leason;
    private string info;

    private List<Level> levels;
    private int levelCount;

    private int currentLevel;

    private Data data;

    public Game(int id) {
        levels = new List<Level>();
        // levels.questions = new List<Questions>();

        ReadData.Read(ref title, ref description, ref subject, ref levels, 
            ref leason, ref info, Application.dataPath + "/Resources/Games/" + id + "/data.json");

        levelCount = levels.Count;

        data.leasonState = false;
        data.highestScore = 0;

        currentLevel = 0;

        icon = Resources.Load<Sprite>("Games/" + id + "/icon");
        banner = Resources.Load<Sprite>("Games/" + id + "/banner");

        if (icon == null)
            icon = Resources.Load<Sprite>("Games/Default/icon");
        if (banner == null)
            banner = Resources.Load<Sprite>("Games/Default/banner");
    }

    private int GetId() { return id; }
    public Sprite GetIcon() { return icon; }
    public Sprite GetBanner() { return banner; }
    public string GetTitle() { return title; }
    public string GetDescription() { return description; }
    public int GetExperience(int index) { return levels[index].experience; }
    public int GetGemReward(int index) { return levels[index].gems; }
    public Difficulty GetDifficulty(int index) { return levels[index].difficulty; }
    public Subject GetSubject() { return subject; }
    public bool GetLeasonState() { return data.leasonState; }
    public void SetLeasonState(bool leasonState) { this.data.leasonState = leasonState; }
    public List<Questions> GetQuestions(int index) { return levels[index].questions; }
    public int GetQuestionsCount(int index) { return levels[index].questionsCount; }
    public string GetLeason() { return leason; }
    public string GetInfo() { return info; }
    public Data GetData() { return data; }
    public void SetData(Data data) { this.data = data; }
    public int GetHighestScore() { return data.highestScore; }
    public void SetHighestScore(int highestScore) { this.data.highestScore = highestScore; }
    public int GetLevelCount() { return levelCount; }
    public int GetCurrentLevel() { return currentLevel; }
    public void SetCurrentLevel(int currentLevel) { this.currentLevel = (currentLevel >= levelCount) ? levelCount - 1 : currentLevel;}

    public static int GetIndex(string str) {
        string aux = string.Empty;
        int val = 0;

        for (int i = 0; i < str.Length; i++)
            if (char.IsDigit(str[i]))
                aux += str[i];

        if (aux.Length > 0)
            val = int.Parse(aux);

        return val;
    }
}
