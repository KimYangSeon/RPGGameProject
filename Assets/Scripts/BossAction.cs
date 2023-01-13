using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{
    public float hp;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void getDamage(float damage)
    {
        hp -= damage;
        anim.SetTrigger("isDamaged");

        if (hp < 0) hp = 0;
        Debug.Log(hp);
    }
}
