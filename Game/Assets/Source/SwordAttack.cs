using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Animation swordAnimation;

    private void Start()
    {
        swordAnimation = GetComponent<Animation>();
    }

    public void PlayAttackAnimation()
    {
        swordAnimation.Play("Attack");
    }
}
