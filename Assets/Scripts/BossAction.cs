using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PatternType
{
    Default = 0,
    DefaultDonut=1,
    DefaultDonut2=2,
    DonutDefault=3,
    DonutDefault2=4,
    Traking=5,
    Traking2=6,
    Summon=7,
    DefaultAttackShort=8,
}

public class BossAction : MonoBehaviour
{
    public float curHp;
    public float maxHp;
    public Image hpImg;
    public float speed;
    public int BossNum;
    public GameObject alert;
    public GameObject attackPrefab;
    public GameObject boss1MiniPrefab;
    public GameObject player;
    public GameObject DealerObj;
    public GameObject DonutAlert;
    public GameObject DonutAlertPrefeb;

    public bool isDead = false;

    bool isAttacking = false;
    Vector3 velocity;
    Transform _playerTransform;
    Transform _npcTransform;
    Animator _anim;
    int[] _patternArr = { (int) PatternType.Summon, (int)PatternType.DefaultAttackShort, (int)PatternType.Traking2, (int)PatternType.DonutDefault2 };
    int _patternIdx = 0;

    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerTransform = player.GetComponent<Transform>();
        _npcTransform = DealerObj.GetComponent<Transform>();
    }

    void Start()
    {
        velocity = Vector3.zero;
        StartCoroutine(initPattern());
        

    }

    IEnumerator initPattern()
    {
        yield return StartCoroutine(BossWait());
        if (BossNum == 3)
        {
            Invoke("choosePattern", 2f);
        }
        else
        {
            Invoke("choosePattern", 5f);
        }
        
    }

    IEnumerator BossWait()
    {
        isAttacking = true;
        yield return new WaitForSeconds(3.0f);
        isAttacking = false;
    }

    void Update()
    {
        if (isDead) return;

        if (!isAttacking)
        {
            if (GameManager.Instance.PlayerType == 0)
            {
                transform.position
            = Vector3.SmoothDamp(transform.position, _playerTransform.position, ref velocity, 1.8f);
            }
            else
            {
                transform.position
            = Vector3.SmoothDamp(transform.position, _npcTransform.position, ref velocity, 1.8f);
            }
            

            // npc 학습용
            // transform.position
            //= Vector3.SmoothDamp(transform.position, _npcTransform.position, ref velocity, 1.8f);


        }
    }

    public virtual void choosePattern()
    {
        if (isDead) return;

        if (BossNum == 1)
        {
            bossPattern((int)PatternType.Default);
        }
        else if (BossNum == 2)
        {
            if(curHp / maxHp <= 0.5f)
            {
                int idx = Random.Range(0, 2);
                if(idx==0)
                    bossPattern((int)PatternType.Traking); 
                else
                    bossPattern((int)PatternType.DefaultDonut); 
            }
            else
            {
                bossPattern((int)PatternType.DefaultDonut); 
            }
            
        }
        else if (BossNum == 3)
        {
            if (curHp / maxHp > 0.7f)
            {
                int idx = Random.Range(0, 3);
                if (idx == 0)
                    bossPattern((int)PatternType.DefaultDonut2);
                else if (idx == 1)
                    bossPattern((int)PatternType.DonutDefault);
                else if (idx == 2)
                    bossPattern((int)PatternType.Traking2);

            }
            else if (curHp / maxHp > 0.4f)
            {
                int idx = Random.Range(0, 3);
                if (idx == 0)
                    bossPattern((int)PatternType.DefaultDonut2);
                else if (idx == 1)
                    bossPattern((int)PatternType.DonutDefault2);
                else if (idx == 2)
                    bossPattern((int)PatternType.Traking2);
            }
            else
            {
                bossPattern(_patternArr[_patternIdx]);
                _patternIdx = (_patternIdx + 1) % 4;
            }
            
        }

    }

    public void bossPattern(int patternIdx)
    {
        switch (patternIdx)
        {
            case 0:
                StartCoroutine(DefaultAttack(3, false, 6));
                break;
            case 1:
                StartCoroutine(DefaultAndDonutAttack());
                break;
            case 2:
                StartCoroutine(DefaultAndDonutAttack2());
                break;
            case 3:
                StartCoroutine(DonutAndDefaultAttack());
                break;
            case 4:
                StartCoroutine(DonutAndDefaultAttack2());
                break;
            case 5:
                StartCoroutine(ContinuousAttack());
                break;
            case 6:
                StartCoroutine(ContinuousAttack2());
                break;
            case 7:
                StartCoroutine(Summon());
                break;
            case 8:
                StartCoroutine(DefaultAttack(0.5f, false, 6, 1));
                break;
        }
    }


    public IEnumerator DefaultAttack(float delayTime, bool isContinuous, int size=3, int delay=6)
    {
        isAttacking = true;
        alert.SetActive(true);
        alert.transform.localScale = new Vector3(size, size, 1);
        yield return StartCoroutine(Delay(delayTime));

        _anim.SetTrigger("isJumping");
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
        //isAttacking = false;

        if(!isContinuous)
        {
            isAttacking = false;
            Invoke("choosePattern", delay);
        }
           
    }

    IEnumerator ContinuousAttack()
    {
        StartCoroutine(PlayerTrackingAttack(2, true));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayerTrackingAttack(2, true));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayerTrackingAttack(2, false, 1));
    }

    IEnumerator ContinuousAttack2()
    {
        StartCoroutine(PlayerTrackingAttack(2, true));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayerTrackingAttack(2, true));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayerTrackingAttack(2, true));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayerTrackingAttack(2, true));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayerTrackingAttack(2, false,1));
    }

    public IEnumerator PlayerTrackingAttack(float delayTime, bool isContinuous, int delay=6)
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
            Invoke("choosePattern", delay);
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
        _anim.SetTrigger("isDamaged");

        if (curHp <= 0)
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

    public IEnumerator DefaultAndDonutAttack2()
    {
        // 기본 공격
        yield return StartCoroutine(DefaultAttack(3, true, 4));

        //0.5초 딜레이
        yield return new WaitForSeconds(0.5f);
        // 도넛 공격
        yield return StartCoroutine(DonutAttack(1, false, 4, 2));

    }

    public IEnumerator DonutAndDefaultAttack()
    {
        yield return StartCoroutine(DonutAttack(3, true, 4));

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(DefaultAttack(1, false, 4, 2));
    }

    public IEnumerator DonutAndDefaultAttack2()
    {

        StartCoroutine(DonutAttack(3, true));

        yield return new WaitForSeconds(1);

        StartCoroutine(DefaultAttack(3, true, 6));

        yield return new WaitForSeconds(1);

        StartCoroutine(DonutAttack(3, false, 6, 2));
    }

    IEnumerator Summon()
    {
        GameObject[] boss1_mini_array = new GameObject[6];
        boss1_mini_array[0] = Instantiate(boss1MiniPrefab, new Vector3(-7, 0, 0), Quaternion.identity);
        boss1_mini_array[1] = Instantiate(boss1MiniPrefab, new Vector3(-3, 4, 0), Quaternion.identity);
        boss1_mini_array[2] = Instantiate(boss1MiniPrefab, new Vector3(3, 4, 0), Quaternion.identity);
        boss1_mini_array[3] = Instantiate(boss1MiniPrefab, new Vector3(7, 0, 0), Quaternion.identity);
        boss1_mini_array[4] = Instantiate(boss1MiniPrefab, new Vector3(3, -4, 0), Quaternion.identity);
        boss1_mini_array[5] = Instantiate(boss1MiniPrefab, new Vector3(-3, -4, 0), Quaternion.identity);


        yield return new WaitForSeconds(2.0f);

        foreach (GameObject bossMini in boss1_mini_array)
        {
            bossMini.GetComponent<Boss1MiniAction>().bossPattern((int)PatternType.DefaultAttackShort);
        }

        yield return new WaitForSeconds(2.0f);


        StartCoroutine(MiniBossMove(boss1_mini_array[0], 1.5f, 2.5f));
        StartCoroutine(MiniBossMove(boss1_mini_array[1], 3, 0));
        StartCoroutine(MiniBossMove(boss1_mini_array[2], 2.5f, -1.5f));
        StartCoroutine(MiniBossMove(boss1_mini_array[3], -1.5f, -2.5f));
        StartCoroutine(MiniBossMove(boss1_mini_array[4], -3, 0));
        StartCoroutine(MiniBossMove(boss1_mini_array[5], -2.5f, 1.5f));


        yield return new WaitForSeconds(2.0f);
            

        foreach (GameObject bossMini in boss1_mini_array) // 이동 후 한번 더 공격
        {
            bossMini.GetComponent<Boss1MiniAction>().bossPattern((int)PatternType.DefaultAttackShort);
        }

        yield return new WaitForSeconds(2.0f);
        
        for(int i=0; i<6; i++)
        {
            Destroy(boss1_mini_array[i]);
        }
        

        Invoke("choosePattern", 2);
    }

    IEnumerator MiniBossMove(GameObject go, float x, float y)
    {
        Vector3 _goalPos = go.transform.position + new Vector3(x, y, 0);
        float curX = 0;
        float curY = 0;
        float absX = x < 0 ? x * -1 : x;
        float absY = y < 0 ? y * -1 : y;

        while (curX < absX || curY < absY)
        {
            if(curX < absX)
            {
                go.transform.position += new Vector3(x * Time.deltaTime * 2, 0, 0);
                curX += Mathf.Abs(Time.deltaTime * x * 2);
            }

            if(curY < absY)
            {
                go.transform.position += new Vector3(0, y * Time.deltaTime * 2, 0);
                curY += Mathf.Abs(Time.deltaTime * y * 2);
            }

            yield return fixedUpdate;
        }

        go.transform.position = _goalPos;
    }

    public IEnumerator DonutAttack(float delayTime, bool isContinuous, int size=3, int delay = 6)
    {
        isAttacking = true;
        GameObject attackObj = Instantiate(DonutAlertPrefeb, transform.position - new Vector3(0, 0.49f, 0), Quaternion.identity);
        //DonutAlert.SetActive(true);
        attackObj.transform.localScale = new Vector3(size, size, 1);
        yield return StartCoroutine(Delay(delayTime));

        _anim.SetTrigger("isJumping");
        Collider2D[] hitColliders = attackObj.GetComponent<BossAttackCheck>().checkRange();

        foreach (Collider2D collider in hitColliders)
        {
            if (collider == null) continue;
            GameObject objectHit = collider.gameObject;
            CharacterAction ch = objectHit.GetComponent<CharacterAction>();
            if (ch != null)
                ch.TakeDamage(5);
        }

        Destroy(attackObj);
        //attackObj.SetActive(false);
        

        if (!isContinuous)
        {
            isAttacking = false;
            Invoke("choosePattern", delay);
        }

            
    }

    void OnDead()
    {
        Debug.Log("Stage Clear");
        isDead = true;
        _anim.SetBool("Dead", isDead);
        DataManager.Instance.LoadGameData();
        DataManager.Instance.data.isCleared[BossNum * 2 - 1] = true;
        DataManager.Instance.SaveGameData();
        GameManager.Instance.StageClear(BossNum);
        //StartCoroutine(StageMove.Instance.LoadSceneAfterDelay("Puzzle2", 3.0f));
    }

    public void AfterDead()
    {
        gameObject.SetActive(false);
    }



    public void Reset()
    {
        isDead = false;
        curHp = maxHp;
        transform.localPosition = new Vector2(0, 0);
        hpBarRefresh(curHp);
    }
}
