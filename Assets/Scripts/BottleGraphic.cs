using UnityEngine;

public class BottleGraphic : MonoBehaviour
{
    public GameGraphic gameGraphic;

    public int index;
    public CubeGraphic[] cubeGraphics;

    private void OnMouseUpAsButton()
    {
        gameGraphic.OnClickBottle(index);
    }
    public void SetGraphic(Game.CubeType[] cubeType)
    {
        for (int i = 0; i < cubeGraphics.Length; i++) 
        {
            if (i >= cubeType.Length)
            {
                cubeGraphics[i].SetColor(CubeGraphicType.NONE);
            }
            else
            {
                CubeGraphicType type = CubeGraphicType.BLUE;

                switch (cubeType[i])
                {
                    case Game.CubeType.BLUE:
                        type = CubeGraphicType.BLUE;
                        break;
                    case Game.CubeType.GREEN:
                        type = CubeGraphicType.GREEN;
                        break;
                    case Game.CubeType.RED:
                        type = CubeGraphicType.RED;
                        break;
                }
                cubeGraphics[i].SetColor(type);
            }
        }
    }
}
