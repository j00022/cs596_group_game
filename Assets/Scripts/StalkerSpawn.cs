using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerSpawn : MonoBehaviour
{
    //[SerializeField] private GameObject Bigboy;
    public GameObject Littleboy;

    private Vector3 start;

    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            Vector3 enemyPlacement = new Vector3((Random.Range(1, 19) * 3.75f), 1, (Random.Range(1, 19) * 3.75f));
            Instantiate(Littleboy, enemyPlacement, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
    void Update()
    {
        if (!(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 10))) {
            Instantiate(Littleboy, new Vector3((Random.Range(1, 19) * 3.75f), 1, (Random.Range(1, 19) * 3.75f)), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            Destroy(gameObject);
        }
    }
}
