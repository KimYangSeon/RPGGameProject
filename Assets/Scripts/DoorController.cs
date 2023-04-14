using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator _door1Anim;
    public GameObject Door1;

    void Start()
    {
        _door1Anim = Door1.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (GameManager.Puzzle.OpenDoor())
            {
                _door1Anim.SetBool("isOpen", true);
                Door1.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }
}
