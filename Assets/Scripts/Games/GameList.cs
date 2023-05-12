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
        new Game("Adunare", "", 1, 1, Game.Difficulty.easy, Game.Subject.math, 0),
        new Game("Scădere", "", 1, 1, Game.Difficulty.easy, Game.Subject.math, 1),
        new Game("Înmulțire", "", 1, 1, Game.Difficulty.medium, Game.Subject.math, 2),
        new Game("Împărțire", "", 1, 1, Game.Difficulty.medium, Game.Subject.math, 3),
        new Game("game 5", "", 1, 1, Game.Difficulty.easy, Game.Subject.math, 4),
        new Game("game 6", "", 1, 1, Game.Difficulty.easy, Game.Subject.math, 5),
        new Game("Litere", "", 1, 1, Game.Difficulty.easy, Game.Subject.romanian, 6),
        new Game("game 8", "", 1, 1, Game.Difficulty.easy, Game.Subject.romanian, 7),
        new Game("game 9", "", 1, 1, Game.Difficulty.easy, Game.Subject.romanian, 8),
        new Game("game 10", "", 1, 1, Game.Difficulty.easy, Game.Subject.romanian, 9),
        new Game("game 11", "", 1, 1, Game.Difficulty.easy, Game.Subject.romanian, 10),
        new Game("game 12", "", 1, 1, Game.Difficulty.easy, Game.Subject.romanian, 11),
        new Game("game 13", "", 1, 1, Game.Difficulty.easy, Game.Subject.science, 12),
        new Game("game 14", "", 1, 1, Game.Difficulty.easy, Game.Subject.science, 13),
        new Game("game 15", "", 1, 1, Game.Difficulty.easy, Game.Subject.science, 14),
        new Game("game 16", "", 1, 1, Game.Difficulty.easy, Game.Subject.science, 15),
        new Game("game 17", "", 1, 1, Game.Difficulty.easy, Game.Subject.science, 16),
        new Game("game 18", "", 1, 1, Game.Difficulty.easy, Game.Subject.science, 17)
    };
}
