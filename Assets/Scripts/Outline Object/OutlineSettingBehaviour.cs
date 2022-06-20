using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class OutlineSettingBehaviour : MonoBehaviour
{
    [SerializeField] private GraspBehaviour graspBehaviour;
    [SerializeField] private ScriptableListTransform _scriptableListTransform;
    
    private Transform _activatedOutline;

    //private OutlineSetting _outlineSetting;
    /*private void Awake()
    {
        _outlineSetting = new OutlineSetting(_activatedOutline);
    }*/
    
    /*public void OnOutline()
    {
        _outlineSetting.OnOutline(graspBehaviour._myTarget);
    }

    public void OffOutline()
    {
        _outlineSetting.OffOutline();
    }

    public void ChangeOutlineColor()
    {
        _outlineSetting.ChangeColor();
    }*/
    
    public void OnOutline()
    {
        if (_activatedOutline !=null)
        {
            _activatedOutline.GetComponent<Outline>().enabled = false;
        }
        
        if (_scriptableListTransform.MyTransforms.Contains(graspBehaviour._myTarget.transform))
        {
            graspBehaviour._myTarget.GetComponent<Outline>().enabled = true;
            
            _activatedOutline = graspBehaviour._myTarget;
        }
    }

    public void OffOutline()
    {
        //Debug.Log(_activatedOutline.name);
        
        if (_activatedOutline != null)
        {
            _activatedOutline.GetComponent<Outline>().enabled = false;
        }
    }

    public void ChangeOutlineColor()
    {
        _activatedOutline.GetComponent<Outline>().color = 1;
    }
}
