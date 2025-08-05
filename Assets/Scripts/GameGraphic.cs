using System.Collections.Generic;
using UnityEngine;

public class GameGraphic : MonoBehaviour
{
    // trang thai mac dinh : -1
    // trang thai co cube : bottleIndex
    public int selectedBottleIndex = -1;

    private Game game;

    public List<BottleGraphic> bottleGraphics;

    private void Start()
    {
        game = FindObjectOfType<Game>();
        selectedBottleIndex = -1;
    }

    public void RefreshBottleGraphic(List<Game.Bottle> bottles)
    {
        for (int i = 0; i < bottles.Count; i++)
        {
            Game.Bottle gb = bottles[i];
            BottleGraphic bottleGraphic = bottleGraphics[i];

            List<Game.CubeType> cubeTypes = new List<Game.CubeType>();

            foreach(var cube in gb.cubes)
            {
                cubeTypes.Add(cube.type);
            }

            bottleGraphic.SetGraphic(cubeTypes.ToArray());
        }
    }

    public void OnClickBottle(int bottleIndex)
    {
        Debug.Log("Click : "+bottleIndex);

        if(selectedBottleIndex == -1)
        {
            selectedBottleIndex = bottleIndex;
        }
        else
        {
            game.SwitchCube(selectedBottleIndex,bottleIndex);
            selectedBottleIndex = -1;
        }
    }
}
