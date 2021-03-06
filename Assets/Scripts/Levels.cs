using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Levels
{
    // l r u d – город лево/право/верх/низ
    // 1 2 3 4 – генератор лево/право/верх/низ
    public static readonly char[,] Level1 = new char[5, 5] {
        { ' ', ' ', ' ', ' ', 'l' },
        { ' ', 'w', ' ', ' ', 'w' },
        { ' ', ' ', 'w', ' ', ' ' },
        { ' ', ' ', ' ', 'w', ' ' },
        { '2', ' ', ' ', ' ', ' ' },
    };

    // 1:  ˂˃˄˅
    // 2:  –|
    // 2:  ⌈⌉⌊⌋
    // 3:  ⊥⊤⊣⊢
    // 4:  +
    public static readonly string Level1Tubes = "–˂˄⊥⌈⊣⌊–⌋|⌉";


    // l r u d – город лево/право/верх/низ
    // 1 2 3 4 – генератор лево/право/верх/низ
    public static readonly char[,] Level2 = new char[7, 7] {
        { ' ', 'w', ' ', ' ', ' ' , ' ' , ' ' },
        { ' ', ' ', ' ', 'w', ' ' , ' ' , 'u' },
        { ' ', 'l', ' ', ' ', ' ' , 'w' , 'w' },
        { ' ', ' ', 'w', 'w', ' ' , ' ' , ' ' },
        { 'w', ' ', ' ', ' ', 'w' , ' ' , ' ' },
        { ' ', ' ', 'w', ' ', 'w' , ' ' , ' ' },
        { '3', ' ', 'w', ' ', ' ' , ' ' , ' ' },
    };

    // 1:  ˂˃˄˅
    // 2:  –|
    // 2:  ⌈⌉⌊⌋
    // 3:  ⊥⊤⊣⊢
    // 4:  +
    public static readonly string Level2Tubes = "⌈⌊⌈˅⊣⌈–˄⊣⌊––⌈|⌊–⌊⊣⊢⌉–˃⊤|⊢⊥˃˃˃⌉";


    public static readonly char[,] Level3 = new char[5, 5] {
        { ' ', ' ', ' ', 'w', ' ' },
        { ' ', 'w', ' ', 'w', ' ' },
        { ' ', ' ', 'w', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', 'w', ' ', ' ', 'w' },
    };

    public static List<TubeData> GetTilesData(string s) => s.Select(e => TubeDataHelper.GetTileData(e)).ToList();
}
