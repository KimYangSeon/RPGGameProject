using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController2 : MonoBehaviour
{
    public GameObject PlayerObj;
    Transform _playerTrans;
    // Start is called before the first frame update
    void Start()
    {
        _playerTrans = PlayerObj.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position += (_playerTrans.position - transform.position);
        //transform.position = PlayerObj.transform.position;
    }
}
