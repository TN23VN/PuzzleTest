using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

public class Game : MonoBehaviour
{
    public List<Bottle> bottles;

    private void Start()
    {
        bottles = new List<Bottle>();

        bottles.Add(new Bottle
        {
            cubes = new List<Cube> { new Cube { type = CubeType.BLUE } }
        });

        PrintBottles();
    }

    public void PrintBottles()
    {
        StringBuilder sb = new StringBuilder();

        for(int i = 0; i < bottles.Count; i++)
        {
            Bottle b = bottles[i];
            sb.Append("Bottle "+(i+1) + ":");
            foreach(Cube cube in b.cubes)
            {
                sb.Append(" "+ cube.type);
                sb.Append(",");
            }
            sb.Clear();
        }
    }

    public void SwitchCube(Bottle bottle1, Bottle bottle2)
    {
        List<Cube> bottle1Cubes = bottle1.cubes;
        List<Cube> bottle2Cubes = bottle2.cubes;

        if(bottle1Cubes.Count == 0) { return; }
        if(bottle2Cubes.Count == 4) { return; }

        int index = bottle1Cubes.Count - 1;
        Cube c = bottle1Cubes[index];

        var type = c.type;
        if (bottle2Cubes.Count > 0 && bottle2Cubes[bottle2Cubes.Count-1].type != type)
        {
            return;
        }

        for (int i = index; i >= 0; i--) 
        {
            Cube cube = bottle1Cubes[i];
            if (cube.type == type)
            {
                bottle1Cubes.RemoveAt(i);
                bottle2Cubes.Add(cube);
            }
            else {break;}
        }
        
    }

    public bool CheckWinCondition()
    {
        bool winFlag = true;

        foreach (Bottle bottle in bottles)
        {
            if (bottle.cubes.Count == 0)
                continue;

            if (bottle.cubes.Count < 4)
            {
                winFlag = false; break;
            }
            bool sameTypeFlag = true;
            CubeType type = bottle.cubes[0].type;
            foreach (Cube cube in bottle.cubes)
            {
                if (cube.type != type)
                {
                    sameTypeFlag = false; break;
                }
            }
            if (!sameTypeFlag)
            {
                winFlag = false; break;
            }
        }
        return winFlag;
    }

    public class Bottle
    {
        public List<Cube> cubes = new List<Cube>();
    }

    public class Cube
    {
        public CubeType type;
    }

    public enum CubeType
    {
        RED,
        GREEN, 
        BLUE,
        YELLOW,
        ORANGE,
    }
}
