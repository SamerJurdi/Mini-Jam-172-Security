using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public Vector2 screenSpace;
    public float xAxis;
    public float yAxis;
    public float distance;
    public GameObject[] inputThing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenSpace = new Vector2(xAxis, yAxis);

        if (Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(inputThing[0], screenSpace, Quaternion.identity);
            screenSpace = new Vector2(xAxis, yAxis);
            yAxis -= distance;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Instantiate(inputThing[1], screenSpace, Quaternion.identity);
            screenSpace = new Vector2(xAxis, yAxis);
            yAxis -= distance;
        }
    }
}
