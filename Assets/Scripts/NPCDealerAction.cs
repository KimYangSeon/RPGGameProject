using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDealerAction : CharacterAction
{
    [SerializeField]
    GameObject player;

    

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

    bool isAttackEnable = true;
    GameObject _healEffect;

    public float atk;
    //public Rigidbody2D rigid;
    //Vector3 dirVec;


    void Start()
    {
        coolTimeCounter = cntCoolTime();
        isHealEnable = true;
        _healEffect = transform.GetChild(0).transform.GetChild(0).gameObject;
        //rigid = GetComponent<Rigidbody2D>();
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

    public void Attack(GameObject scanObject)
    {
        //StartCoroutine(IconEffect(attackIcon));
        //anim.SetTrigger("doAttack");
        
        if (isDead || !isAttackEnable) return; // Á×¾ú´ÂÁö & ÄðÅ¸ÀÓ Ã¼Å©
        if (scanObject != null && scanObject.tag == "Boss")
        {
            StartCoroutine(attackCoolTime());

            //if (!scanObject) return;
            scanObject.GetComponent<BossAction>().getDamage(atk);
           // Debug.Log("doAttack");
        }


    }


    IEnumerator attackCoolTime()
    {
        float t = 0f;
        isAttackEnable = false;
        //coolDownImg.fillAmount = 1;

        while (true)
        {
            if (t < 1)
            {
                yield return fixedUpdate;
                t += Time.deltaTime;
                //coolDownImg.fillAmount = (1 - t) / 1;
            }
            else
            {
                isAttackEnable = true;
                //coolDownImg.fillAmount = 0;
                yield break;
            }
        }
    }

    public void getHealing(int value)
    {
        if (isDead) return;
        //isOverHealed = false;
        //Debug.Log(curPlayerHp);
        
        curNPCHp += value;
        if (curNPCHp > maxNPCHp)
        {
            curNPCHp = maxNPCHp;
            //isOverHealed = true;
        }
        hpBarRefresh(curNPCHp);

        _healEffect.SetActive(true);
        StartCoroutine(effectDealy());
    }

    IEnumerator effectDealy()
    {
        yield return new WaitForSeconds(1.0f);
        _healEffect.SetActive(false);
    }
}
