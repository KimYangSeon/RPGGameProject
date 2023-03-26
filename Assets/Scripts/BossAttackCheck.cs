using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCheck : MonoBehaviour
{
    public LayerMask playerLayer;

    public Collider2D[] checkRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 3f, playerLayer);
        return hitColliders;
    }
    
}
