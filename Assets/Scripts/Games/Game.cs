using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Game
{
    public enum Difficulty { easy, medium, hard, veryHard };

    public enum Subject { math, romanian, science, english};

    public enum GameType { quiz };

    public struct Questions {
        public string id;
        public string question;
        public List<string> answers;
        public int correct;
    }

    [System.Serializable]
    public struct Data {
        public bool leasonState;
        public int experience;
    }

    public struct Level {
        public int id;
        public int timer;
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
    private GameType gameType;

    private string leason;
    private string info;

    private List<Level> levels;
    private int levelCount;

    private int currentLevel;

    private int experience;

    private Data data;

    public Game(string path, string directory) {
        levels = new List<Level>();
        // levels.questions = new List<Questions>();

        ReadData.Read(ref id, ref title, ref gameType, ref description, ref subject, ref levels, 
            ref leason, ref info, path);

        levelCount = levels.Count;

        data.leasonState = false;
        data.experience = 0;

        levels = levels.OrderBy(x => x.id).ToList();

        currentLevel = 0;

        icon = Resources.Load<Sprite>("Games/" + directory + "/icon");
        banner = Resources.Load<Sprite>("Images/Banners/" + subject.ToString());

        if (icon == null)
            icon = Resources.Load<Sprite>("Games/Default/icon");
        if (banner == null)
            banner = Resources.Load<Sprite>("Images/Banners/default");
    }

    public int GetId() { return id; }
    public Sprite GetIcon() { return icon; }
    public Sprite GetBanner() { return banner; }
    public string GetTitle() { return title; }
    public string GetDescription() { return description; }
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
    public int GetExperience() { return data.experience; }
    public int GetLevelCount() { return levelCount; }
    public int GetCurrentLevel() { return currentLevel; }
    public void SetCurrentLevel(int currentLevel) { this.currentLevel = (currentLevel >= levelCount) ? levelCount - 1 : currentLevel;}
    public GameType GetGameType() { return gameType; }
    public void SetGameType(GameType type) { this.gameType = type; }
    public List<Level> GetLevels(int index) { return levels; }
    public int GetTimer(int index) { return levels[index].timer; }

    public void AddExperience(int experience) {
        data.experience += experience;
    }

    public int GetGemReward(int index) {
        if (levels[index].difficulty == Difficulty.easy)
            return 1;
        else if (levels[index].difficulty == Difficulty.medium)
            return 2;
        else if (levels[index].difficulty == Difficulty.hard)
            return 3;
        else if (levels[index].difficulty == Difficulty.veryHard)
            return 5;
        return 0;
    }

    public int GetExperience(int index) {
        if (levels[index].difficulty == Difficulty.easy)
            return 10;
        else if (levels[index].difficulty == Difficulty.medium)
            return 12;
        else if (levels[index].difficulty == Difficulty.hard)
            return 15;
        else if (levels[index].difficulty == Difficulty.veryHard)
            return 20;
        return 0;
    }

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
