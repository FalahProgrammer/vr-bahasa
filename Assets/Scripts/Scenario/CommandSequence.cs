using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandSequence : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private IntegerVariable _integerVariable;
    public List<GameObject> _commandCarrier = new List<GameObject>();
    private Queue<ICommandValues> _commandSequences = new Queue<ICommandValues>();
    [SerializeField] private UnityEvent OnFinished;
    
    void Awake()
    {
        //ResetQueue();
        
        for (int i = 0; i < _commandCarrier.Count; i++)
        {
            var scenario = _commandCarrier[i].GetComponent<ICommandValues>();
            _commandSequences.Enqueue(scenario);
        }
    }
    public void Deserialize(string json)
    {
        
    }

    public void ResetQueue()
    {
        for (int i = 0; i < _commandCarrier.Count; i++)
        {
            var scenario = _commandCarrier[i].GetComponent<ICommandValues>();
            _commandSequences.Enqueue(scenario);
        }
    }
    //Debug
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Return)) StartNextCommand();
    }

    public void StartNextCommand()
    {
        if (_id == _integerVariable.IntegerValue)
        {
            if(_commandSequences.Count==0)
            {
                Debug.Log("Scenario finished");
                OnFinished?.Invoke();
                return;
            }
            var scenario = _commandSequences.Dequeue();

            scenario.Initialize();
            if (!scenario.IsWaitForUserInteraction)
            {
                scenario.Execute(StartNextCommand);
            }
        }
    }
    public void SubmitInteraction(GameObject gameObject)
    {
        DetermineCommandType(gameObject);
    }
    void DetermineCommandType(GameObject gameObject)
    {

    }
    void AnalyzeSelectInteraction(GameObject gameObject)
    {

    }

    
}

public class Command
{
    public string CommandType;
    public ICommandValues CorrectValue;
}


public class SelectValues :  MonoBehaviour, ICommandValues
{
    public string ObjectName;
    public StepType Type { get; set; }
    public bool IsWaitForUserInteraction { get; set; }

    public void Initialize()
    {
    }

    public void Execute(Action onFinished)
    {
    }
}


public interface ICommandValues
{
    StepType Type { get; set; }
    bool IsWaitForUserInteraction { get; set; }
    void Initialize();
    void Execute(Action onFinished);
}

public enum StepType
{
    SELECT,
    SPEECH
}