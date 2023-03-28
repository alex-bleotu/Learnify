using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryData
{
    public static bool loading = true;

    public static int currentGameIndex;

    public static int rewardedGems, rewardedCrowns, rewardedExperience;

    public static User user;

    public static List<Game> gameList = new List<Game>();

    public static Sprite avatar;

    public static List<FriendsPageHandler.Friend> friends = new List<FriendsPageHandler.Friend>();
}
