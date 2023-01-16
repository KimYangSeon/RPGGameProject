using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    float h;
    float v;
    public float velocity;
    Rigidbody2D rigid;
    Animator anim;
    Vector3 dirVec;
    GameObject scanObject;
    bool isMoving = false;
    bool isDead = false;
    SpriteRenderer sprite;

    public float atk;
    public ParticleSystem dust;
    public float curPlayerHp;
    public float maxPlayerHp;
    public Image playerHpImg;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //StartCoroutine(dotdamage());
        hpBarRefresh(curPlayerHp);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GM.isGameOver) return;
        // 플레이어 이동  
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        /*
        if (anim.GetInteger("hAxisRaw") != h) {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }
        */
        if (h != 0 || v != 0)
        {
            if (!isMoving)
            {
                isMoving = true;
                anim.SetBool("isMoving", isMoving);
                dust.Play();
                
            }
            dirVec = new Vector2(h, v).normalized;
            if (h < 0) sprite.flipX = true;
            else sprite.flipX = false;
        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", isMoving);
            dust.Stop();
        }

        // scan object
        if (Input.GetKeyDown(KeyCode.X) && scanObject) Debug.Log(scanObject);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack(scanObject);
        }

    }

    void Attack(GameObject scanObject)
    {
        if (isDead) return;

        anim.SetTrigger("doAttack");
        if (!scanObject) return;
        scanObject.GetComponent<BossAction>().getDamage(atk);

    }

    public void getDamage(float damage)
    {
        if (isDead) return;

        curPlayerHp -= damage;
        anim.SetTrigger("doHit");

        if (curPlayerHp <= 0) {
            curPlayerHp = 0;
            playerDie();
        } 

        hpBarRefresh(curPlayerHp);
    }

    public void playerDie()
    {
        if (isDead) return;

        anim.SetTrigger("doDie");
        GameManager.GM.setGameOver();
    }

    void hpBarRefresh(float hp)
    {
        playerHpImg.fillAmount = hp / maxPlayerHp;
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v).normalized * Time.deltaTime * velocity; // 플레이어 이동

        Debug.DrawRay(rigid.position, dirVec * 1.5f, new Color(0, 1, 0)); // 레이캐스트
        // Boss 레이어만 스캔
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.5f, LayerMask.GetMask("Boss"));
        if (rayHit.collider)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else scanObject = null;


    }

    public void getHealing(float heal)
    {
        if (isDead) return;

        curPlayerHp += heal;
        if (curPlayerHp > maxPlayerHp)
        {
            curPlayerHp = maxPlayerHp;
        }
        hpBarRefresh(curPlayerHp);
    }

    IEnumerator dotdamage()
    {
        while (true)
        {
            getDamage(1);
            yield return new WaitForSeconds(1.0f);
        }
        
    }
}
