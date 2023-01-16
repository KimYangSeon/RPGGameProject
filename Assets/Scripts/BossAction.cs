using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAction : MonoBehaviour
{
    public float curHp;
    public float maxHp;
    Animator anim;
    public Image hpImg;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void getDamage(float damage)
    {
        curHp -= damage;
        anim.SetTrigger("isDamaged");

        if (curHp < 0) curHp = 0;

        Debug.Log(curHp);
        hpBarRefresh(curHp);
    }

    void hpBarRefresh(float hp)
    {
        hpImg.fillAmount = hp / maxHp;
    }
}
