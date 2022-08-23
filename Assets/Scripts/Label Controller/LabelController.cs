using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LabelController : MonoBehaviour
{
    [SerializeField] private GraspBehaviour _rayGraspBehaviour;
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _labelRectTransform;
    
    [Space(10)]
    [SerializeField] private FloatVariable labelSliderValue;
    [SerializeField] private float defaultScale = 0.02f;
    
    private Vector3 _labelScale = new Vector3(0.01f, 0.01f, 0.01f);

    [Space(15)]
    public UnityEvent OnSelectingGrabableObjectWithLabel;
    public UnityEvent OnNotSelectingGrabableObjectWithLabel;
    [Space(10)]
    public UnityEvent OnLabelizeChanged;

    public Transform target;

    
    
    private void OnEnable()
    {
        _slider.value = labelSliderValue.floatValue;
    }

    private void OnDisable()
    {
        labelSliderValue.floatValue = _slider.value;
    }

    public void CheckForLabel(bool status)
    {
        if (status)
        {
            Transform tempTarget = _rayGraspBehaviour._myTarget;
        
            if (tempTarget.CompareTag("Grabable"))
            {
                if (target != tempTarget)
                {
                    target = tempTarget;
                    OnSelectingGrabableObjectWithLabel?.Invoke();
                }
                else
                {
                    // target = null;
                    // OnNotSelectingGrabableObjectWithLabel?.Invoke();
                }
            }
            else
            {
                target = null;
                OnNotSelectingGrabableObjectWithLabel?.Invoke();
            }
        }
        else
        {
            target = null;
            OnNotSelectingGrabableObjectWithLabel?.Invoke();
        }
    }

    public void SliderChanged()
    {
        ChangeLabelSize();
        OnLabelizeChanged?.Invoke();
    }

    public void ChangeLabelSize()
    {
        float v = _slider.value * defaultScale;
        _labelScale.x = v;
        _labelScale.y = v;
        _labelScale.z = v;

        _labelRectTransform.localScale = _labelScale;
    }
}
