using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameGraphic : MonoBehaviour
{
    // trang thai mac dinh : -1
    // trang thai co cube : bottleIndex
    public int selectedBottleIndex = -1;

    private Game game;

    public List<BottleGraphic> bottleGraphics;

    public CubeGraphic prefabCubeGraphic;

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
        if(isSwitchingCube)
            return;
        
        if (selectedBottleIndex == -1)
        {
            selectedBottleIndex = bottleIndex;
        }
        else
        {
            /*/
            game.SwitchCube(selectedBottleIndex,bottleIndex);
            selectedBottleIndex = -1;
            /*/
        }
        StartCoroutine(SwitchCubeCoroutine(selectedBottleIndex, bottleIndex));
    }

    private bool isSwitchingCube = false;
    private IEnumerator SwitchCubeCoroutine(int fromBottleIndex, int toBottleIndex)
    {
        isSwitchingCube = true;
        List<Game.SwitchCubeCommand> commands = game.CheckSwitchCube(fromBottleIndex, toBottleIndex);
        if(commands.Count == 0)
        {
            Debug.Log("Cant move");
            
        }
        else
        {
            pendingCubes = commands.Count;
            foreach (Game.SwitchCubeCommand command in commands)
            {
                StartCoroutine(SwitchCube(command));
                yield return new WaitForSeconds(0.1f);
            }
            while(pendingCubes > 0);
            {
                yield return null;
            }
            game.SwitchCube(fromBottleIndex,toBottleIndex);
            
        }
        selectedBottleIndex = -1;
        isSwitchingCube = false;
    }
    private int pendingCubes = 0;
    private IEnumerator SwitchCube(Game.SwitchCubeCommand command)
    {
        bottleGraphics[command.fromBottleIndex].SetGraphicNone(command.fromCubeIndex);
        var cubeObject = Instantiate(prefabCubeGraphic, bottleGraphics[command.fromBottleIndex].GetCubePosition(command.fromCubeIndex),Quaternion.identity);
        
        cubeObject.SetColor(CubeGraphic.ConvertFromGameType(command.type));
        
        Queue<Vector3> queueMovement = new Queue<Vector3>();

        queueMovement.Enqueue(bottleGraphics[command.fromBottleIndex].GetCubePosition(command.fromCubeIndex));
        queueMovement.Enqueue(bottleGraphics[command.fromBottleIndex].GetBottleUpPosition());
        queueMovement.Enqueue(bottleGraphics[command.toBottleIndex].GetBottleUpPosition());
        queueMovement.Enqueue(bottleGraphics[command.toBottleIndex].GetCubePosition(command.toCubeIndex));

        while (queueMovement.Count > 0)
        {
            Vector3 target = queueMovement.Dequeue();

            while(Vector3.Distance(cubeObject.transform.position, target)>0.005f)
            {
                cubeObject.transform.position = Vector3.MoveTowards(cubeObject.transform.position, target, 10 * Time.deltaTime);
                yield return null;
            }
        }
        
        yield return null;
        Destroy(cubeObject.gameObject);
        bottleGraphics[command.toBottleIndex].SetGraphic(command.toCubeIndex,command.type);
        pendingCubes--;
    }
}
