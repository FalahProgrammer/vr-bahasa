using System;
using DG.Tweening;
using UnityEngine;

public class MenuController
{
    //taruh di monobehaviour (sudah)
    private InteractionCanvasGroupCommand _interactionCanvasGroupCommand;
    private CanvasGroup _myCanvasGroup;
    private float _speed;
    private ScriptableCanvasGroup _scriptableCanvasGroup;
    private CanvasGroup _targetCanvasGroup;
    public MenuController(InteractionCanvasGroupCommand interactionCanvasGroupCommand, CanvasGroup myCanvasGroup, float speed, ScriptableCanvasGroup scriptableCanvasGroup)
    {
        _interactionCanvasGroupCommand = interactionCanvasGroupCommand;
        _myCanvasGroup = myCanvasGroup;
        _speed = speed;
        _scriptableCanvasGroup = scriptableCanvasGroup;
    }
    public void ClickMyTargetMenu(CanvasGroup targetCanvasGroup)
    {
        //ButtonManager._animatorMenuMask.BorderHide();

/*        menuControllerBehaviour._buttonModulHelper.DoGameObject(transform1 =>
            MenuControllerBehaviour.InteractionCommand.DoAction(menuControllerBehaviour.Module[0], t => t.SetActive(false)));
        menuControllerBehaviour._buttonModulHelper.DoGameObject(transform1 =>
            MenuControllerBehaviour.InteractionCommand.DoAction(menuControllerBehaviour.Module[9], t => t.SetActive(true)));*/
        _targetCanvasGroup = targetCanvasGroup;


        HideMainMenu();
        
        _interactionCanvasGroupCommand.DoAction(targetCanvasGroup,
                t => DoFade(1));
        
        _interactionCanvasGroupCommand.DoAction(targetCanvasGroup,
                t => targetCanvasGroup.interactable = true);
        
        _interactionCanvasGroupCommand.DoAction(targetCanvasGroup,
                t => targetCanvasGroup.blocksRaycasts = true);
    }

    public void PreviousMenu()
    {
        _targetCanvasGroup = _myCanvasGroup;
        
        _interactionCanvasGroupCommand.DoAction(_targetCanvasGroup,
            t => DoFade(0));
        
        _interactionCanvasGroupCommand.DoAction(_targetCanvasGroup,
            t => _targetCanvasGroup.interactable = false);
        
        _interactionCanvasGroupCommand.DoAction(_targetCanvasGroup,
            t => _targetCanvasGroup.blocksRaycasts = false);

        //ShowPreviousMenu();
        
        _interactionCanvasGroupCommand.DoAction(_scriptableCanvasGroup.CG,
            t => DoFadeVariant(_scriptableCanvasGroup.CG,1));
        
        _interactionCanvasGroupCommand.DoAction(_scriptableCanvasGroup.CG,
            t => _scriptableCanvasGroup.CG.interactable = true);
        
        _interactionCanvasGroupCommand.DoAction(_scriptableCanvasGroup.CG,
            t => _scriptableCanvasGroup.CG.blocksRaycasts = true);
    }
    
    private void ShowPreviousMenu()
    {
        
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


    public void DoMove()
    {
        throw new NotImplementedException();
    }

    public void DoLocalMove()
    {
        throw new NotImplementedException();
    }

    public void DoSelectMove(float myTarget)
    {
        throw new NotImplementedException();
    }

    public void DoSelectLocalMove(float myTarget)
    {
        throw new NotImplementedException();
    }

    public void DoLocalRotate()
    {
        throw new NotImplementedException();
    }

    public void DoLocalRotateQuaternion()
    {
        throw new NotImplementedException();
    }

    public void DoFade(float endValue)
    {
        _targetCanvasGroup.DOFade(endValue, _speed);
    }

    private void DoFadeVariant(CanvasGroup myCanvasGroup, float endValue)
    {
        myCanvasGroup.DOFade(endValue, _speed);
    }
}