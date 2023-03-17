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
    public float skillRange;
    //public float speed;
    public Transform player;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(bossMove());
    }

    void Update()
    {

    }

    IEnumerator bossMove()
    {
        while (true)
        {
            Vector3 velocity = Vector3.zero;
            float distance = Vector2.Distance(transform.position, player.position);
            yield return new WaitForSeconds(1.5f);
            while (distance > skillRange)
            {
                transform.position
                    = Vector3.SmoothDamp(transform.position, player.position, ref velocity, 1.8f);

                yield return null;
            }
            yield return null;
        }
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
