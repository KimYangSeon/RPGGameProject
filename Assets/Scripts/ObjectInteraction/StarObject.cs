using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{


    public void SearchEvent()
    {
        if (GameManager.Puzzle.OpenDoor())
        {
            Debug.Log("�� ����");
        }
        else
        {
            Debug.Log("�� ���� ����");
        }
    }


}
