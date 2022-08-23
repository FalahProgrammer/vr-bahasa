using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelNameBehaviour : MonoBehaviour
{
    [SerializeField] private GraspBehaviour graspBehaviour;
    
    [SerializeField] private RectTransform _label;
    [SerializeField] private RectTransform _panel;
    [SerializeField] private Vector2 _padding = new Vector2(20,20);
    [SerializeField] private float sizePerLetter = 2;
    
    [SerializeField] private Text _myTextName;
    [SerializeField] private InitialPosRotBehaviour _initialPosRotBehaviour;

    public void SetName()
    {
        if (graspBehaviour._myTarget != null)
        {
            var initGrab = graspBehaviour._myTarget.GetComponent<InitializeGrab>();
            
            var initTeleport = graspBehaviour._myTarget.GetComponent<InitializeTeleport>();

            if (initGrab)
            {
                _myTextName.text = graspBehaviour._myTarget.name;
            }
            else if (initTeleport)
            {
                _myTextName.text = graspBehaviour._myTarget.name;
            }


            if (_panel != null)
            {
                float x1 = _myTextName.text.Length * sizePerLetter + _padding.x;
                _panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x1);
                _label.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x1);
                _initialPosRotBehaviour.GetLocalPos.x = -x1;
                
                _panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _myTextName.preferredHeight + _padding.y);

                _panel.anchoredPosition = new Vector2(- _panel.sizeDelta.x, _panel.anchoredPosition.y);
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        var initGrab = other.GetComponent<InitializeGrab>();
        
        if (initGrab)
        {
            _myTextName.text = other.gameObject.name;
        }
       
        
    }
}