using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager
{
   // public bool hasKey = false;
    bool _isOpen = false;

    public bool OpenDoor()
    {
        if (_isOpen) return false;
        _isOpen = true;
        return true;
    }


}
