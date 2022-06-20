using System;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerHandlerBehaviour : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{
    private ScriptableTransform _scriptableTransform;

    public UnityEvent OnPointerEnterEvent;
    
    public UnityEvent OnPointerClickEvent;
    
    public UnityEvent OnPointerExitEvent;

    private void Start()
    {
        _scriptableTransform = Resources.Load<ScriptableTransform>("ScriptableObjects/Transform/ScriptableTransform");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _scriptableTransform.MyTransform = eventData.pointerCurrentRaycast.gameObject.transform;
        
        OnPointerEnterEvent.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent.Invoke();
        
        _scriptableTransform.MyTransform = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent.Invoke();
    }
}
