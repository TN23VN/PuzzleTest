using System.Collections.Generic;
using System.Text;
using UnityEngine;

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
