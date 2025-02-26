using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingPage : MonoBehaviour
{
    public GameObject loadingPage;
    public Image loadingBar;

    public GameObject errorText;

    private string scene;

    void Start()
    {
        scene = null;

        errorText.SetActive(false);

        loadingPage.SetActive(false);

        TemporaryData.gameList = new List<Game>();

        TextAsset[] objectArray = Resources.LoadAll<TextAsset>("Games");

        foreach (TextAsset asset in objectArray)
            TemporaryData.gameList.Add(new Game(asset.name));

        TemporaryData.gameList = TemporaryData.gameList.OrderBy(x => x.GetId()).ToList();

        SaveSystem.LoadData();

        try
        {
            FriendsPageHandler friendsPageHandler = new FriendsPageHandler();
            friendsPageHandler.CreateList();

            loadingPage.SetActive(true);

            TimerSystem.TimerStart(250, LoadingBar);

            TimerSystem.TimerStart(3000, LoadScene);
        }
        catch (System.Exception e)
        {
            errorText.GetComponent<TMP_Text>().text = e.ToString();
        }
    }

    private void LoadScene()
    {
        try
        {
            if (TemporaryData.user != null && TemporaryData.gameList != null)
                scene = "MainPage";
            else
                scene = "CreateProfilePage";
        }
        catch (System.Exception e)
        {
            errorText.GetComponent<TMP_Text>().text = e.ToString();
        }
    }

    private void LoadingBar()
    {
        try {
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

        UnityMainThreadDispatcher.Instance().Destroy();
        } catch {}
    }

    private void Update()
    {
        if (scene != null)
        {
            try
            {
                SceneManager.LoadScene(scene);
                scene = null;
            }
            catch (System.Exception e)
            {
                errorText.GetComponent<TMP_Text>().text = e.ToString();
            }
        }
    }
}
