using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    [SerializeField] private FpsMovement player;

    private MazeConstructor generator;
    private bool goalReached;

    void Start()
    {
        generator = GetComponent<MazeConstructor>();
        StartNewGame();
    }
    private void StartNewGame()
    {
        StartNewMaze();
    }
    public void StartNewMaze()
    {
        generator.GenerateNewMaze(15, 15, OnStartTrigger, OnGoalTrigger);
        Debug.Log("triggersgenerated");

        float x = generator.startCol * generator.hallWidth;
        float y = 1;
        float z = generator.startRow * generator.hallWidth;
        player.transform.position = new Vector3(x, y, z);

        goalReached = false;
        player.enabled = true;
    }

    void Update()
    {
        if (!player.enabled)
        {
            return;
        }
    }

    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("goaltriggered");
        goalReached = true;
        Destroy(trigger);
    }

    void OnStartTrigger(GameObject trigger, GameObject other)   //this method does not do anything
    {
        Debug.Log("starttriggered");
        if (goalReached)
        {
            player.enabled = false;
            SceneManager.LoadScene("Level 1");
        }
    }
}