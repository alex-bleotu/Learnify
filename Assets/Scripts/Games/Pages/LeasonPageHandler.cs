using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeasonPageHandler : MonoBehaviour
{
    private TMP_Text leasonText;
    private TMP_Text leasonTitleText;

    IEnumerator LoadScene(int index) {
        while (SceneManager.GetActiveScene().buildIndex != index)
            yield return null;

        if (SceneManager.GetActiveScene().buildIndex == index)
            UpdateText(index);
            
        Debug.Log(SceneManager.GetActiveScene().buildIndex == index);
    }

    private void UpdateText(int index) {
        Debug.Log(GameObject.Find("LeasonText (TMP)").GetComponent<TMP_Text>());

        leasonText = GameObject.Find("LeasonText (TMP)").GetComponent<TMP_Text>();
        leasonTitleText = GameObject.Find("LeasonTitleText (TMP)").GetComponent<TMP_Text>();

        leasonTitleText.text = GameList.gameList[index].GetTitle();
        leasonText.text = GameList.gameList[index].GetLeason();
    }

    public void OpenInterface(int index) {
        SceneManager.LoadScene(3);

        // while (SceneManager.GetActiveScene().buildIndex != index);

        // UpdateText(index);
            // StartCoroutine("LoadScene", 3);
    }

    public void CloseInterface() {
        SceneManager.LoadScene(1);
    }
}
