using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmmountLoading : MonoBehaviour
{
    
    [SerializeField] private Image[] _cooldown;
    [SerializeField] private TimerBehaviour[] _timeBehaviour;
    
    public void StartFill()
    {
        StartCoroutine(Fill());
    }
    
    IEnumerator Fill()
    {
        while (true)
        {
            _cooldown[0].fillAmount = (_timeBehaviour[0]._initialDuration - _timeBehaviour[0]._currentDuration) / _timeBehaviour[0]._initialDuration;
            //_cooldown[1].fillAmount = (_timeBehaviour[1]._initialDuration - _timeBehaviour[1]._currentDuration) / _timeBehaviour[1]._initialDuration;
            yield return null;
        }
    }
}
