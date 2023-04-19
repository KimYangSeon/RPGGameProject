using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1MiniAction : MonoBehaviour
{
    Animator _anim;

    public GameObject alert;
    bool isAttacking = false;

    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    public void bossPattern(int patternIdx)
    {
        switch (patternIdx)
        {
            case 8:
                StartCoroutine(DefaultAttack(0.5f, false));
                break;
        }
    }


    public IEnumerator DefaultAttack(float delayTime, bool isContinuous, int size = 8)
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
            CharacterAction ch = objectHit.GetComponent<CharacterAction>();
            if (ch != null)
                ch.TakeDamage(5);
        }

        alert.SetActive(false);


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
     
}
