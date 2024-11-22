using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1f : 
                      Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f;

        float moveY = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1f : 
                      Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;

        Vector2 movement = new Vector2(moveX, moveY).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
