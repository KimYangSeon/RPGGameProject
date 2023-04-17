using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCheck : MonoBehaviour
{
    public LayerMask layersToCheck;
   // public float skillRadius;

    public Collider2D[] checkRange()
    {
        Collider2D collider = GetComponent<Collider2D>();

        ContactFilter2D contact = new ContactFilter2D();
        contact.SetLayerMask(layersToCheck);

        Collider2D[] hitColliders = new Collider2D[2];

        collider.OverlapCollider(contact, hitColliders);
        //Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius, layersToCheck);
        //Debug.Log(hitColliders[0]);
        return hitColliders;
    }
}
