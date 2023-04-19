using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarType
{
    White=0,
    Blue=1,
    Red=2
}

public class StarObject : MonoBehaviour
{

    public GameObject Bridge;
    public StarType type;
    public List<int> moveList = new List<int>();
    public bool isVertical;
    //Transform _trans;
    GameObject _door;
    GameObject _map;
    Transform _playerTrans;
    int _moveIdx = 0;

    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

    public void SearchEvent()
    {
        if (type == StarType.White)
        {
            Rotate();
        }
        else if(type == StarType.Blue)
        {
            _door = GameObject.Find("Door");
            _door.GetComponent<Animator>().SetBool("isOpen", true);
            _door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (type == StarType.Red)
        {
            Move();

        }
        
    }

    public void Rotate()
    {
        float cur = Bridge.transform.localEulerAngles.z;
        Bridge.transform.localEulerAngles = new Vector3(0, 0, cur - 90);
    }

    public void Move()
    {
        GameManager.Instance.CanMove = false;
        if (_playerTrans == null)
        {
            _playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        }
        _map = transform.parent.gameObject;


        if (moveList[_moveIdx] >= 0)
        {
            StartCoroutine(MapMove(moveList[_moveIdx], isVertical, 1));
        }
        else
        {
            StartCoroutine(MapMove(moveList[_moveIdx], isVertical, -1));
        }
        
        _moveIdx = (_moveIdx + 1) % moveList.Count;
        //Debug.Log(_moveIdx);
    }

    IEnumerator MapMove(float distance, bool isVertical, int dir)
    {
        float cur = 0;
        Vector3 initMapPos = _map.transform.position;
        Vector3 initPlayerPos = _playerTrans.position;
        float d = Mathf.Abs(distance);

        if (isVertical)
        {
            while (cur < d)
            {
                _map.transform.position = new Vector3(_map.transform.position.x, _map.transform.position.y + Time.deltaTime * 5 * dir, 0);
                _playerTrans.position = new Vector3(_playerTrans.position.x, _playerTrans.position.y + Time.deltaTime * 5 * dir, 0);
                cur += Time.deltaTime * 5;

                yield return fixedUpdate;
            }

            _map.transform.position = initMapPos + new Vector3(0, distance, 0);
            _playerTrans.position = initPlayerPos + new Vector3(0, distance, 0);
        }
        else
        {
            while (cur < d)
            {
                _map.transform.position = new Vector3(_map.transform.position.x + Time.deltaTime * 5 * dir, _map.transform.position.y, 0);
                _playerTrans.position = new Vector3(_playerTrans.position.x + Time.deltaTime * 5 * dir, _playerTrans.position.y, 0);
                cur += Time.deltaTime * 5;

                yield return fixedUpdate;
            }

            _map.transform.position = initMapPos + new Vector3(distance, 0, 0);
            _playerTrans.position = initPlayerPos + new Vector3(distance, 0, 0);
        }
        

        GameManager.Instance.CanMove = true;


    }


}
