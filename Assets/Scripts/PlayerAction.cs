using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    SpriteRenderer sprite;

    public float atk;
    public ParticleSystem dust;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
        anim.SetTrigger("doAttack");
        if (!scanObject) return;
        scanObject.GetComponent<BossAction>().getDamage(atk);

    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v).normalized * Time.deltaTime * velocity; // 플레이어 이동

        Debug.DrawRay(rigid.position + new Vector2(0, 0.5f), dirVec * 1.5f, new Color(0, 1, 0)); // 레이캐스트
        // Boss 레이어만 스캔
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position + new Vector2(0, 0.5f), dirVec, 1.5f, LayerMask.GetMask("Boss"));
        if (rayHit.collider)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else scanObject = null;


    }
}
