﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalkerMovement : MonoBehaviour
{
    [SerializeField] GameObject newBody;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 2f;
        if (!(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 10)))
        {
            Instantiate(newBody, new Vector3((Random.Range(1, 19) * 3.75f), 1, (Random.Range(1, 19) * 3.75f)), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            Destroy(gameObject);
        }
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Level 1");
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        }
    }
}
