using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerSpawn : MonoBehaviour
{
    //[SerializeField] private GameObject Bigboy;
    [SerializeField] private GameObject Littleboy;

    private Vector3 start;

    void Awake()
    {

    }

    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            Vector3 enemyPlacement = new Vector3((Random.Range(1, 19) * 3.75f), 1, (Random.Range(1, 19) * 3.75f));
            Instantiate(Littleboy, enemyPlacement, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        }
    }
    void Update()
    {

    }
}
