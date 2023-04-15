using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCAction : CharacterAction
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject healEffect;

    IEnumerator coolTimeCounter;
    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    public float coolTime = 5f;
    public float filledTime;
    bool isHealEnable;
    public int defaultHealValue = 20;
    public float skillRange;
    public float distance;

    public int curNPCHp;
    public int maxNPCHp;
    public bool isBorder;
    public bool isAttacked;

    public Image npcHpImg;


    void Start()
    {
        coolTimeCounter = cntCoolTime();
        isHealEnable = true;
        //StartCoroutine(dotHealing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hpBarRefresh(int hp)
    {

        npcHpImg.fillAmount = (float)hp / maxNPCHp;
        

    }


    public bool Healing(int value)
    {
        //Debug.Log("Èú ½Ãµµ");
        //bool abcd = enableRange();
        //Debug.Log(abcd);
        if (isHealEnable && enableRange())
        {
            //Debug.Log("heal");
            player.GetComponent<PlayerAction>().getHealing(value);
            healEffect.SetActive(true);
            StartCoroutine(effectDealy());
            StartCoroutine(cntCoolTime());
            return true;
      
        }
        return false;
    }

    bool enableRange()
    {
        distance = Vector2.Distance(transform.localPosition, player.GetComponent<Transform>().localPosition);
        if (distance > skillRange) return false;
        return true;
    }

    //IEnumerator dotHealing()
    //{
    //    while (true)
    //    {
    //        Healing(defaultHealValue);
    //        yield return new WaitForSeconds(1.0f);
    //    }
    //}

    IEnumerator cntCoolTime()
    {
        filledTime = 0f;
        isHealEnable = false;

        while (true)
        {
            if(filledTime < coolTime)
            {
                yield return fixedUpdate;
                filledTime += Time.deltaTime;
            }
            else
            {
                isHealEnable = true;
                yield break;
            }
        }
    }

    IEnumerator effectDealy()
    {
        yield return new WaitForSeconds(1.0f);
        healEffect.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            isBorder = true;
        }
    }
    

    public override void TakeDamage(int damage)
    {
        //if (isDead || damage <= 0) return;

        curNPCHp -= damage;
        isAttacked = true;
        //Debug.Log(curNPCHp);
        //anim.SetTrigger("doHit");

        if (curNPCHp <= 0)
        {
            curNPCHp = 0;
            OnDead();
        }

        hpBarRefresh(curNPCHp);
    }

    public override void OnDead()
    {
        if (isDead) return;
        isDead = true;
        //anim.SetTrigger("doDie");
        //StopCoroutine(dotdamage());
    }
}
