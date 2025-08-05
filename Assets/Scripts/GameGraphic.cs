using System.Collections.Generic;
using UnityEngine;

public class GameGraphic : MonoBehaviour
{
    public List<BottleGraphic> bottleGraphics;

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
}
