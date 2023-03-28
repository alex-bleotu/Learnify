using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ReadData
{
    private struct Data
    {
        public int id;
        public string title;
        public string type;
        public string subject;
        public string description;
        public string leason;
        public string info;
        public List<Level> levels;
    }

    private struct Level
    {
        public int id;
        public int timer;
        public string difficulty;
        public List<Game.Questions> questions;
    }

    public static void Read(ref int id, ref string title, ref Game.GameType type, ref string description,
        ref Game.Subject subject, ref List<Game.Level> levels, ref string leason, ref string info,
        string filePath)
    {
        // if (!File.Exists(fileData))
        //     return;

        TextAsset jsonFile = Resources.LoadAll<TextAsset>("Games/" + filePath)[0];

        // List<Data> data = new List<Data>();
        Data data = new Data();

        string json = jsonFile.text;
        data = JsonConvert.DeserializeObject<Data>(json);

        id = data.id;
        title = data.title;
        description = data.description;

        leason = data.leason;
        info = data.info;

        for (int i = 0; i < data.levels.Count; i++)
        {
            Game.Level aux = new Game.Level();
            aux.id = data.levels[i].id;
            aux.timer = data.levels[i].timer;
            aux.questionsCount = data.levels[i].questions.Count;
            aux.questions = data.levels[i].questions;

            switch (data.levels[i].difficulty)
            {
                case "easy":
                    aux.difficulty = Game.Difficulty.easy;
                    break;
                case "medium":
                    aux.difficulty = Game.Difficulty.medium;
                    break;
                case "hard":
                    aux.difficulty = Game.Difficulty.hard;
                    break;
                case "veryHard":
                    aux.difficulty = Game.Difficulty.veryHard;
                    break;
            }

            levels.Add(aux);
        }

        switch (data.subject)
        {
            case "math":
                subject = Game.Subject.math;
                break;
            case "romanian":
                subject = Game.Subject.romanian;
                break;
            case "science":
                subject = Game.Subject.english;
                break;
            case "english":
                subject = Game.Subject.science;
                break;
        }

        switch (data.type)
        {
            case "quiz":
                type = Game.GameType.quiz;
                break;
        }
    }
}
