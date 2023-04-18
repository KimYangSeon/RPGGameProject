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
                //GameObject _canvasObj;
                //_canvasObj = new GameObject("Panel Canvas");
                //_canvasObj.transform.parent = _obj.transform;
                //_canvasObj.AddComponent<Canvas>();

                //GameObject _panelObj;
                //_panelObj = new GameObject("Panel");
                //_panelObj.transform.parent = _canvasObj.transform;
                //image = _panelObj.AddComponent<Image>();
                //image.color = Color.black;

                //DontDestroyOnLoad(_obj);
            }
            
            return _instance;
        }
    }
    

    void Awake()
    {
        //_panel = transform.GetChild(0).gameObject;
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
        Debug.Log("??????");
        yield return new WaitForSeconds(delay);
        Debug.Log("???");
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
