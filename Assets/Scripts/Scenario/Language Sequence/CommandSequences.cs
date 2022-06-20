using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandSequences : MonoBehaviour
{
    private List<ICommandValue> _commandSequences = new List<ICommandValue>();
    private int _currentIteration;
    private bool _alreadyFinished;
    private bool _onResetCooldown;
    [SerializeField] private List<Commands> ListOfCommand = new List<Commands>();
    [SerializeField] private SequentialAnimation Anim;
    [SerializeField] private float WaitBeforeInvokeFinish;
    [SerializeField] private UnityEvent OnFinished;

    public const float reset_cooldown = 2.0f;

    void Awake()
    {
        Initialize();
    }
    public void Deserialize(string json)
    {
        
    }

    public void MoveNext()
    {
        if (_currentIteration >= _commandSequences.Count)
        {
            if (_alreadyFinished)
                return;
            Finished();
        }
        
        _currentIteration++;
        if (_currentIteration >= _commandSequences.Count)
        {
            if (WaitBeforeInvokeFinish > 0)
            {
                StartCoroutine(Waiting());
            }
            else
            {
                Finished();
            }
        }
        else
        {
        }
    }

    private void Finished()
    {
        OnFinished?.Invoke();
        _alreadyFinished = true;  
    }

    public bool InitializeList(List<Commands> list)
    {
        if (list == null) return false;

        ListOfCommand = list;
        return true;
    }
    private void Initialize()
    {
        if (ListOfCommand.Count == 0) return;
        
        for (int i = 0; i < ListOfCommand.Count; i++)
        {
            ICommandValue a = null;
            switch (ListOfCommand[i].TypeOfCommand)
            {
                case Commands.CommandType.AND:
                    a = ListOfCommand[i].CommandObject.GetComponent<AndCommandSequence>();
                    break;
                case Commands.CommandType.SELECT:
                    a = ListOfCommand[i].CommandObject.GetComponent<SelectValue>();
                    break;
                case Commands.CommandType.DRAG:
                    a = ListOfCommand[i].CommandObject.GetComponent<DragValue>();
                    break;
                case Commands.CommandType.WAIT:
                    a = ListOfCommand[i].CommandObject.GetComponent<WaitCommand>();
                    break;
                case Commands.CommandType.ROTATE:
                    a = ListOfCommand[i].CommandObject.GetComponent<RotateValue>();
                    break;
                case Commands.CommandType.TRANSFORM:
                    a = ListOfCommand[i].CommandObject.GetComponent<TransformValue>();
                    break;
                default:
                    throw new Exception("No Command added");
            }
            _commandSequences?.Add(a);
        }
    }

    public void SubmitInteraction(GameObject gameObject)
    {
        if (!CheckCollection() || _currentIteration>=_commandSequences.Count || _onResetCooldown) return;
        _commandSequences[_currentIteration].UpdateValue(gameObject);
    }
    public void SubmitInteraction()
    {
        if (!CheckCollection() || _currentIteration>=_commandSequences.Count || _onResetCooldown) return;
        _commandSequences[_currentIteration].UpdateValue();
    }

    public void SubmitInteractionKey(GameObject gameObject)
    {
        if (!CheckCollection() || _currentIteration>=_commandSequences.Count || _onResetCooldown) return;
        _commandSequences[_currentIteration].UpdateKey(gameObject);
    }

    public void SubmitInteractionKey()
    {
        if (!CheckCollection() || _currentIteration>=_commandSequences.Count || _onResetCooldown) return;
        _commandSequences[_currentIteration].UpdateKey();
    }

    private bool CheckCollection()
    {
        if (ListOfCommand.Count == 0) 
            return false;
        if(_commandSequences==null || _commandSequences.Count==0)
            Initialize();
        return true;
    }
    public void ResetInteraction()
    {
        StartCoroutine(Cooldown(reset_cooldown));
        foreach (var command in _commandSequences)
        {
            command.Reset();
        }
        _currentIteration = 0;
        _alreadyFinished = false;
    }

    IEnumerator Cooldown(float sec)
    {
        _onResetCooldown = true;
        yield return new WaitForSeconds(sec);
        _onResetCooldown = false;
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(WaitBeforeInvokeFinish);
        OnFinished?.Invoke();
        _alreadyFinished = true;  
    }
}
[Serializable]
public class Commands
{
    [Serializable]
    public enum CommandType
    {
        SELECT,
        AND,
        WAIT,
        DRAG,
        ROTATE,
        TRANSFORM
    }
    
