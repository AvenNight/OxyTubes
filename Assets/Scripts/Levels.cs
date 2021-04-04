using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Levels
{
    public static readonly char[,] Level1 = new char[5, 5] {
        { ' ', ' ', ' ', ' ', 'w' },
        { ' ', 'w', ' ', ' ', 'w' },
        { ' ', ' ', 'w', ' ', ' ' },
        { ' ', ' ', ' ', 'w', ' ' },
        { 'w', ' ', ' ', ' ', ' ' },
    };

    // 1:  ˂˃˄˅
    // 2:  –|
    // 2:  ⌈⌉⌊⌋
    // 3:  ⊥⊤⊣⊢
    // 4:  +
    public static readonly string Level1Tubes = "–˂˄⊥⌈⊣⌊–⌋|⌉";

    public static readonly char[,] Level2 = new char[3, 5] {
        { ' ', ' ', ' ', 'w', ' ' },
        { ' ', 'w', ' ', 'w', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
    };

    public static readonly char[,] Level3 = new char[5, 5] {
        { ' ', ' ', ' ', 'w', ' ' },
        { ' ', 'w', ' ', 'w', ' ' },
        { ' ', ' ', 'w', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', 'w', ' ', ' ', 'w' },
    };

    public static List<TileData> GetTilesData(string s) => s.Select(e => TileDataHelper.GetTileData(e)).ToList();
}
