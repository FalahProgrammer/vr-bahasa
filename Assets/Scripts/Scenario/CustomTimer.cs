using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimer : MonoBehaviour
{
    public void Wait(float duration, Action then)
    {
        StartCoroutine(WaitCoroutine(duration, then));
    }
    private IEnumerator WaitCoroutine(float duration, Action then)
    {
        yield return new WaitForSeconds(duration);
        then?.Invoke();
    }
}
