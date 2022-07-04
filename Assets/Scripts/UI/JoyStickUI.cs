using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickUI : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Vector3 positionOffset;

    private void Update()
    {
        var pos = _cam.transform.position;
        pos.x += positionOffset.x;
        pos.y += positionOffset.y;
        pos.z += positionOffset.z;

        transform.position = pos;
    }
}
