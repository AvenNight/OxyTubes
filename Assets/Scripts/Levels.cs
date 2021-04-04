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

    public static readonly string Level1Tubes = "–⊢⊤⊥⌈⊣⌊–⌋|⌉";

    public static List<TileData> GetTilesData(string s)
    {
        //var result = new List<TileData>();
        //return result;
        return s.Select(e => GetTileData(e)).ToList();
    }

    public static TileData GetTileData(char c)
    {
        switch (c)
        {
            // 1:  ˂˃˄˅
            case '˂':
                return new TileData(false, false, false, true);
            case '˃':
                return new TileData(false, false, true, false);
            case '˄':
                return new TileData(false, true, false, false);
            case '˅':
                return new TileData(true, false, false, false);
            // 2:  –|
            case '–':
                return new TileData(false, false, true, true);
            case '|':
                return new TileData(true, true, false, false);
            // 2:  ⌈⌉⌊⌋
            case '⌈':
                return new TileData(false, true, false, true);
            case '⌉':
                return new TileData(false, true, true, false);
            case '⌊':
                return new TileData(true, false, false, true);
            case '⌋':
                return new TileData(true, false, true, false);
            // 3:  ⊥⊤⊣⊢
            case '⊥':
                return new TileData(true, false, true, true);
            case '⊤':
                return new TileData(false, true, true, true);
            case '⊣':
                return new TileData(true, true, true, false);
            case '⊢':
                return new TileData(true, true, false, true);
            // 4:  +
            case '+':
                return new TileData(true, true, true, true);
            default:
                return null;
        }
    }

    //public static readonly char[] Level1Tubes = new char[]
    //{
    //    '–','⊢','⊤','–','–','–','–','–','–','–','–','–','–','–',
    //};


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
    public static Vector2 ZeroPos = new Vector2(-1000 , -400);
    public static Vector2 ScrollPos = new Vector2(700 , 200);
    public static Vector2 ScrollBoundsMin = new Vector2(500 , -500);
    public static Vector2 ScrollBoundsMax = new Vector2(900 , 500);
}
