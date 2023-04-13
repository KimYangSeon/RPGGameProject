using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{


    public void SearchEvent()
    {
        if (GameManager.Puzzle.OpenDoor())
        {
            Debug.Log("문 열림");
        }
        else
        {
            Debug.Log("문 열기 실패");
        }
    }


}
