using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1MiniAction : MonoBehaviour
{
    Animator anim;

    public GameObject alert;
    bool isAttacking = false;

    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void choosePattern()
    {
        bossPattern(0);
    }

    public void bossPattern(int patternIdx)
    {
        switch (patternIdx)
        {
            case 0:
                StartCoroutine(defaultAttack());
                break;
        }
    }


    public IEnumerator defaultAttack()
    {
        isAttacking = true;
        alert.SetActive(true);
        yield return StartCoroutine(delay(3));

        anim.SetTrigger("isJumping");
        Collider2D[] hitColliders = alert.GetComponent<BossAttackCheck>().checkRange();

        foreach (Collider2D collider in hitColliders)
        {
            if (collider == null) continue;
            GameObject objectHit = collider.gameObject;
            CharacterAction ch =  objectHit.GetComponent<CharacterAction>();
            ch.TakeDamage(5);
        }

        alert.SetActive(false);
        isAttacking = false;

        Destroy(gameObject);
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
     
}
