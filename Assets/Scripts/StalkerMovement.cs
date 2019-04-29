using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerMovement : MonoBehaviour
{
    [SerializeField] GameObject newBody;

    private void Start()
    {
        
    }
    private void Update()
    {

        if (gameObject.transform.position.y < 0)
        {

            Instantiate(newBody, new Vector3((Random.Range(1, 19) * 3.75f), 1, (Random.Range(1, 19) * 3.75f)), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            Destroy(gameObject);
        }
    }
}
