using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public Vector2 screenSpace;
    public float xAxis;
    public float yAxis;
    public float distance;
    public GameObject text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenSpace = new Vector2(xAxis, yAxis);

        if (Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(text, screenSpace, Quaternion.identity);
            screenSpace = new Vector2(xAxis, yAxis);
            yAxis -= distance;
        }
    }
}
