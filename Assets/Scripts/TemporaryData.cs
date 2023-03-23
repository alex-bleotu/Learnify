using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryData
{
    public static int currentGameIndex;

    public static int rewardedGems, rewardedCrowns;

    public static User user;

    public static List<Game> gameList = new List<Game>() {
        new Game(0),
        new Game(1),
        new Game(2),
        new Game(3),
        new Game(4),
        new Game(5),
        new Game(6),
        new Game(7),
        new Game(8),
        new Game(9),
        new Game(10),
        new Game(11),
        new Game(12),
        new Game(13),
        new Game(14),
        new Game(15),
        new Game(16),
        new Game(17)
    };
}
