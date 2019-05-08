using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{

    public Light lt;
    public GameObject credits;


    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, 4f) / 5f;
        lt.color = Color.Lerp(Color.white, Color.gray, t);

        credits.transform.position += Vector3.up * Time.deltaTime * .2f;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
