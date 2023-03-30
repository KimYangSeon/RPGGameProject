using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCheck : MonoBehaviour
{
    public LayerMask playerLayer;
    public float skillRadius;
    public Collider2D[] checkRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius, playerLayer);
        return hitColliders;
    }
}
