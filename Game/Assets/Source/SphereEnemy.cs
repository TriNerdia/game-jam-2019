using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemy : MonoBehaviour
{
    public SwordAttack sword;

    public KeyCode attackKey = KeyCode.L;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(attackKey))
        {
            sword.PlayAttackAnimation();
        }
    }
}
