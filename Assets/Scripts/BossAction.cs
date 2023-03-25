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
    public GameObject alert;
    bool isAttacking = false;
    Vector3 velocity;

    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //StartCoroutine(bossMove());
        velocity = Vector3.zero;
        Invoke("choosePattern", 5f);

    }

    void Update()
    {
        if (!isAttacking)
        {
            transform.position
                    = Vector3.SmoothDamp(transform.position, player.position, ref velocity, 1.8f);
        }
        
    }

    void choosePattern()
    {
        bossPattern(0);
    }

    void bossPattern(int patternIdx)
    {
        switch (patternIdx)
        {
            case 0:
                StartCoroutine(defaultAttack());
                break;
        }
    }

    IEnumerator defaultAttack()
    {
        isAttacking = true;
        alert.SetActive(true);
        yield return StartCoroutine(delay(3));
        Debug.Log("공격");
        alert.SetActive(false);
        isAttacking = false;

        Invoke("choosePattern", 10);
    }

    IEnumerator delay(float delayTime)
    {
        float cur = 0;
        while (true)
        {
            if (cur < delayTime)
            {
                yield return fixedUpdate;
                cur += Time.deltaTime;
            }
            else
            {
                yield break;
            }
        }
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
                if (isAttacking) continue;

                transform.position
                    = Vector3.SmoothDamp(transform.position, player.position, ref velocity, 1.8f);
                distance = Vector2.Distance(transform.position, player.position);
                yield return null;
            }
            //Debug.Log("이동 끝");
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
