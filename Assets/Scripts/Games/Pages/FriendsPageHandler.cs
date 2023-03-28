using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendsPageHandler : MonoBehaviour
{
    private int friendsCount = 19;

    public struct Friend
    {
        public string username;
        public int xp;
        public Sprite avatar;
    }

    private List<string> usernames = new List<string> {
        "Mihai", "Robert", "Paul", "Vlad", "Mateo", "Ioana", "Julia", "Adelina", "Laura",
        "Raluca", "Iulia", "Andrei", "Marius", "Darius", "Luca", "Cristi", "Catalin",
        "Lavinia", "Antonia", "Denisa", "Cosmin", "Rafael", "Liviu", "Sofia", "Emilia"
    };

    private List<string> avatars = new List<string>();

    public GameObject friendTemplate;
    public GameObject friendslist;

    private float coordX;
    private float coordY;
    private float spacingCoord = 70;

    private int userIndex;

    private Color32 highLight = new Color32(244, 144, 0, 255);

    static void ShuffleList(List<string> list)
    {
        System.Random random = new System.Random();

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            string temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    private void FindUserIndex()
    {
        for (int i = 0; i < friendsCount + 1; i++)
            if (TemporaryData.friends[i].username == TemporaryData.user.GetUsername())
            {
                userIndex = i;
                break;
            }
    }

    public string GetRandomAvatar()
    {
        Object[] avatars = Resources.LoadAll("Avatars");

        return avatars[UnityEngine.Random.Range(0, avatars.Length)].name;
    }

    public void CreateList()
    {
        ShuffleList(usernames);

        for (int i = 0; i < friendsCount; i++)
        {
            Friend aux = new Friend();

            aux.username = usernames[i];
            aux.xp = UnityEngine.Random.Range(0, 300);
            aux.avatar = Resources.Load<Sprite>("Avatars/" + GetRandomAvatar());

            TemporaryData.friends.Add(aux);
        }

        Friend userAux = new Friend();
        userAux.username = TemporaryData.user.GetUsername();
        userAux.xp = TemporaryData.user.GetXP();
        userAux.avatar = TemporaryData.avatar;

        TemporaryData.friends.Add(userAux);

        TemporaryData.friends = TemporaryData.friends.OrderByDescending(x => x.xp).ToList();
    }

    public void RefreshUserXP()
    {
        FindUserIndex();

        Friend aux = new Friend();

        aux.username = TemporaryData.user.GetUsername();
        aux.xp = TemporaryData.user.GetXP();
        aux.avatar = TemporaryData.avatar;

        TemporaryData.friends[userIndex] = aux;

        TemporaryData.friends = TemporaryData.friends.OrderByDescending(x => x.xp).ToList();

        FindUserIndex();
    }

    public void FriensListHandler()
    {
        RefreshUserXP();

        coordX = friendTemplate.transform.position.x;
        coordY = friendTemplate.transform.position.y;
        spacingCoord = friendTemplate.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < friendsCount + 1; i++)
        {
            GameObject copyFriend = Instantiate(friendTemplate, new Vector3(0, 0, 0), Quaternion.identity);

            copyFriend.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = TemporaryData.friends[i].username;
            copyFriend.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = TemporaryData.friends[i].xp + "XP";
            copyFriend.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = (i + 1) + ".";
            copyFriend.transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = TemporaryData.friends[i].avatar;

            if (i == userIndex)
            {
                copyFriend.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = highLight;
                copyFriend.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().color = highLight;
                copyFriend.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().color = highLight;

                copyFriend.transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<Image>().color = highLight;
            }

            copyFriend.transform.SetParent(friendslist.transform);

            copyFriend.transform.position = new Vector3(friendslist.transform.position.x, coordY - spacingCoord * i, 0);
            copyFriend.name = "Friend" + i;
            copyFriend.SetActive(true);
        }

        if (friendsCount <= 5)
            friendslist.transform.parent.GetComponent<ScrollRect>().enabled = false;

        friendslist.GetComponent<RectTransform>().sizeDelta = new Vector2(350, (friendsCount + 1) * 70);

        if (userIndex > friendsCount - 4)
            friendslist.transform.position = new Vector3(friendslist.transform.position.x, (friendsCount - 4) * spacingCoord, 0);
        else friendslist.transform.position = new Vector3(friendslist.transform.position.x, userIndex * spacingCoord - 165, 0);
    }
}
