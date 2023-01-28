using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAction : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    IEnumerator coolTimeCounter;
    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    public float coolTime = 5f;
    public float filledTime;
    bool isHealEnable;
    public float defaultHealValue = 20;

    void Start()
    {
        coolTimeCounter = cntCoolTime();
        isHealEnable = true;
        //StartCoroutine(dotHealing());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C)) Debug.Log("bb");
    }

    public bool Healing(float value)
    {
        if (isHealEnable)
        {
           // Debug.Log("heal");
            player.GetComponent<PlayerAction>().getHealing(value);
            StartCoroutine(cntCoolTime());
            return true;
            
        }
        return false;
    }

    IEnumerator dotHealing()
    {
        while (true)
        {
            Healing(defaultHealValue);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator cntCoolTime()
    {
        filledTime = 0f;
        isHealEnable = false;
        //Debug.Log("ƒ≈∏¿” Ω√¿€");

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
}
