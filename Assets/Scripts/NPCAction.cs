using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAction : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject healEffect;

    IEnumerator coolTimeCounter;
    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    public float coolTime = 5f;
    public float filledTime;
    bool isHealEnable;
    public float defaultHealValue = 20;
    public float skillRange;
    public float distance;

    public float curNPCHp;
    public float maxNPCHp;


    void Start()
    {
        coolTimeCounter = cntCoolTime();
        isHealEnable = true;
        //StartCoroutine(dotHealing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Healing(float value)
    {
        //Debug.Log("Èú ½Ãµµ");
        //bool abcd = enableRange();
        //Debug.Log(abcd);
        if (isHealEnable && enableRange())
        {
            //Debug.Log("heal");
            player.GetComponent<PlayerAction>().getHealing(value);
            healEffect.SetActive(true);
            StartCoroutine(effectDealy());
            StartCoroutine(cntCoolTime());
            return true;
      
        }
        return false;
    }

    bool enableRange()
    {
        //float distance = Vector2.Distance(transform.position, player.GetComponent<Transform>().position);
        //Debug.Log(distance);
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
        //Debug.Log("ÄðÅ¸ÀÓ ½ÃÀÛ");

        while (true)
        {
            if(filledTime < coolTime)
            {
                yield return fixedUpdate;
                filledTime += Time.deltaTime;
                //Debug.Log(filledTime);
            }
            else
            {
                isHealEnable = true;
                yield break;
            }
        }
    }

 

    IEnumerator effectDealy()
    {
        yield return new WaitForSeconds(1.0f);
        healEffect.SetActive(false);
    }
}
