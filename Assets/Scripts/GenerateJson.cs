using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateJson : MonoBehaviour
{
    private struct Game {
        public int id;
        public string title;
        public string type;
        public string subject;
        public string description;
        public string leason;
        public string info;
        public List<Level> levels;

        public Game(int id, string title, string type, string subject, string description, string leason, string info, List<Level> levels) {
            this.id = id;
            this.title = title;
            this.type = type;
            this.subject = subject;
            this.description = description;
            this.leason = leason;
            this.info = info;
            this.levels = levels;
        }
    }

    private struct Level {
        public int id;
        public int timer;
        public string difficulty;
        public List<Questions> questions;
    }

    public struct Questions {
        public string id;
        public string question;
        public List<string> answers;
        public int correct;
    }

    public static void Generate() {
        int levelCount = 1; // questionsCount = 1;

        List<Level> levels = new List<Level>();

        for (int i = 0; i < levelCount; i++) {
            levels.Add(new Level());

            Level aux = new Level();

            aux.id = i;

            aux.timer = 60;
            aux.difficulty = "easy";
            aux.questions = new List<Questions>();
        }

        Game game = new Game(0, "Teste", "test", "math", "Teste de matematica", "Leason", "Info", new List<Level>());
    }
}
