using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public Vector2 screenSpace;
    public static GameObject[] total;
    public float xAxis;
    public float yAxis;
    public float distance = 1f; 
    public GameObject[] inputThing;
    private int maxObj = 15;
    private int obj = 0; 

    void Start()
    {
        total = new GameObject[maxObj];
    }

    void Update()
    {
       
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
    
        if (obj >= maxObj)
        {
            RemoveFirstRow();
        }

   
        Vector2 newPosition = new Vector2(xAxis, -obj * distance);

        // Instantiate the new object
        GameObject g = Instantiate(prefab, newPosition, Quaternion.identity);
         total[obj] = g;

   
        obj++;
    }

    private void RemoveFirstRow()
    {
 
        if (total[0] != null)
        {
            Destroy(total[0]);
        }

    
        for (int i = 1; i < maxObj; i++)
        {
            total[i - 1] = total[i];
        }


        total[maxObj - 1] = null;

        for (int i = 0; i < maxObj; i++)
        {
            if (total[i] != null)
            {
                total[i].transform.position = new Vector2(xAxis, -i * distance);
            }
        }


        obj--;
    }

    private void RemoveAllRows()
    {

        for (int i = 0; i < maxObj; i++)
        {
            if (total[i] != null)
            {
                Destroy(total[i]);
            }
        }

        total = new GameObject[maxObj];
        obj = 0;
    }
}
