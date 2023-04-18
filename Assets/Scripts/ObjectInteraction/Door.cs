using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int CurSceneIdx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DataManager.Instance.LoadGameData();
            DataManager.Instance.data.isCleared[CurSceneIdx] = true;
            DataManager.Instance.SaveGameData();
            StartCoroutine(StageMove.Instance.LoadScene($"Stage{CurSceneIdx / 2 + 1}"));
            //SceneManager.LoadScene($"Stage{idx}");
        }
    }

}
