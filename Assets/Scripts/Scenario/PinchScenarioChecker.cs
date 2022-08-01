using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinchScenarioChecker : MonoBehaviour
{
    [SerializeField] private ScenarioManager _scenarioManager;

    public UnityEvent OnActiveScenario;
    
    public UnityEvent OnInactiveScenario;

    public void CheckForActiveScenario()
    {
        switch (_scenarioManager.ScenarioIsActive)
        {
            case true:
                OnActiveScenario?.Invoke();
                break;
            case false:
                OnInactiveScenario?.Invoke();
                break;
        }
    }
}
