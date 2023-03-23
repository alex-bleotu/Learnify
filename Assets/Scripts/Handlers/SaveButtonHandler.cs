using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonHandler : MonoBehaviour
{
    public void OnPress() {
        SaveSystem.SaveData();
    }
}
