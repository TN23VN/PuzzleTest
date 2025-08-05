using UnityEngine;

public class BottleGraphic : MonoBehaviour
{
    public GameGraphic gameGraphic;

    public int index;
    public CubeGraphic[] cubeGraphics;
    public Transform bottleUpTransform;

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
                SetGraphicNone(i);
            }
            else
            {
                SetGraphic(i, cubeType[i]);
            }
        }
    }

    public void SetGraphic(int index, Game.CubeType type)
    {
        CubeGraphicType colorType = CubeGraphicType.BLUE;
        switch (type)
        {
            case Game.CubeType.BLUE:
                colorType = CubeGraphicType.BLUE;
                break;
            case Game.CubeType.GREEN:
                colorType = CubeGraphicType.GREEN;
                break;
            case Game.CubeType.RED:
                colorType = CubeGraphicType.RED;
                break;
        }
        cubeGraphics[index].SetColor(colorType);
    }

    public void SetGraphicNone(int index)
    {
        cubeGraphics[index].SetColor(CubeGraphicType.NONE);
    }

    public Vector3 GetCubePosition(int index)
    {
        return cubeGraphics[index].transform.position;
    }

    public Vector3 GetBottleUpPosition()
    {
        return bottleUpTransform.position;
    }
}
