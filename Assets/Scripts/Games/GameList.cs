using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameList
{
    public static int GetIndex(string str) {
        string aux = string.Empty;
        int val = 0;

        for (int i = 0; i < str.Length; i++)
            if (char.IsDigit(str[i]))
                aux += str[i];

        if (aux.Length > 0)
            val = int.Parse(aux);

        return val;
    }

    public static Game[] gameList = new Game[] {
        new Game("game 1", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 2", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 3", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 4", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 5", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 6", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 7", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 8", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 9", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 10", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 11", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 12", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 13", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 14", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 15", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 16", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 17", "lol", 1, 1, Game.difficulty.easy),
        new Game("game 18", "lol", 1, 1, Game.difficulty.easy)
    };
}
