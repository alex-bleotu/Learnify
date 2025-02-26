using UnityEngine;

public class SaveButtonHandler : MonoBehaviour
{
    public void OnPress()
    {
        SaveSystem.SaveData();
    }
}
