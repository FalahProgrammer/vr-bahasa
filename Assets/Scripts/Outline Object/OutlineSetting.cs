using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class OutlineSetting
{
    private Transform _activatedOutline { set; get; }

    public OutlineSetting(Transform activatedOutline)
    {
        _activatedOutline = activatedOutline;
    }
    
    
}
