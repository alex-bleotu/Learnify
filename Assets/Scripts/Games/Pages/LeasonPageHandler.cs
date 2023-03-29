using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LeasonPageHandler : MonoBehaviour
{
    public GameObject ttsButton;

    public TMP_Text leasonText;
    public TMP_Text leasonTitleText;

    public TTSHandler ttsHandler;

    private int gameIndex;

    private void Start()
    {
        gameIndex = TemporaryData.currentGameIndex;

        leasonTitleText.text = TemporaryData.gameList[gameIndex].GetTitle();
        leasonText.text = TemporaryData.gameList[gameIndex].GetLeason();

        if (Application.internetReachability == NetworkReachability.NotReachable)
            ttsButton.GetComponent<Button>().interactable = false;
    }

    public void OnTTSButton()
    {
        try
        {
            string text = "Lecția, " + leasonTitleText.text + ". " + leasonText.text;

            ttsHandler.PlayTTS(text);
        }
        catch
        {
            ttsHandler.PlayTTS("Nu am putut citi întrebarea");
        }
    }

    public void CloseInterface()
    {
        SceneManager.LoadScene("MainPage");
    }
}
