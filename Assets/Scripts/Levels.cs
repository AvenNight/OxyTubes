using UnityEngine;

public static class Levels
{
    public static readonly char[,] Level1 = new char[3, 3] {
        { ' ', ' ', ' ' },
        { ' ', 'w', ' ' },
        { ' ', ' ', ' ' },
    };

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
