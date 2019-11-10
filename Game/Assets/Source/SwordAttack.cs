using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Animation swordAnimation;
    public int swordDamage = 10;

    private void Start()
    {
        swordAnimation = GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || (collision.gameObject.tag == "Portal" && transform.parent.gameObject.tag != "Enemy"))
        {
            collision.gameObject.GetComponent<ObjectHealth>().TakeDamage(swordDamage);
        }
    }

    public void PlayAttackAnimation()
    {
        if (transform.parent.tag == "Player") 
            swordAnimation.Play("Attack");
        else
            swordAnimation.Play("Attack 1");
    }
}
