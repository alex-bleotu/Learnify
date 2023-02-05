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

    public struct Leason {
        public string leason;
    }

    public static void Read(ref List<string> questions, ref List<List<string>> answers, ref List<int> correctAnswers, ref string leason, string fileData, string fileLeason) {
        if (!File.Exists(fileData))
            return;

        List<Data> data = new List<Data>();
        Leason leasonText = new Leason();

        using (StreamReader r = new StreamReader(fileData)) {
            string json = r.ReadToEnd();
            data = JsonConvert.DeserializeObject<List<Data>>(json);
        }

        using (StreamReader r = new StreamReader(fileLeason)) {
            string json = r.ReadToEnd();
            leasonText = JsonConvert.DeserializeObject<Leason>(json);
        }

        int count = data.Count;

        leason = leasonText.leason;

        for (int i = 0; i < count; i++) {
            questions.Add(data[i].question);
            answers.Add(data[i].answers);
            correctAnswers.Add(data[i].correct);

            // Debug.Log(data[i].correct);
        }
    }
}
