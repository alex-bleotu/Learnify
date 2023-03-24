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
        public List<Game.Level> levels;
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

        levels = data.levels;

        // for (int i = 0; i < data.levels.Count; i++) {
        //     switch(data.levels[i].difficulty) {
        //         case Game.Difficulty.easy:
        //             levels[i].difficulty = Game.Difficulty.easy;
        //             break;
        //         case "medium":
        //             levels[i].difficulty = Game.Difficulty.medium;
        //             break;
        //         case "hard":
        //             levels[i].difficulty = Game.Difficulty.hard;
        //             break;
        //         case "veryHard":
        //             levels[i].difficulty = Game.Difficulty.veryHard;
        //             break;
        //     }
        // }

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
