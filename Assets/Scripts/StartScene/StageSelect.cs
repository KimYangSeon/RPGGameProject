using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public Button[] StageButtons = new Button[6];

    void Start()
    {
        DataManager.Instance.LoadGameData();
        StageButtons[0].interactable = true;
        //DataManager.Instance.data.isUnlock[0] = true;
        for (int i=0; i<5; i++)
        {
            if (DataManager.Instance.data.isCleared[i])
            {
                StageButtons[i+1].interactable = true;
            }
        }
        //DataManager.Instance.SaveGameData();
        //DataManager.Instance.LoadGameData();
    }

    public void MoveToSelectedStage(int idx)
    {
        if (idx % 2 != 0)
        {
            StartCoroutine(StageMove.Instance.LoadScene($"Puzzle{idx/2+1}"));
            //SceneManager.LoadScene($"Puzzle{idx}");
        }
        else
        {
            StartCoroutine(StageMove.Instance.LoadScene($"Stage{idx/2}"));
            //SceneManager.LoadScene($"Stage{idx}");
        }
        
    }
}
