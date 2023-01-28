using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    // Start is called before the first frame update

    public bool isGameOver = false;
    private void Awake()
    {
        if(!GM)
            GM = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGameOver()
    {
        isGameOver = true;
        //Debug.Log("Game Over");
        //Time.timeScale = 0;
    }
}
