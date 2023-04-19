using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMove : MonoBehaviour
{
    public GameObject _panel;
    Image image;
    static StageMove _instance;
    static GameObject _obj;
    
    public static StageMove Instance
    {
        get
        {
            
            if (!_instance)
            {
                _obj = new GameObject("SceneManager");
                _instance = _obj.AddComponent(typeof(StageMove)) as StageMove;
                DontDestroyOnLoad(_obj);
            }
            
            return _instance;
        }
    }
    

    void Awake()
    {
        if (_panel == null) return;
        image = _panel.GetComponent<Image>();
        if(!_instance) _instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        StartCoroutine(StartScene());
    }

    public IEnumerator LoadScene(string sceneName)
    {
        if (_panel == null)
        {
            Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName);
            yield return null;
        }
        else
        {
            _panel.SetActive(true);
            Color color = image.color;

            while (image.color.a < 1)
            {
                color.a += 0.1f;

                image.color = color;
                //Debug.Log(image.color.a);
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(sceneName);
            StartCoroutine(StartScene());
            //_panel.SetActive(false);

            yield return null;
        }

                                    
    }

    public IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_panel == null)
        {
            Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName);
            yield return null;
        }
        else
        {
            _panel.SetActive(true);
            Color color = image.color;

            while (image.color.a < 1)
            {
                color.a += 0.1f;

                image.color = color;
                //Debug.Log(image.color.a);
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(sceneName);
            StartCoroutine(StartScene());
            //_panel.SetActive(false);

            yield return null;
        }


    }

    public IEnumerator StartScene()
    {
        if (_panel == null)
        {

            yield return null;
        }
        else
        {
            _panel.SetActive(true);
            Color color = image.color;

            while (image.color.a > 0)
            {
                color.a -= 0.1f;

                image.color = color;
                //Debug.Log(image.color.a);
                yield return new WaitForSeconds(0.05f);
            }
            _panel.SetActive(false);
            yield return null;
        }

        
    }
}
