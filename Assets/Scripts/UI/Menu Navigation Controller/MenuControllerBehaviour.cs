using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class MenuControllerBehaviour : MonoBehaviour
{
    /*public ButtonModulHelper _buttonModulHelper;

    /*[Header("Button Object")]
    public List<GameObject> Module;#1#
    */

    [Header("Settings")]
    [Tooltip("Set your main menu canvas group")]
    [SerializeField] private CanvasGroup _myCanvasGroup;
    
    [Tooltip("Adjust your move speed / duration, e.g : 1 is faster than 10")]
    [SerializeField] private float _speed = 0.5f;

    [SerializeField] private CustomTimer _customTimer;

    public InteractionCanvasGroupCommand _interactionCanvasGroupCommand;
    
    [SerializeField] private ScriptableCanvasGroup _scriptableCanvasGroup;

    [SerializeField] private UnityEvent OnBeginFadeMenu;
    
    [SerializeField] private UnityEvent OnFadeMenuEnd;
    
/*    [SerializeField] private UnityEvent OnBeginFadeOutMenu;
    
    [SerializeField] private UnityEvent OnBeginFadeOutMenuEnd;*/
    
    private CanvasGroup _targetCanvasGroup;
    void Awake()
    {
        _interactionCanvasGroupCommand = new InteractionCanvasGroupCommand();
        
        //_scriptableCanvasGroup = Resources.Load<ScriptableCanvasGroup>("ScriptableObjects/Canvas Group/ScriptableCanvasGroup");

        _customTimer = FindObjectOfType<CustomTimer>();
    }

    public void SetMyButtonState(CanvasGroup targetCanvasGroup)
    {
        _scriptableCanvasGroup.CG = targetCanvasGroup;
    }

    /*public void ClickMyTargetMenu(CanvasGroup targetCanvasGroup)
    {
        _menuController.ClickMyTargetMenu(targetCanvasGroup);
    }

    public void PreviousMenu()
    {
        _menuController.PreviousMenu();
    }*/

    public void ClickMyTargetMenu(CanvasGroup targetCanvasGroup)
    {
        _targetCanvasGroup = targetCanvasGroup;

        HideMainMenu();

        OnBeginFadeMenu.Invoke();

        _customTimer.Wait(_speed, ()=>FadeOut(targetCanvasGroup));
        
    }
    
    public void HideMyMenu()
    {
        HideMainMenu();

    }

    void FadeOut(CanvasGroup targetCanvasGroup)
    {
        _interactionCanvasGroupCommand.DoAction(targetCanvasGroup,
            t => DoFade(1, targetCanvasGroup));
        

    }
    public void PreviousMenu()
    {
        _targetCanvasGroup = _myCanvasGroup;
        
        _customTimer.Wait(_speed,()=>WaitPrevMenu());
    }

    private void WaitPrevMenu()
    {
        _interactionCanvasGroupCommand.DoAction(_targetCanvasGroup,
            t => DoFade(0, _targetCanvasGroup));
        
        _interactionCanvasGroupCommand.DoAction(_targetCanvasGroup,
            t => _targetCanvasGroup.interactable = false);
        
        _interactionCanvasGroupCommand.DoAction(_targetCanvasGroup,
            t => _targetCanvasGroup.blocksRaycasts = false);

        _interactionCanvasGroupCommand.DoAction(_scriptableCanvasGroup.CG,
            t => DoFadeVariant(_scriptableCanvasGroup.CG,1));
        
        _interactionCanvasGroupCommand.DoAction(_scriptableCanvasGroup.CG,
            t => _scriptableCanvasGroup.CG.interactable = true);
        
        _interactionCanvasGroupCommand.DoAction(_scriptableCanvasGroup.CG,
            t => _scriptableCanvasGroup.CG.blocksRaycasts = true);
    }
    
    private void HideMainMenu()
    {
        _interactionCanvasGroupCommand.DoAction(_myCanvasGroup,
            t => DoFadeVariant(_myCanvasGroup,0));
        
        _interactionCanvasGroupCommand.DoAction(_myCanvasGroup,
            t => _myCanvasGroup.interactable = false);
        
        _interactionCanvasGroupCommand.DoAction(_myCanvasGroup,
            t => _myCanvasGroup.blocksRaycasts = false);
    }
    
    private void DoFade(float endValue, CanvasGroup targetCanvasGroup)
    {
        _targetCanvasGroup.DOFade(endValue, _speed).OnComplete(()=>OnCompleteFade(targetCanvasGroup));
        
    }

    private void DoFadeVariant(CanvasGroup myCanvasGroup, float endValue)
    {
        myCanvasGroup.DOFade(endValue, _speed).OnComplete(OnCompleteDoFadeVariant);
    }

    IEnumerator CoroutineOnCompleteDoFadeVariant()
    {
        yield return new WaitForSeconds(_speed);

        OnFadeMenuEnd.Invoke();
        
    }
    private void OnCompleteDoFadeVariant()
    {
        _customTimer.Wait(_speed,()=>OnFadeMenuEnd.Invoke());
        //StartCoroutine(CoroutineOnCompleteDoFadeVariant());
    }

    private void OnCompleteFade(CanvasGroup targetCanvasGroup)
    {
        if (targetCanvasGroup.alpha < 1)
        {
            _interactionCanvasGroupCommand.DoAction(targetCanvasGroup,
                t => targetCanvasGroup.interactable = false);
        
            _interactionCanvasGroupCommand.DoAction(targetCanvasGroup,
                t => targetCanvasGroup.blocksRaycasts = false);
        }
        else
        {
            _interactionCanvasGroupCommand.DoAction(targetCanvasGroup,
                t => targetCanvasGroup.interactable = true);
        
            _interactionCanvasGroupCommand.DoAction(targetCanvasGroup,
                t => targetCanvasGroup.blocksRaycasts = true);
        }
                
    }
}
