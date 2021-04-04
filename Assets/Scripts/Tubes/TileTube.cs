using UnityEngine;

public class TileTube : MonoBehaviour
{
    public UI2DSprite Sprite;
    public TubeData Data { get; protected set; }

    public void Set(TubeData data)
    {
        Data = data;

        switch (data.Sides)
        {
            case 1:
                Sprite.sprite2D = ArtCollection.Instance.Tube1;
                break;
            case 2:
                Sprite.sprite2D = data.IsLine ? ArtCollection.Instance.Tube2Line : ArtCollection.Instance.Tube2;
                break;
            case 3:
                Sprite.sprite2D = ArtCollection.Instance.Tube3;
                break;
            case 4:
                Sprite.sprite2D = ArtCollection.Instance.Tube4;
                break;
        }

        this.transform.rotation = Quaternion.Euler(0, 0, data.Rotate);
    }
}
