using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCheck : MonoBehaviour
{
    public LayerMask layersToCheck;
    public float skillRadius;
    

    public Collider2D[] checkRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius, layersToCheck);
        //Debug.Log(hitColliders[0]);
        return hitColliders;
    }
}
