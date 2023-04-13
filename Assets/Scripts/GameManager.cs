using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    static bool _init = false;

    public bool isGameOver = false;
    PuzzleManager _puzzle = new PuzzleManager();
    public static PuzzleManager Puzzle { get { return Instance?._puzzle ; } }

    
    public static GameManager Instance
    {
        get
        {
            if (_init == false)
            {
                _init = true;
                GameObject go = GameObject.Find("GameManager");

                _instance = go.GetComponent<GameManager>();
                DontDestroyOnLoad(go);

            }
            return _instance;
        }
    }


}
