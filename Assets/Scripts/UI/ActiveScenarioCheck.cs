using UnityEngine;
using UnityEngine.Events;

public class ActiveScenarioCheck : MonoBehaviour
{
    [SerializeField] private ScenarioManager _scenarioManager;

    public UnityEvent OnScenarioIsActive;
    public UnityEvent OnScenarioisInactive;

    public void CheckScenario()
    {
        switch (_scenarioManager.ScenarioIsActive)
        {
            case true:
                OnScenarioIsActive?.Invoke();
                break;
            case false:
                OnScenarioisInactive?.Invoke();
                break;
        }
    }
}
