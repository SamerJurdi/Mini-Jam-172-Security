using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public Vector2 screenSpace;
    public static GameObject[] total;
    public float xAxis;
    public float yAxis;
    public float distance = 1f; // Space between rows
    public GameObject[] inputThing;
    private int maxObj = 15; // Max number of objects
    private int obj = 0; // Current number of objects

    void Start()
    {
        total = new GameObject[maxObj];
    }

    void Update()
    {
        // Add a row with "U" or "Y" input
        if (Input.GetKeyDown(KeyCode.U))
        {
            AddRow(inputThing[0]);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            AddRow(inputThing[1]);
        }
    }

    private void AddRow(GameObject prefab)
    {
        // If max objects are reached, remove the first row
        if (obj >= maxObj)
        {
            RemoveFirstRow();
        }

        // Calculate the new position for the object
        Vector2 newPosition = new Vector2(xAxis, -obj * distance);

        // Instantiate the new object
        GameObject g = Instantiate(prefab, newPosition, Quaternion.identity);

        // Add the new object to the array
        total[obj] = g;

        // Increment the object counter
        obj++;
    }

    private void RemoveFirstRow()
    {
        // Destroy the first object
        if (total[0] != null)
        {
            Destroy(total[0]);
        }

        // Shift all objects up in the array
        for (int i = 1; i < maxObj; i++)
        {
            total[i - 1] = total[i];
        }

        // Clear the last slot in the array
        total[maxObj - 1] = null;

        // Adjust the position of all objects in the array
        for (int i = 0; i < maxObj; i++)
        {
            if (total[i] != null)
            {
                total[i].transform.position = new Vector2(xAxis, -i * distance);
            }
        }

        // Decrement the object counter
        obj--;
    }

    private void RemoveAllRows()
    {
        // Destroy all objects in the array
        for (int i = 0; i < maxObj; i++)
        {
            if (total[i] != null)
            {
                Destroy(total[i]);
            }
        }

        // Reset everything
        total = new GameObject[maxObj];
        obj = 0;
    }
}
