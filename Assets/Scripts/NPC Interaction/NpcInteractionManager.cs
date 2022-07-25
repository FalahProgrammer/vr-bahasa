using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;
using UnityEngine.Events;

public class NpcInteractionManager : MonoBehaviour
{
    //public List<UnityEvent> ListEventNpcInteractor;
    public IntegerVariable _integerVariable;

    /*[SerializeField] private PinchDetector _pinchDetector;

    [SerializeField] private ScriptController _scriptController;*/

    public GraspBehaviour _graspBehaviour;
    
    public List<NpcInteraction> _npcInteractions = new List<NpcInteraction>();

    public void CallNpcInteraction()
    {
        for (int i = 0; i < _npcInteractions.Count; i++)
        {
            _npcInteractions[i].BeginNPCInteraction();
        }
    }
    /*public void Reset()
    {
        for (int i = 0; i < _npcInteractions.Count; i++)
        {
            _npcInteractions[i]._integerVariable.IntegerValue = 0;
        }
    }*/

    public void ClearData()
    {
        _npcInteractions.Clear();
    }

    private void Awake()
    {
        /* 
        _scriptController = FindObjectOfType<ScriptController>();

       for (int i = 0; i < _npcInteractions.Count; i++)
        {
            _scriptController._npcInteractor.Add(_npcInteractions[i].gameObject);
        }
        */
        
        GameObject a = GameObject.Find("Interaction/Ray");

        _graspBehaviour = a.GetComponent<GraspBehaviour>();
    }

    private void Update()
    {
        if (_graspBehaviour._myTarget)
        {
            if (_graspBehaviour._myTarget.GetComponent<InitializeNpcInteraction>())
            {
                //Debug.Log( _graspBehaviour._myTarget.GetComponent<NpcInteraction>()._id);

                _integerVariable.IntegerValue =  _graspBehaviour._myTarget.GetComponent<NpcInteraction>()._id;
                
                //_graspBehaviour._myTarget.GetComponent<NpcInteraction>()._integerVariable.IntegerValue = _id;
            }
        }
    }

    public void EnableInteractors()
    {
        foreach (var v in _npcInteractions)
        {
            v.MeshRenderer.enabled = true;
        }
    }

    public void DisableInteractors()
    {
        foreach (var v in _npcInteractions)
        {
            v.MeshRenderer.enabled = false;
        }
    }
}
