using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public Sprite Ingridents;
    public Sprite Radio;
    public Sprite GoldenSpatula;
    public Sprite Marketing;
    public Sprite Bell;

    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }

    }
}
