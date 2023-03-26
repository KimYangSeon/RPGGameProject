using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //float distance = Vector2.Distance(transform.position, player.GetComponent<Transform>().position);
        //Debug.Log(distance);
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
        //Debug.Log("ÄðÅ¸ÀÓ ½ÃÀÛ");

        while (true)
        {
            if(filledTime < coolTime)
            {
                yield return fixedUpdate;
                filledTime += Time.deltaTime;
                //Debug.Log(filledTime);
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
        //Debug.Log(curNPCHp);
        //anim.SetTrigger("doHit");

        if (curNPCHp <= 0)
        {
            curNPCHp = 0;
            //NPCDie();
        }

        //hpBarRefresh(curNPCHp);
    }
}
