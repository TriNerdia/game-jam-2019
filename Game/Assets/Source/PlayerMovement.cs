using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;

    private Vector3 Direction;
    private Vector3 Rotation;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;

    public void Update()
    {
        Direction = Vector3.zero;
        Rotation = Vector3.zero;

        if (Input.GetKey(moveUp))
        {
            Rotation.y += 1;
            Direction = Vector3.up;
        }

        if (Input.GetKey(moveDown))
        {
            Rotation.y -= 1;
            Direction = Vector3.up;
        }

        if (Input.GetKey(moveRight))
        {
            Rotation.x += 1;
            Direction = Vector3.up;
        }

        if (Input.GetKey(moveLeft))
        {
            Rotation.x -= 1;
            Direction = Vector3.up;
        }

        if (Rotation != Vector3.zero)
        {
            Quaternion rotate = Quaternion.LookRotation(Vector3.forward, Rotation);

            // This is to prevent the rotation along these two axis. The game
            // has 3d objects within a 2d scene.
            rotate.x = 0;
            rotate.y = 0;
            transform.rotation = rotate;
        }

        transform.Translate(Direction * MoveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // detects collision with an object named Pickup, duplicate script to add different kinds
        if (collision.gameObject.tag == "power up")
        {
            //Remove the pickup from game
            Destroy(collision.gameObject);
            //increase health or add other affect to player
            //Health += 10;
        }
    }
}

