using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    public bool isDead;
    public virtual void TakeDamage(int damage)
    {
        // Default implementation for taking damage
        // Can be overridden by child classes
    }

    public virtual void OnDead()
    {
        // Default implementation for taking damage
        // Can be overridden by child classes
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
