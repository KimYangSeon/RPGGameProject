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
    public float speed;
    public GameObject attackPrefab;

    public GameObject alert;
    bool isAttacking = false;
    Vector3 velocity;
    Transform playerTransform;
    PlayerAction playerAction;
    NPCAction NPCAction;
    //CharacterAction characterAction;
    public int BossNum;
    public GameObject boss1MiniPrefab;

    public GameObject player;
    public GameObject npc;

    public GameObject DonutAlert;

    public bool isDead = false;

    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerTransform = player.GetComponent<Transform>();
        playerAction = player.GetComponent<PlayerAction>();
        NPCAction = npc.GetComponent<NPCAction>();
        //characterAction = player.GetComponent<CharacterAction>();
    }

    private void Start()
    {
        velocity = Vector3.zero;
        Invoke("choosePattern", 5f);

    }

    IEnumerator BossWait()
    {
        yield return new WaitForSeconds(3.0f);
    }

    void Update()
    {
        if (isDead) return;

        if (!isAttacking)
        {
            transform.position
            = Vector3.SmoothDamp(transform.position, playerTransform.position, ref velocity, 1.8f);
            //transform.position += ((player.transform.position) - (transform.position)).normalized * speed * Time.deltaTime;
        }
    }

    /*
     보스 2 패턴: 
    장판 -> 바로 도넛 장판
    도넛 장판 -> 바로 장판
    피 50% 이하 되면 플레이어 추적 3번

     */

    public void choosePattern()
    {
        if (isDead) return;

        if (BossNum == 1)
        {
            bossPattern(0);
        }
        else if (BossNum == 2)
        {
            if(curHp / maxHp <= 0.5f)
            {
                int idx = Random.Range(0, 2);
                if(idx==0)
                    bossPattern(2); // 추적
                else
                    bossPattern(1); // 장판 2개 
            }
            else
            {
                bossPattern(1); // 장판 2개 
            }
            
        }
        else if (BossNum == 3)
        {
            bossPattern(2);
        }
        else if (BossNum == 4)
        {
            bossPattern(3);
        }

    }

    public void bossPattern(int patternIdx)
    {
        switch (patternIdx)
        {
            case 0:
                StartCoroutine(DefaultAttack(3, false));
                break;
            case 1:
                StartCoroutine(DefaultAndDonutAttack());
                break;
            case 2:
                StartCoroutine(ContinuousAttack());
                //summon();
                break;
            case 3:
                //StartCoroutine(DonutAttack());
                break;
        }
    }


    public IEnumerator DefaultAttack(float delayTime, bool isContinuous)
    {
        isAttacking = true;
        alert.SetActive(true);
        yield return StartCoroutine(Delay(delayTime));

        anim.SetTrigger("isJumping");
        Collider2D[] hitColliders = alert.GetComponent<BossAttackCheck>().checkRange();

        foreach (Collider2D collider in hitColliders)
        {
            if (collider == null) continue;
            GameObject objectHit = collider.gameObject;
            CharacterAction ch =  objectHit.GetComponent<CharacterAction>();
            if(ch != null)
                ch.TakeDamage(5);
        }

        alert.SetActive(false);
        isAttacking = false;

        if(!isContinuous) 
            Invoke("choosePattern", 6);
    }

    IEnumerator ContinuousAttack()
    {
        StartCoroutine(PlayerTrackingAttack(2, true));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayerTrackingAttack(2, true));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayerTrackingAttack(2, false));
    }

    public IEnumerator PlayerTrackingAttack(float delayTime, bool isContinuous)
    {

            GameObject attackObj = Instantiate(attackPrefab, player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(delayTime);
            // 나중에 BossAttackCheck를 AttackCheck로 변경하기
            Collider2D[] hitColliders = attackObj.GetComponent<BossAttackCheck>().checkRange();

            foreach (Collider2D collider in hitColliders)
            {
                if (collider == null) continue;
                GameObject objectHit = collider.gameObject;
                CharacterAction ch = objectHit.GetComponent<CharacterAction>();
                ch.TakeDamage(5);
            }

            Destroy(attackObj);
        

        if(!isContinuous)
            Invoke("choosePattern", 6);
    }



    IEnumerator Delay(float delayTime)
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


    public void getDamage(float damage)
    {
        if (isDead) return;

        curHp -= damage;
        anim.SetTrigger("isDamaged");

        if (curHp < 0)
        {
            curHp = 0;
            OnDead();
        }

        //Debug.Log(curHp);
        hpBarRefresh(curHp);
    }

    void hpBarRefresh(float hp)
    {
        hpImg.fillAmount = hp / maxHp;
    }

    public IEnumerator DefaultAndDonutAttack()
    {
        // 기본 공격
        yield return StartCoroutine(DefaultAttack(3, true));

        //0.5초 딜레이
        yield return new WaitForSeconds(0.5f);
        // 도넛 공격
        yield return StartCoroutine(DonutAttack(1, false));

    }

    public void Summon()
    {
        GameObject[] boss1_mini_array = new GameObject[6];
        boss1_mini_array[0] = Instantiate(boss1MiniPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        boss1_mini_array[1] = Instantiate(boss1MiniPrefab, new Vector3(-3, -2.5f, 0), Quaternion.identity);
        boss1_mini_array[2] = Instantiate(boss1MiniPrefab, new Vector3(3, -2.5f, 0), Quaternion.identity);
        boss1_mini_array[3] = Instantiate(boss1MiniPrefab, new Vector3(5, 0, 0), Quaternion.identity);
        boss1_mini_array[4] = Instantiate(boss1MiniPrefab, new Vector3(3, 2.5f, 0), Quaternion.identity);
        boss1_mini_array[5] = Instantiate(boss1MiniPrefab, new Vector3(-3, 2.5f, 0), Quaternion.identity);
        foreach (GameObject bossMini in boss1_mini_array)
        {
            bossMini.GetComponent<Boss1MiniAction>().bossPattern(0);
        }

        Invoke("choosePattern", 5);
    }

    public IEnumerator DonutAttack(float delayTime, bool isContinuous)
    {
        isAttacking = true;
        DonutAlert.SetActive(true);
        yield return StartCoroutine(Delay(delayTime));

        anim.SetTrigger("isJumping");
        Collider2D[] hitColliders = DonutAlert.GetComponent<BossAttackCheck>().checkRange();

        foreach (Collider2D collider in hitColliders)
        {
            if (collider == null) continue;
            GameObject objectHit = collider.gameObject;
            CharacterAction ch = objectHit.GetComponent<CharacterAction>();
            if (ch != null)
                ch.TakeDamage(5);
        }

        DonutAlert.SetActive(false);
        isAttacking = false;

        if (!isContinuous)
            Invoke("choosePattern", 6);
    }

    void OnDead()
    {
        isDead = true;
    }
}
