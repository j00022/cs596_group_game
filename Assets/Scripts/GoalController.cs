using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    GameObject spawn;
    SpawnController spawn_script;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawn");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            spawn.GetComponent<SpawnController>().goal_reached = true;
            Destroy(gameObject);
        }
    }
}
