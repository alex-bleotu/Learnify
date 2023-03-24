using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ReadData
{
    private struct Data {
        public string title;
        public string subject;
        public string description;
        public string leason;
        public string info;
        public List<Level> levels;
    }

    private struct Level {
        public int id;
        public int timer;
        public int gems;
        public int experience;
        public string difficulty;
        public List<Game.Questions> questions;
    }

    public static void Read(ref string title, ref string description, 
        ref Game.Subject subject, ref List<Game.Level> levels, ref string leason, ref string info, 
        string fileData) {
    
        if (!File.Exists(fileData))
            return;

        // List<Data> data = new List<Data>();
        Data data = new Data();

        using (StreamReader r = new StreamReader(fileData)) {
            string json = r.ReadToEnd();
            data = JsonConvert.DeserializeObject<Data>(json);
        }

        title = data.title;
        description = data.description;

        leason = data.leason;
        info = data.info;

        for (int i = 0; i < data.levels.Count; i++) {
            Game.Level aux = new Game.Level();
            aux.id = data.levels[i].id;
            aux.questionsCount = data.levels[i].questions.Count;
            aux.timer = data.levels[i].timer;
            aux.gems = data.levels[i].gems;
            aux.experience = data.levels[i].experience;
            aux.questions = data.levels[i].questions;

            switch(data.levels[i].difficulty) {
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

        switch(data.subject) {
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
    }
}
