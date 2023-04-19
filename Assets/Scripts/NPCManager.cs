using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject HealerNPC;
    public GameObject DealerNPC;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.PlayerType == 0)
        {
            HealerNPC.SetActive(true);
        }
        else
        {
            DealerNPC.SetActive(true);
        }
    }

}
