using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMove : MonoBehaviour
{
    public GameObject panel;
    Image image;

    void Awake()
    {
        image = panel.GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(FadeOut("Stage1"));
            //SceneManager.LoadScene("Stage1");
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        Color color = image.color;                            

        while(image.color.a < 1)
        {
            color.a += 0.1f;

            image.color = color;
            //Debug.Log(image.color.a);
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(sceneName);

        yield return null;                                       
    }

    IEnumerator FadeIn()
    {
        Color color = image.color;

        while (image.color.a > 0)
        {
            color.a -= 0.1f;

            image.color = color;
            //Debug.Log(image.color.a);
            yield return new WaitForSeconds(0.01f);
        }

        //SceneManager.LoadScene("Stage1");

        yield return null;
    }
}
