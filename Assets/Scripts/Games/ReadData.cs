using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ReadData
{
    public struct Data {
        public int id;
        public string question;
        public List<string> answers;
        public int correct;
    }

    public static void Read(ref List<string> questions, ref List<List<string>> answers, ref List<int> correctAnswers, string file) {
        if (!File.Exists(file))
            return;

        List<Data> data = new List<Data>();

        using (StreamReader r = new StreamReader(file)) {
            string json = r.ReadToEnd();
            data = JsonConvert.DeserializeObject<List<Data>>(json);
        }

        int count = data.Count;

        for (int i = 0; i < count; i++) {
            questions.Add(data[i].question);
            answers.Add(data[i].answers);
            correctAnswers.Add(data[i].correct);

            // Debug.Log(data[i].correct);
        }
    }
}
