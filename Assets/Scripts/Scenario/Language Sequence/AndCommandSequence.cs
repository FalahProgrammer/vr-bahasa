using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AndCommandSequence : MonoBehaviour, ICommandValue
{
    public List<CustomDictionary> GameObjects = new List<CustomDictionary>();
    public CommandSequences Commands;
    public bool LastAnimArrayIsLooping;
    private int _lastTrueSelectIndex;
    [SerializeField] private bool _isCustomDictHasBackedValue;
    [SerializeField] private UnityEvent OnAlreadyTrue;
    private void Awake()
    {
        foreach (var key in GameObjects)
        {
            key.Start();
        }
        /*Buffer = new List<CustomDictionary>();
        foreach (var VARIABLE in GameObjects)
        {
            Buffer.Add(VARIABLE);
        }*/
    }

    public bool UpdateValue(GameObject gameObject)
    {
        Check(gameObject);
        return true;
    }

    public bool UpdateKey(GameObject param)
    {
        throw new NotImplementedException();
    }

    public bool UpdateValue()
    {
        throw new NotImplementedException();
    }

    public bool UpdateKey()
    {
        throw new NotImplementedException();
    }

    void Check(GameObject gameObject)
    {
        //Updating the list
        for(int i=0; i<GameObjects.Count;i++)
        {     
            if (ObjectIsSimilar(gameObject,i) && ObjectNotYetClicked(i))
            {
                if(GameObjects[i].Anim!=null)
                    GameObjects[i].Anim.PlayAnimation();
                _lastTrueSelectIndex = i;
                GameObjects[i].ChangeValue("true");
            }
            else
            {
                
            }
        }
        
        //Checking if all the list is true
        var a = (from o in GameObjects
            where o.GetValue() == "true"
            select o).ToList();
        if (a.Count == GameObjects.Count)
        {
            if (LastAnimArrayIsLooping)
            {
                if (GameObjects.Count >= _lastTrueSelectIndex)
                {
                    Commands.MoveNext();
                }
                else
                {
                    if (_lastTrueSelectIndex >= GameObjects.Count)
                        _lastTrueSelectIndex = GameObjects.Count - 1;
                    //Delete the last array of the animation
                    GameObjects[_lastTrueSelectIndex].Anim.DeleteLastAnim();
                }
            }
            else
            {
                Commands.MoveNext();
            }
            return;
            
        }
        else
        {

        }
    }

    private bool ObjectIsSimilar(GameObject source, int interator)
    {
        if (source.name == GameObjects[interator].GetKey())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ObjectNotYetClicked(int iterator)
    {
        if (GameObjects[iterator].GetValue() == "false")
        {
            return true;
        }
        else
        {
            OnAlreadyTrue?.Invoke();
            return false;
        }
    }
    public void Reset()
    {
        _lastTrueSelectIndex = 0;
        //GameObjects.Clear();
        foreach (var key in GameObjects)
        {
            if (_isCustomDictHasBackedValue)
            {
                key.ResetWithBackedValue();
            }else
            {
                key.Reset();
            }
            
        }

    }
}
