public class TubeData
{
    public bool Up { get; protected set; }
    public bool Down { get; protected set; }
    public bool Left { get; protected set; }
    public bool Right { get; protected set; }
    public int Rotate { get; protected set; }
    public int Sides => (Up ? 1 : 0) + (Down ? 1 : 0) + (Left ? 1 : 0) + (Right ? 1 : 0);
    public bool IsLine => (Up && Down && !Left && !Right) || (!Up && !Down && Left && Right);

    public TubeData(bool up, bool down, bool left, bool right, int rotate = 0)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;
        Rotate = rotate;
    }
}

public static class TubeDataHelper
{
    public static TubeData GetTileData(char c)
    {
        switch (c)
        {
            // 1:  ˂˃˄˅
            case '˂':
                return new TubeData(false, false, false, true, -90);
            case '˃':
                return new TubeData(false, false, true, false, 90);
            case '˄':
                return new TubeData(false, true, false, false, 180);
            case '˅':
                return new TubeData(true, false, false, false);
            // 2:  –|
            case '–':
                return new TubeData(false, false, true, true);
            case '|':
                return new TubeData(true, true, false, false, 90);
            // 2:  ⌈⌉⌊⌋
            case '⌈':
                return new TubeData(false, true, false, true, 90);
            case '⌉':
                return new TubeData(false, true, true, false);
            case '⌊':
                return new TubeData(true, false, false, true, 180);
            case '⌋':
                return new TubeData(true, false, true, false, -90);
            // 3:  ⊥⊤⊣⊢
            case '⊥':
                return new TubeData(true, false, true, true);
            case '⊤':
                return new TubeData(false, true, true, true, 180);
            case '⊣':
                return new TubeData(true, true, true, false, 90);
            case '⊢':
                return new TubeData(true, true, false, true, -90);
            // 4:  +
            case '+':
                return new TubeData(true, true, true, true);
            default:
                return null;
        }
    }
}