using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoHandler : MonoBehaviour
{
    public GameObject videoPanel;
    public VideoPlayer videoPlayer;
    public Slider progress;

    void Start()
    {
        // videoPlayer.Prepare();
        if (TemporaryData.gameList[TemporaryData.currentGameIndex].GetVideoState())
        {
            videoPanel.SetActive(true);
            videoPlayer.url = TemporaryData.gameList[TemporaryData.currentGameIndex].GetVideoPath();
        }
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
                progress.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        }
        catch { }
    }

    public void OnSldierUpdate()
    {
        // videoPlayer.frame = (long)(progress.value * videoPlayer.frameCount);
    }
}
