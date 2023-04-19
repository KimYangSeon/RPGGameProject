using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    static bool _init = false;
    //static GameObject _obj;

    public bool isGameOver = false;
    public bool isStageClear = false;
    public bool CanMove = true;
    
    public static PuzzleManager Puzzle { get { return Instance?._puzzle ; } }
    PuzzleManager _puzzle = new PuzzleManager();

    public int PlayerType;
    GameObject _playerObj;
    GameObject _npcObj;
    public GameObject PlayerObj
    {
        get
        {
            if (_playerObj == null)
            {
                _playerObj = GameObject.Find("Player");
            }
            return _playerObj;
        }
    }

    public GameObject NPCObj
    {
        get
        {
            if (NPCObj == null)
            {
                _npcObj = GameObject.Find("NPC");
            }
            return _npcObj;
        }
    }


    public static GameManager Instance
    {
        get
        {
            if (_init == false)
            {
                _init = true;
                GameObject go = GameObject.Find("GameManager");
                if (go == null)
                {
                    go = new GameObject("GameManager");
                    go.AddComponent<GameManager>();
                }
                _instance = go.GetComponent<GameManager>();
                DontDestroyOnLoad(go);

            }
            return _instance;
        }
    }

    public void StageClear(int idx) 
    {
        if (idx < 3)
        {
            StartCoroutine(StageMove.Instance.LoadSceneAfterDelay($"Puzzle{idx + 1}", 5.0f));
        }
        else
        {
            Debug.Log("게임 클리어");
        }
        
    }


}
