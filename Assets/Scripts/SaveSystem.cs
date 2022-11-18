using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    private static readonly string FileName = "user.data";

    public static void SaveData(User user)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + FileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        Debug.Log(path);

        formatter.Serialize(stream, user);
        stream.Close();
    }
    
    public static User LoadData()
    {
        string path = Application.persistentDataPath + "/" + FileName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            User user = formatter.Deserialize(stream) as User;
            stream.Close();

            return user;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
