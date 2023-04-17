using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Action : BossAction
{
    //public float curHp;
    //public float maxHp;
    Animator anim;
    //public Image hpImg;
    //public float skillRange;
    //public float speed;

    //public GameObject alert;
    bool isAttacking = false;
    Vector3 velocity;
    Transform playerTransform;
    PlayerAction playerAction;
    NPCAction NPCAction;
    //CharacterAction characterAction;

    //public GameObject player;
    //public GameObject npc;
    //public GameObject boss1Prefab;
    //public GameObject attackPrefab;
    GameObject boss1;

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
        //StartCoroutine(bossMove());
        velocity = Vector3.zero;
        Invoke("choosePattern", 5f);

    }

    void Update()
    {
        if (!isAttacking)
        {
            transform.position
                    = Vector3.SmoothDamp(transform.position, playerTransform.position, ref velocity, 1.8f);
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
                //summon();
                StartCoroutine(playerTrackingAttack());
                break;
        }
    }

    IEnumerator defaultAttack()
    {
        isAttacking = true;
        alert.SetActive(true);
        yield return StartCoroutine(delay(3));

        anim.SetTrigger("isJumping");
        Collider2D[] hitColliders = alert.GetComponent<BossAttackCheck>().checkRange();

        foreach (Collider2D collider in hitColliders)
        {
            GameObject objectHit = collider.gameObject;
            CharacterAction ch = objectHit.GetComponent<CharacterAction>();
            ch.TakeDamage(5);
        }

        alert.SetActive(false);
        isAttacking = false;

        Invoke("choosePattern", 5);
    }
    /*
    public void summon()
    {
        GameObject[] boss1_mini_array = new GameObject[6];
        boss1_mini_array[0] = Instantiate(boss1Prefab, new Vector3(-5, 0, 0), Quaternion.identity);
        boss1_mini_array[1] = Instantiate(boss1Prefab, new Vector3(-3, -2.5f, 0), Quaternion.identity);
        boss1_mini_array[2] = Instantiate(boss1Prefab, new Vector3(3, -2.5f, 0), Quaternion.identity);
        boss1_mini_array[3] = Instantiate(boss1Prefab, new Vector3(5, 0, 0), Quaternion.identity);
        boss1_mini_array[4] = Instantiate(boss1Prefab, new Vector3(3, 2.5f, 0), Quaternion.identity);
        boss1_mini_array[5] = Instantiate(boss1Prefab, new Vector3(-3, 2.5f, 0), Quaternion.identity);
        foreach (GameObject bossMini in boss1_mini_array)
        {
            bossMini.GetComponent<Boss1MiniAction>().bossPattern(0);
        }

        Invoke("choosePattern", 5);
    }
    */
    /*
    public IEnumerator playerTrackingAttack()
    {
        GameObject attackObj = Instantiate(attackPrefab, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        // 나중에 BossAttackCheck를 AttackCheck로 변경하기
        Collider2D[] hitColliders = attackObj.GetComponent<BossAttackCheck>().checkRange();

        foreach (Collider2D collider in hitColliders)
        {
            GameObject objectHit = collider.gameObject;
            CharacterAction ch = objectHit.GetComponent<CharacterAction>();
            ch.TakeDamage(5);
        }

        Destroy(attackObj);
        Invoke("choosePattern", 5);
    }
    */

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

    /*
    public void getDamage(float damage)
    {
        curHp -= damage;
        anim.SetTrigger("isDamaged");

        if (curHp < 0) curHp = 0;

        Debug.Log(curHp);
        hpBarRefresh(curHp);
    }
    */
    /*
    void hpBarRefresh(float hp)
    {
        hpImg.fillAmount = hp / maxHp;
    }*/
}
