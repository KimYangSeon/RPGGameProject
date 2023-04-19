using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTypeManager : MonoBehaviour
{
    //public GameObject DealerObj;
    //public GameObject HealerObj;
    public Image AttackIcon;
    public Sprite DealerAttackSprite;
    public Sprite HealerAttackSprite;
    public GameObject PlayerObj;
    public Sprite DealerSprite;
    public Sprite HealerSprite;
    public RuntimeAnimatorController DealerAnim;
    public RuntimeAnimatorController HealerAnim;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.PlayerType == 0) // µô·¯
        {
            PlayerObj.GetComponent<SpriteRenderer>().sprite = DealerSprite;
            PlayerObj.GetComponent<Animator>().runtimeAnimatorController = DealerAnim;
            AttackIcon.sprite = DealerAttackSprite;

        }
        else
        {
            PlayerObj.GetComponent<SpriteRenderer>().sprite = HealerSprite;
            PlayerObj.GetComponent<Animator>().runtimeAnimatorController = HealerAnim;
            AttackIcon.sprite = HealerAttackSprite;
        }
    }


}
