using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;

    public Vector3 Direction;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;

    public void Update()
    {
        Direction = Vector3.zero;

        if (Input.GetKey(moveUp))
        {
            Direction.y += 1;
        }

        if (Input.GetKey(moveDown))
        {
            Direction.y -= 1;
        }

        if (Input.GetKey(moveRight))
        {
            Direction.x += 1;
        }

        if (Input.GetKey(moveLeft))
        {
            Direction.x -= 1;
        }

        if (Direction.magnitude > 1)
        {
            Direction.Normalize();
        }

        transform.Translate(Direction * MoveSpeed * Time.deltaTime);
    }
}
