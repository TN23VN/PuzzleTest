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
}
