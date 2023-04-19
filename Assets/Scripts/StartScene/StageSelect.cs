using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public Button[] StageButtons = new Button[6];
    int _selectedStage;

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

    public void SelectStage(int idx)
    {
        _selectedStage = idx;
    }

    public void MoveToSelectedStage()
    {
        //_selectedStage = idx;
        if (_selectedStage % 2 != 0)
        {
            StartCoroutine(StageMove.Instance.LoadScene($"Puzzle{_selectedStage / 2 + 1}"));
            //SceneManager.LoadScene($"Puzzle{idx}");
        }
        else
        {
            StartCoroutine(StageMove.Instance.LoadScene($"Stage{_selectedStage / 2}"));
            //SceneManager.LoadScene($"Stage{idx}");
        }
        
    }

    public void SelecteParty(int idx)
    {
        if (idx == 0) // 플레이어 = 딜러
        {
            GameManager.Instance.PlayerType = 0;
        }
        else // 플레이어 = 힐러
        {
            GameManager.Instance.PlayerType = 1;
        }
        MoveToSelectedStage();
    }
}
