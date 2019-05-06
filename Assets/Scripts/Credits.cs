using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            lt.color = Color.green; //testing escape key working. REMOVE WHEN PUTTING IN RETURN TO MAIN MENU
            //INSERT RETURN TO MAIN MENU CODE HERE
        }
    }
}
