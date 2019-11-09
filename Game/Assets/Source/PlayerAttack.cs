using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public SwordAttack sword;

    public KeyCode attackKey = KeyCode.Space;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(attackKey))
        {
            sword.PlayAttackAnimation();
        }
    }
}
