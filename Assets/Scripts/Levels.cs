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
}
