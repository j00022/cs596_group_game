using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MazeConstructor : MonoBehaviour
{
    private MazeDataGenerator dataGenerator;
    private MazeMeshGenerator meshGenerator;

    public bool showDebug;

    [SerializeField] private Material floorMat;
    [SerializeField] private Material wallMat;
    [SerializeField] public GameObject startPrefab;
    [SerializeField] public GameObject treasurePrefab;

    public float hallWidth
    {
        get; private set;
    }
    public float hallHeight
    {
        get; private set;
    }
    public int startRow
    {
        get; private set;
    }
    public int startCol
    {
        get; private set;
    }
    public int goalRow
    {
        get; private set;
    }
    public int goalCol
    {
        get; private set;
    }
    public int[,] data
    {
        get; private set;
    }

    void Awake()
    {
        meshGenerator = new MazeMeshGenerator();
        dataGenerator = new MazeDataGenerator();
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols,
     TriggerEventHandler startCallback = null, TriggerEventHandler goalCallback = null)
    {

        DisposeOldMaze();

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);

        FindStartPosition();
        FindGoalPosition();

        hallWidth = meshGenerator.width;
        hallHeight = meshGenerator.height;

        DisplayMaze();

        PlaceStartTrigger(startCallback);
        PlaceGoalTrigger(goalCallback);
    }

    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = meshGenerator.FromData(data);

        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] { floorMat, wallMat };
    }

    public void DisposeOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }

    private void FindStartPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    startRow = i;
                    startCol = j;
                    return;
                }
            }
        }
    }

    private void FindGoalPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        // loop top to bottom, right to left
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = cMax; j >= 0; j--)
            {
                if (maze[i, j] == 0)
                {
                    goalRow = i;
                    goalCol = j;
                    return;
                }
            }
        }
    }
    private void PlaceStartTrigger(TriggerEventHandler callback)
    {
        GameObject go = startPrefab;
        var startPostion = go.transform.position;
        startPostion = new Vector3(startCol * hallWidth, .15f, startRow * hallWidth);
        Quaternion startRotation = Quaternion.identity;
        

        go.name = "Start Trigger";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        Debug.Log("starttriggerassigned");
        Instantiate(go, startPostion, startRotation);

        TriggerEventRouter tc = go.GetComponent<TriggerEventRouter>();
        tc.callback = callback;

    }

    private void PlaceGoalTrigger(TriggerEventHandler callback)
    {
        GameObject go = treasurePrefab;
        var treasurePostion = go.transform.position;
        treasurePostion = new Vector3(goalCol * hallWidth, .2f, goalRow * hallWidth);
        Quaternion treasureRotation = Quaternion.identity;
        

        go.name = "Treasure";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        Debug.Log("goaltriggerassigned");
        Instantiate(go, treasurePostion, treasureRotation);

        TriggerEventRouter tc = go.GetComponent<TriggerEventRouter>();
        tc.callback = callback;


    }
}