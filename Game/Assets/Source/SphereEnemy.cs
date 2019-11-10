using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemy : MonoBehaviour
{
    public SwordAttack sword;

    public string TargetTag = "Player";
    public bool IsAttacking { get; internal set; }

    //public KeyCode attackKey = KeyCode.L;

    void Start()
    {
        IsAttacking = false;
    }

    void Update()
    {
        if (IsAttacking)
        {
            sword.PlayAttackAnimation();
        }
    }

    // Look at the second sphere collider that has the IsTrigger button set
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TargetTag))
        {
            IsAttacking = true;
            transform.parent.LookAt(collider.transform);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(TargetTag))
        {
            IsAttacking = false;
        }   
    }
}
