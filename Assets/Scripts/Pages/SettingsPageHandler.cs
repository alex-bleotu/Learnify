using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsPageHandler : MonoBehaviour
{
    public Slider audioSlider;

    public void Start()
    {
        if (TemporaryData.user.GetMute())
            audioSlider.value = 0f;
        else
            audioSlider.value = TemporaryData.user.GetVolume();
    }

    public void CloseInterface()
    {
        SceneManager.LoadScene("MainPage");
    }

    public void UpdateSlider()
    {
        if (audioSlider.value != 0f)
            TemporaryData.user.SetVolume(audioSlider.value);

        if (audioSlider.value == 0f)
            TemporaryData.user.SetMute(true);
        else
            TemporaryData.user.SetMute(false);
    }

    public void OnGuideButton()
    {
        Application.OpenURL("https://docs.google.com/document/d/1Db-EzgW_x_OTb5SrT9x_Xse4f6TTpOFOY7zRDD1X6AY/edit?usp=sharing");
    }
}
