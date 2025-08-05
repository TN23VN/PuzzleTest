using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

public class Game : MonoBehaviour
{
    public GameGraphic graphic;
    public List<Bottle> bottles;

    private IEnumerator Start()
    {
        bottles = new List<Bottle>();

        bottles.Add(new Bottle
        {
            cubes = new List<Cube> { new Cube { type = CubeType.BLUE }, new Cube { type = CubeType.RED }, new Cube { type = CubeType.RED } }
        });

        bottles.Add(new Bottle
        {
            cubes = new List<Cube> { new Cube { type = CubeType.BLUE }, new Cube { type = CubeType.BLUE }, new Cube { type = CubeType.RED } }
        });

        bottles.Add(new Bottle
        {
            cubes = new List<Cube> { new Cube { type = CubeType.RED }, new Cube { type = CubeType.BLUE } }
        });

        bottles.Add(new Bottle
        {
            cubes = new List<Cube> { }
        });

        graphic.RefreshBottleGraphic(bottles);
        yield return new WaitForSeconds(2f);

        /*/
        PrintBottles();
        SwitchCube(bottles[0],bottles[1]);
        graphic.RefreshBottleGraphic(bottles);
        /*/
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

                if(bottle2Cubes.Count == 4)
                {
                    break;
                }
            }
            else {break;}
        }
        
    }

    public void SwitchCube(int bottleIndex1, int bottleIndex2)
    {
        Bottle b1 = bottles[bottleIndex1];
        Bottle b2 = bottles[bottleIndex2];

        SwitchCube(b1,b2);

        graphic.RefreshBottleGraphic(bottles);

    }

    public List<SwitchCubeCommand> CheckSwitchCube(int bottleIndex1, int bottleIndex2)
    {
        List<SwitchCubeCommand > commands = new List<SwitchCubeCommand>();
        
        Bottle bottle1 = bottles[bottleIndex1];
        Bottle bottle2 = bottles[bottleIndex2];

        List<Cube> bottle1Cubes = bottle1.cubes;
        List<Cube> bottle2Cubes = bottle2.cubes;

        if (bottle1Cubes.Count == 0) { return commands; }
        if (bottle2Cubes.Count == 4) { return commands; }

        int index = bottle1Cubes.Count - 1;
        Cube c = bottle1Cubes[index];

        var type = c.type;
        if (bottle2Cubes.Count > 0 && bottle2Cubes[bottle2Cubes.Count - 1].type != type)
        {
            return commands;
        }

        int targetIndex = bottle2Cubes.Count;

        for (int i = index; i >= 0; i--)
        {
            Cube cube = bottle1Cubes[i];
            if (cube.type == type)
            {
                int fromCubeIndex = i;
                int toCubeIndex = targetIndex;
                int fromBottleIndex = bottleIndex1;
                int toBottleIndex = bottleIndex2;

                commands.Add(new SwitchCubeCommand
                {
                    type = type,
                    fromCubeIndex = fromCubeIndex,
                    toCubeIndex = toCubeIndex,
                    fromBottleIndex = fromBottleIndex,
                    toBottleIndex = toBottleIndex
                });
                /*/
                bottle1Cubes.RemoveAt(i);
                bottle2Cubes.Add(cube);
                /*/

                targetIndex++;
                if (targetIndex == 4)
                {
                    break;
                }
            }
            else { break; }
        }

        return commands;
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

    public class SwitchCubeCommand
    {
        public CubeType type;
        public int fromBottleIndex;
        public int fromCubeIndex;

        public int toBottleIndex;
        public int toCubeIndex;
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
