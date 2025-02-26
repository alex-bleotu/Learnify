using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class VideoHandler : MonoBehaviour
{
    public GameObject videoPanel;
    public VideoPlayer videoPlayer;
    public Button ttsButton;
    public Slider progress;
    public TMP_Text timeText;

    void Start()
    {
        // videoPlayer.Prepare();
        if (TemporaryData.gameList[TemporaryData.currentGameIndex].GetVideoState())
        {
            videoPanel.SetActive(true);
            ttsButton.interactable = false;
            videoPlayer.clip = TemporaryData.gameList[TemporaryData.currentGameIndex].GetVideo();
        }
        else if (Application.internetReachability != NetworkReachability.NotReachable)
            ttsButton.interactable = true;
    }

    public void OnButton()
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
        else
            videoPlayer.Play();
    }

    void Update()
    {
        try
        {
            if (videoPlayer.frameCount > 0)
            {
                progress.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
                timeText.text = videoPlayer.time.ToString("00:00");
            }
        }
        catch { }
    }

    public void OnSldierUpdate()
    {
        // videoPlayer.frame = (long)(progress.value * videoPlayer.frameCount);
    }
}
