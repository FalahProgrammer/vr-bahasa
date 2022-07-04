using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptController : MonoBehaviour, iResetable
{
    [Header("Integer Variable")] 
    [SerializeField] private IntegerVariable _integerVariable;
    
    [Header("List Interactor")] 
    [SerializeField] private ListInteractor _listInteractor;
    
    [Header("Game Object NPC Interactor")]
    private List<GameObject> _npcInteractor = new List<GameObject>();
    
    [Header("Menu Panel User")]
    [SerializeField] private MenuControllerBehaviour _menuPanelUser;

    [SerializeField] private CanvasGroup _menuPanelUserTargetMenu;


    [Header("Menu UI Movement")]
    [SerializeField] private DoMoveBehaviour _uiMovement;
    
    [SerializeField] private List<Transform> _setTargetLocationUIMovement;

    
    [Header("Menu Player Move")]
    [SerializeField] private DoMoveBehaviour _playerMove;

    [SerializeField] private List<Transform> _setTargetLocationPlayerMove;

    [SerializeField] private FadingBehaviour _playerFading; 
    

    [Header("Menu Panel Score")]
    [SerializeField] private MenuControllerBehaviour _menuPanelScore;

    [SerializeField] private CanvasGroup _menuPanelScoreTargetMenu;
    
    
    [Header("Ray Controller")]
    [SerializeField] private ToggleController _rayToggleController;
    
    //[SerializeField] private List<ToggleBehaviour> _laserToggleController = new List<ToggleBehaviour>();

    [Header("Answer Checker")]
    [SerializeField] private AnswerCheckerBehaviour _answerCheckerBehaviour;
    
    [SerializeField] private List<GraspBehaviour> _graspBehaviours = new List<GraspBehaviour>();

    [SerializeField] private Transform _target;
    
    private void UITransition()
    {
        _menuPanelUser.ClickMyTargetMenu(_menuPanelUserTargetMenu);
        
        _menuPanelScore.ClickMyTargetMenu(_menuPanelScoreTargetMenu);
    }

    public void UIMovement()
    {
        // dimaz revision
        int index = _integerVariable.IntegerValue - 1;
        for (int i = 0; i < _listInteractor.ListUIPosition.Count; i++)
        {
            _target.position = _listInteractor.ListUIPosition[index];
            _target.rotation =  Quaternion.Euler(_listInteractor.listCharacterRotation[index]);
            
            _uiMovement.SetTargetLocation(_target);
        }
        
        // original: 
        /*for (int i = 0; i < _setTargetLocationUIMovement.Count; i++)
        {
            _uiMovement.SetTargetLocation(_setTargetLocationUIMovement[_integerVariable.IntegerValue -1]);
        }*/
        
        _uiMovement.BeginDoLocalMove();
    }

    public void PlayerMove()
    {
        // dimaz revision
        int index = _integerVariable.IntegerValue - 1;
        for (int i = 0; i < _listInteractor.listCharacterPosition.Count; i++)
        {
            if (i == _integerVariable.IntegerValue - 1)
            {
                _target.position = _listInteractor.listCharacterPosition[index];
                // currently set in UI movement
                //_target.rotation =  Quaternion.Euler(_listInteractor.listCharacterRotation[index]);
                
                /*if (!vr)
                    _target.position.y += 1;*/

                _playerMove.SetTargetLocation(_target);
                
                //Debug.Log(_setTargetLocationPlayerMove[_integerVariable.IntegerValue - 1].name);   
                
                _playerFading.BeginFadingIn();
                
                break;
            }
        }
        
        /*for (int i = 0; i < _setTargetLocationPlayerMove.Count; i++)
        {
            if (i == _integerVariable.IntegerValue - 1)
            {
                _playerMove.SetTargetLocation(_setTargetLocationPlayerMove[_integerVariable.IntegerValue -1]);
                
                Debug.Log(_setTargetLocationPlayerMove[_integerVariable.IntegerValue - 1].name);   
                
                _playerFading.BeginFadingIn();
                
                break;
            }
        }*/
    }

    public void PlayerReturnToInitialPosition()
    {
        _target.position = _listInteractor.homeVector;
        _playerMove.SetTargetLocation(_target);
        _playerFading.BeginFadingIn();
    }

    public void EnableNPCInteractor()
    {
        for (int i = 0; i < _npcInteractor.Count; i++)
        {
            _npcInteractor[i].SetActive(true);
        }
    }
    
    
    public void Reset()
    {
        for (int i = 0; i < _graspBehaviours.Count; i++)
        {
            _graspBehaviours[i].Reset();
        }
        
        /*for (int i = 0; i < _laserToggleController.Count; i++)
        {
            _laserToggleController[i].SetToggle();
        }*/

        for (int i = 0; i < _npcInteractor.Count; i++)
        {
            _npcInteractor[i].SetActive(false);
        }
        
        UIMovement();
        
        UITransition();

        PlayerMove();
        
        _menuPanelScore.transform.gameObject.SetActive(false);
        
        _rayToggleController.SetToggleController();
        
        _answerCheckerBehaviour.Reset();
        
        
    }
}
