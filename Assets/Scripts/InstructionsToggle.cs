using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InstructionsToggle : MonoBehaviour
{
    public Text instructions;
    private int instructionsFlag = 0;
    private GameObject minimap;

    void Start()
    {
        instructionsFlag = 0;
        minimap = GameObject.Find("Minimap Image");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (instructionsFlag == 0)
            {
                instructionsFlag = 1;
                instructions.enabled = false;

            }
            else
            {
                instructionsFlag = 0;
                instructions.enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            minimap.SetActive(!minimap.activeSelf);
        }
    }
}
