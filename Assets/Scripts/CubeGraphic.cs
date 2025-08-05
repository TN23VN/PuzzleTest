using UnityEngine;


public enum CubeGraphicType
{
    NONE,
    BLUE,
    GREEN,
    RED
}
public class CubeGraphic : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(CubeGraphicType type)
    {
        switch (type)
        {
            case CubeGraphicType.NONE:
                spriteRenderer.color = new Color(0,0,0,0);
                break;
            case CubeGraphicType.BLUE:
                spriteRenderer.color = Color.blue; 
                break;
            case CubeGraphicType.GREEN:
                spriteRenderer.color = Color.green;
                break;
            case CubeGraphicType.RED:
                spriteRenderer.color = Color.red;
                break;

        }
    }

    public static CubeGraphicType ConvertFromGameType(Game.CubeType cubeType)
    {
        switch (cubeType)
        {
            case Game.CubeType.BLUE:
                return CubeGraphicType.BLUE;
            case Game.CubeType.GREEN:
                return CubeGraphicType.GREEN;
            case Game.CubeType.RED:
                return CubeGraphicType.RED;
        }
        return CubeGraphicType.NONE;
    }
}
