using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class LoadingPage : MonoBehaviour
{
    public GameObject loadingPage;
    public Image loadingBar;

    void Start()
    {
        loadingPage.SetActive(false);

        string path = Application.dataPath + "/Resources/Games/";

        string[] directories = Directory.GetDirectories(path);
        foreach (string directory in directories)
        {
            if (!directory.EndsWith("Default"))
            {
                string filePath = directory + "/data.json";

                if (File.Exists(filePath))
                    TemporaryData.gameList.Add(new Game(filePath, directory.Split('/').Last()));
            }
        }

        TemporaryData.gameList = TemporaryData.gameList.OrderBy(x => x.GetId()).ToList();

        SaveSystem.LoadData();

        FriendsPageHandler friendsPageHandler = new FriendsPageHandler();
        friendsPageHandler.CreateList();

        if (TemporaryData.loading)
        {
            loadingPage.SetActive(true);

            TimerSystem.TimerStart(250, LoadingBar);

            TimerSystem.TimerStart(3000, () =>
            {
                if (TemporaryData.user != null && TemporaryData.gameList != null)
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        SceneManager.LoadScene("MainPage");
                    });
                else
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        SceneManager.LoadScene("CreateProfilePage");
                    });
            });
        }
        else
        {
            if (TemporaryData.user != null && TemporaryData.gameList != null)
                SceneManager.LoadScene("MainPage");
            else
                SceneManager.LoadScene("CreateProfilePage");
        }
    }

    private void LoadingBar()
    {
        TimerSystem.StartStopWatch();

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            loadingBar.fillAmount = 1f;
        });

        while (TimerSystem.GetStopWatchTime() < 2500)
            if (TimerSystem.GetStopWatchTime() % 50 == 0)
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    loadingBar.fillAmount = 1f - 1f * (TimerSystem.GetStopWatchTime() / 2500f);
                });

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            loadingBar.fillAmount = 0f;
        });

        TimerSystem.StopStopWatch();
    }
}
