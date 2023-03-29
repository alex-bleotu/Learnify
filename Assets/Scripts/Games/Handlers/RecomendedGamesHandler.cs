using UnityEngine;
using UnityEngine.UI;

public class RecomendedGamesHandler : MonoBehaviour
{
    private int[] GetRandomValues()
    {
        System.Random random = new System.Random();

        int[] randomValues = new int[3];

        randomValues[0] = random.Next(0, TemporaryData.gameList.Count);
        randomValues[1] = random.Next(0, TemporaryData.gameList.Count);
        while (randomValues[1] == randomValues[0])
            randomValues[1] = random.Next(0, TemporaryData.gameList.Count);
        randomValues[2] = random.Next(0, TemporaryData.gameList.Count);
        while (randomValues[2] == randomValues[0] || randomValues[2] == randomValues[1])
            randomValues[2] = random.Next(0, TemporaryData.gameList.Count);

        return randomValues;
    }

    // private void buttonClick() {
    //     Debug.Log("lol");
    // }

    private void Start()
    {
        System.Random random = new System.Random();

        if (TemporaryData.gameList.Count >= 3)
        {
            int[] randomValues = GetRandomValues();

            // Debug.Log(randomValues[0] + ", " + randomValues[1] + ", " + randomValues[2]);

            int index = 0;
            GameObject buttonParent = GameObject.Find("RecommendedGames");

            foreach (Transform child in buttonParent.transform)
            {
                if (child.name == "TitleText (TMP)")
                    continue;

                GameObject game = child.gameObject;

                game.name = "RecommendedGame" + randomValues[index];

                // games[i].onClick.AddListener(buttonClick);

                // TMP_Text buttonText = game.transform.GetChild(1).GetComponent<TMP_Text>();
                // buttonText.text = GameList.gameList[randomValues[index]].GetTitle();

                game.transform.GetChild(0).GetComponent<Image>().sprite = TemporaryData.gameList[randomValues[index]].GetIcon();

                index++;
            }
        }
    }
}
