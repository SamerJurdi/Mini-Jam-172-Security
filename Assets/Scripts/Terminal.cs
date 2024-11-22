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

    private bool isDragging = false;
    private Vector3 offset;

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

  
        if (isDragging)
        {
            DragTerminal();
        }
    }

    private void AddRow(GameObject prefab)
    {
        
        if (obj >= maxObj)
        {
            RemoveFirstRow();
        }

      
        Vector2 newPosition = new Vector2(transform.position.x + xAxis, transform.position.y + (-obj * distance) - 1.6f);

     
        GameObject g = Instantiate(prefab, newPosition, Quaternion.identity);
        g.transform.parent = transform; 

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
                total[i].transform.position = new Vector2(transform.position.x + xAxis, transform.position.y + (-i * distance) - 1.6f);
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

  
    private void OnMouseDown()
    {
        isDragging = true;

       
        offset = transform.position - GetMouseWorldPosition();
    }

   
    private void OnMouseUp()
    {
        isDragging = false;
    }

   
    private void DragTerminal()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        transform.position = mousePosition + offset;

   
        for (int i = 0; i < obj; i++)
        {
            if (total[i] != null)
            {
                total[i].transform.position = new Vector2(transform.position.x + xAxis, transform.position.y + (-i * distance) - 1.6f);
            }
        }
    }

  
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z; 
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}
