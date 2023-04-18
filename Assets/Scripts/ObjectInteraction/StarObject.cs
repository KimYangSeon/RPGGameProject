using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{
    public GameObject Bridge;
    Transform _trans;
    public bool isBlue;
    GameObject _door;

    public void SearchEvent()
    {
        if (!isBlue)
        {
            Rotate();
        }
        else
        {
            _door = GameObject.Find("Door");
            _door.GetComponent<Animator>().SetBool("isOpen", true);
            _door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        
    }

    public void Rotate()
    {
        float cur = Bridge.transform.localEulerAngles.z;
        Bridge.transform.localEulerAngles = new Vector3(0, 0, cur - 90);
    }


}