    public CommandType TypeOfCommand;
    public GameObject CommandObject;
    public ICommandValue CorrectValue;
}

public class WaitCommand : ICommandValue
{
    public bool UpdateValue(GameObject param)
    {
        throw new NotImplementedException();
    }

    public bool UpdateKey(GameObject param)
    {
        return false;
    }

    public bool UpdateValue()
    {
        return false;
    }

    public bool UpdateKey()
    {
        return false;
    }
    public void Reset()
    {
        
    }
}
public class SelectValue : ICommandValue
{
    public string ObjectName;
    public bool UpdateValue(GameObject param)
    {
        if (param.name == ObjectName)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateKey(GameObject param)
    {
        return false;
    }

    public bool UpdateValue()
    {
        return false;
    }

    public bool UpdateKey()
    {
        return false;
    }
    public void Reset()
    {
        
    }
}

public class DragValue : ICommandValue
{
    public string ObjectName;
    public Collider Area;
    public bool UpdateValue(GameObject param)
    {
        return false;
    }

    public bool UpdateKey(GameObject param)
    {
        return false;
    }

    public bool UpdateValue()
    {
        return false;
    }

    public bool UpdateKey()
    {
        return false;
    }
    public void Reset()
    {
        
    }
}


public class MaterialValue : ICommandValue
{
    public Material CorrectMaterial;
    public bool UpdateValue(GameObject param)
    {
        return false;
    }

    public bool UpdateKey(GameObject param)
    {
        return false;
    }

    public bool UpdateValue()
    {
        return false;
    }

    public bool UpdateKey()
    {
        return false;
    }
    public void Reset()
    {
        
    }
}

public class PositionValue : ICommandValue
{
    public Vector3 CorrectPosition;
    public float Tolerance;
    public bool UpdateValue(GameObject param)
    {
        return false;
    }

    public bool UpdateKey(GameObject param)
    {
        return false;
    }

    public bool UpdateValue()
    {
        return false;
    }

    public bool UpdateKey()
    {
        return false;
    }
    public void Reset()
    {
        
    }
}

public class RotateValue : ICommandValue
{
    public Vector3 CorrectRotation;
    public float Tolerance;
    public bool UpdateValue(GameObject param)
    {
        return false;
    }

    public bool UpdateKey(GameObject param)
    {
        return false;
    }

    public bool UpdateValue()
    {
        return false;
    }

    public bool UpdateKey()
    {
        return false;
    }
    public void Reset()
    {
        
    }
}

public class TransformValue : ICommandValue
{
    public Vector3 CorrectPosition;
    public Vector3 CorrectRotation;
    public float Tolerance;
    public bool UpdateValue(GameObject param)
    {
        return false;
    }

    public bool UpdateKey(GameObject param)
    {
        return false;
    }

    public bool UpdateValue()
    {
        return false;
    }

    public bool UpdateKey()
    {
        return false;
    }

    public void Reset()
    {
        
    }
}

public interface ICommandValue
{
    bool UpdateValue(GameObject param);
    bool UpdateKey(GameObject param);
    bool UpdateValue();
    bool UpdateKey();
    void Reset();
}
[Serializable]
public class CustomDictionary: ICloneable
{
    [SerializeField]private string Key;
    [SerializeField]private string Value;
    private string _backedValue;
    public SequentialAnimation Anim;

    public CustomDictionary(string key, string value,SequentialAnimation anim)
    {
        Key = key;
        Value = value;
        Anim = anim;
    }
    
    public void Start()
    {
        _backedValue = Value;
    }

    public void Reset()
    {
        //Value = _backedValue;
    }

    public void ResetWithBackedValue()
    {
        Value = _backedValue;
    }
    public void ChangeValue(string arg)
    {
        Value = arg;
    }

    public string GetValue()
    {
        return Value;
    }
    public string GetKey()
    {
        return Key;
    }

    public object Clone()
    {
        return new CustomDictionary(Key,Value,Anim);
    }
}



