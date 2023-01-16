using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAction : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dotHealing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Healing(float heal)
    {
        player.GetComponent<PlayerAction>().getHealing(heal);
    }

    IEnumerator dotHealing()
    {
        while (true)
        {
            Healing(1.0f);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
