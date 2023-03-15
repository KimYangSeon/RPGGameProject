using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    // Start is called before the first frame update

    public bool isGameOver = false;
    public bool timeOver = false;
    public float timer;
    public float curTime;
    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    private void Awake()
    {
        if(!GM)
            GM = this;
    }
    void Start()
    {
        //timeOver = false;
        //StartCoroutine(startTimer());
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

    public void resetManager()
    {
        timeOver = false;
        StopCoroutine(startTimer());
        StartCoroutine(startTimer());
    }

    IEnumerator startTimer()
    {
        curTime = 0;
        Debug.Log("start timer");
        
        while (true)
        {
            if(curTime < timer)
            {
                yield return fixedUpdate;
                curTime += Time.deltaTime;
            }
            else
            {
                Debug.Log("timeover");
                timeOver = true;
                yield return null;
            }
        }
        
    }
}
