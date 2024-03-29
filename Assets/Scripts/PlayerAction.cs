using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAction : CharacterAction
{
    float h;
    float v;
    public float velocity;
    public Rigidbody2D rigid;
    Animator anim;
    Vector3 dirVec;
    GameObject scanObject;
    bool isMoving = false;
    //public bool isDead = false;
    SpriteRenderer sprite;

    public float atk;
    //public ParticleSystem dust;
    public int curPlayerHp;
    public int maxPlayerHp;
    public Image[] playerHpImg = new Image[2];
    public Image coolDownImg;
    // public bool isOverHealed;
    public bool randomMode;
    public float curTime;
    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    //public float timer;
    //public bool isTimeOver;
    public Image attackIcon;
    public Color pressedColor;
    public GameObject skillIcon;
    public GameObject NPCObj;
    bool isAttackEnable = true;
    bool isPuzzleStage = false;
    bool isHealEnable = true;
    float skillRange=2;
    float distance;

    Coroutine dotCo;
    Coroutine timerCo;
    Coroutine attackCo;

    public Text playerHPText;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //resetPlayer();
        //hpBarRefresh(curPlayerHp);
        //StartCoroutine(dotdamage());
        if (randomMode) StartCoroutine(randomMove());
        if (SceneManager.GetActiveScene().name.Contains("Puzzle"))
        {
            isPuzzleStage = true;
            skillIcon.GetComponentInChildren<Text>().text = "조사\nX";
        }
        else
        {
            isPuzzleStage = false;
            skillIcon.GetComponentInChildren<Text>().text = "스킬\nX";
        }
        //Debug.Log("플레이어 start");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        // 플레이어 이동  
        if (!randomMode)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        if (h != 0 || v != 0)
        {
            if (!isMoving)
            {
                isMoving = true;
                anim.SetBool("isMoving", isMoving);
                //dust.Play();
                
            }
            dirVec = new Vector2(h, v).normalized;
            if (h > 0) sprite.flipX = true;
            else sprite.flipX = false;
        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", isMoving);
            //dust.Stop();
        }

        // scan object
        if (Input.GetKeyDown(KeyCode.X) && scanObject && isPuzzleStage)
        {
            if(scanObject && isPuzzleStage)
            {
                Search();
            }
            else if (!isPuzzleStage)
            {
                // 스킬
            }
                
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (GameManager.Instance.PlayerType == 0)
                Attack(scanObject);
            else
                Healing(5);
        }


    }

    public void Search()
    {
        ///Debug.Log(scanObject);
        if (scanObject.tag == "Star")
        {
            scanObject.GetComponent<StarObject>().SearchEvent();
        }
        
    }

    IEnumerator randomMove()
    {
        while (true)
        {
            h = Random.Range(-1, 2);
            v = Random.Range(-1, 2);
            yield return new WaitForSeconds(2.0f);
        }
    }

    public bool Healing(int value)
    {
        if (isHealEnable && enableRange())
        {
            NPCObj.GetComponent<NPCDealerAction>().getHealing(value);
            //healEffect.SetActive(true);
            //StartCoroutine(effectDealy());
            StartCoroutine(HealCoolTime());
            return true;

        }
        return false;
    }

    bool enableRange()
    {
        distance = Vector2.Distance(transform.localPosition, NPCObj.GetComponent<Transform>().localPosition);
        if (distance > skillRange) return false;
        return true;
    }

    public void Attack(GameObject scanObject)
    {
        StartCoroutine(IconEffect(attackIcon));

        if (isDead || !isAttackEnable) return; // 죽었는지 & 쿨타임 체크

            if (scanObject != null && scanObject.tag == "Boss")
            {
                anim.SetTrigger("doAttack");
                StartCoroutine(attackCoolTime());

                //if (!scanObject) return;
                scanObject.GetComponent<BossAction>().getDamage(atk);
            }


    }

    IEnumerator IconEffect(Image icon)
    {
        Color origin = icon.color;
        icon.color *= pressedColor;
        yield return new WaitForSeconds(0.1f);
        icon.color = origin;
    }

    public override void TakeDamage(int damage)
    {
        if (isDead || damage <= 0) return;

        curPlayerHp -= damage;
        anim.SetTrigger("doHit");

        if (curPlayerHp <= 0) {
            curPlayerHp = 0;
            OnDead();
        } 

        hpBarRefresh(curPlayerHp);
    }

    public override void OnDead()
    {
        if (isDead) return;
        isDead = true;
        anim.SetTrigger("doDie");
        GameManager.Instance.GameOver();
        //StopCoroutine(dotdamage());
    }

    void hpBarRefresh(int hp)
    {
        playerHPText.text = "HP " + hp ;
        foreach(Image img in playerHpImg)
        {
            img.fillAmount = (float)hp / maxPlayerHp;
        }
        
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.CanMove)
        {
            rigid.velocity = new Vector2(h, v).normalized * Time.deltaTime * velocity; // 플레이어 이동
        }
        

        // Debug.DrawRay(rigid.position, dirVec * 1.5f, new Color(0, 1, 0)); // 레이캐스트

        // Object 레이어만 스캔
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.5f, LayerMask.GetMask("Object"));
        if (rayHit.collider)
        {
            scanObject = rayHit.collider.gameObject;
            //Debug.Log(scanObject);
        }
        else scanObject = null;


    }

    public void getHealing(int value)
    {
        if (isDead) return;
        //isOverHealed = false;
        //Debug.Log(curPlayerHp);
        curPlayerHp += value;
        if (curPlayerHp > maxPlayerHp)
        {
            curPlayerHp = maxPlayerHp;
            //isOverHealed = true;
        }
        hpBarRefresh(curPlayerHp);
        //Debug.Log(isOverHealed);
    }

    public IEnumerator dotdamage()
    {
        while (true)
        {
            int damage = Random.Range(0, 5);
            TakeDamage(damage);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator attackCoolTime()
    {
        float t = 0f;
        isAttackEnable = false;
        //coolDownImg.fillAmount = 1;

        while (true)
        {
            if (t < 0.1f)
            {
                yield return fixedUpdate;
                t += Time.deltaTime;
                //coolDownImg.fillAmount = (0.2f - t) / 0.2f;
            }
            else
            {
                isAttackEnable = true;
                //coolDownImg.fillAmount = 0;
                yield break;
            }
        }
    }
    public void resetPlayer()
    {
        /*
        if(dotCo != null)
            StopCoroutine(dotCo);
        if(timerCo != null)
            StopCoroutine(timerCo);
        */

        float x = Random.Range(-8.0f, 8.0f);
        float y = Random.Range(-4.0f, 5.0f);
        transform.localPosition = new Vector3(x, y, 0);

        //transform.localPosition = new Vector3(2, 0, 0);
        curPlayerHp = maxPlayerHp;
        //GameManager.GM.isGameOver = false;
        //GameManager.GM.timeOver = false;
        //isTimeOver = false;
        isDead = false;
        anim.ResetTrigger("doDie");
        hpBarRefresh(curPlayerHp);

        //dotCo = StartCoroutine(dotdamage());
        //timerCo = StartCoroutine(startTimer());

        //StartCoroutine(dotdamage());
        //StartCoroutine(startTimer());
    }

    IEnumerator HealCoolTime()
    {
        float t = 0f;
        isHealEnable = false;
        coolDownImg.fillAmount = 1;

        while (true)
        {
            if (t < 5f)
            {
                yield return fixedUpdate;
                t += Time.deltaTime;
                coolDownImg.fillAmount = (5 - t) / 5f;
            }
            else
            {
                isHealEnable = true;
                coolDownImg.fillAmount = 0;
                yield break;
            }
        }
    }
}
