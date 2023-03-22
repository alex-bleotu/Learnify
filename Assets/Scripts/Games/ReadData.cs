using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ReadData
{
    private struct Data {
        public string title;
        public int level;
        public int experience;
        public string difficulty;
        public string subject;
        public string description;
        public string leason;
        public string info;
        public List<Game.Questions> questions;
    }

    public static void Read(ref string title, ref string description, ref int level, 
        ref int experience, ref Game.Difficulty difficulty, ref Game.Subject subject, 
        ref List<Game.Questions> questions, ref string leason, ref string info, string fileData) {
    
        if (!File.Exists(fileData))
            return;

        // List<Data data = new List<Data>();
        Data data = new Data();

        using (StreamReader r = new StreamReader(fileData)) {
            string json = r.ReadToEnd();
            data = JsonConvert.DeserializeObject<Data>(json);
        }

        title = data.title;
        description = data.description;
        level = data.level;
        experience = data.experience;

        leason = data.leason;
        info = data.info;

        questions = data.questions;

        switch(data.difficulty) {
            case "easy":
                difficulty = Game.Difficulty.easy;
                break;
            case "medium":
                difficulty = Game.Difficulty.medium;
                break;
            case "hard":
                difficulty = Game.Difficulty.hard;
                break;
            case "veryHard":
                difficulty = Game.Difficulty.veryHard;
                break;
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
