using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelNameBehaviour : MonoBehaviour
{
    [SerializeField] private GraspBehaviour graspBehaviour;

    [SerializeField] private RectTransform _panel;
    [SerializeField] private Vector2 _padding = new Vector2(20,20);
    
    [SerializeField] private Text _myTextName;

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
                _panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _myTextName.preferredHeight + _padding.x);
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