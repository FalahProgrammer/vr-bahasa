using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;
using UnityEngine.Events;

public class GraspBehaviour : MonoBehaviour, iResetable
{
    [Header("The Target Object")] 
    public Transform _myTarget;

    [Header("Settings")] 
    [SerializeField] private Transform _myPlayer;

    //[SerializeField] private TeleportBehaviour _teleportBehaviour;
    
    //[SerializeField] private Booleanbehaviour _booleanbehaviour;
    
    [SerializeField] private ScriptableTransform _scriptableTransform;
    [Tooltip("Activate This Function?")] 
    public bool Active;
    [SerializeField] private float _raycastLenght;
    [SerializeField] private bool _collideWithMyLayerOnly;


    [Header("Event")] 
    [SerializeField] private UnityEvent OnHitObject;
    //[SerializeField] private UnityEvent OnHitNonGrasp;
    [SerializeField] private UnityEvent OnNotHitObject;
    //[SerializeField] private UnityEvent OnHitTeleport;

    //private GraspWithEvent _graspWithEvent;
    
    private Ray myRay { set; get; }
    public RaycastHit _myRaycastHit;
    /*private void Awake()
    {
        /*_graspWithEvent = new GraspWithEvent(
            _myTarget,
            _myTransform,
            _collideWithMyLayerOnly,
            _raycastLenght,
            scriptableListTransform,
            OnHitObject,
            OnHitNonGrasp,
            OnNotHitObject,
            OnHitTeleport,
            Active,
            _dotCursor);#1#
    }*/

    public void AcitvateMyRaycast()
    {
        Active = true;
        /*if (!_booleanbehaviour.isBoolean)
        {
            Active = true;
        }*/
    }
    
    public void DeacitvateMyRaycast()
    {
        Active = false;
        /*if (!_teleportBehaviour.isActive)
        {
            
        }*/
    }

    private void Update()
    {
        if (Active)
        {
            BeginRaycasting();

            //_myTarget = _graspWithEvent._myTarget;
        }
    }
    
    public void BeginRaycasting()
    {
        myRay = new Ray(_myPlayer.position, _myPlayer.forward);

        if (Active)
        {
            //_raycastLenght = raycastlenght;
            
            int myLayerMask = -1;

            if (_collideWithMyLayerOnly)
            {
                myLayerMask = 1 << _myPlayer.gameObject.layer;
            }

            if (Physics.Raycast(myRay, out _myRaycastHit, _raycastLenght, myLayerMask))
            {
                //Debug.Log("IN");

                //_raycastLenght = Vector3.Distance(_myRaycastHit.point, _myTransform.position);

                //didalam class tidak memanggil get component lagi, taruh di monobehaviour
                
                if (_myRaycastHit.collider)
                {
                    if (_myTarget != null && _myTarget.transform)
                    {
                        _scriptableTransform.MyTransform = _myTarget;
                        
                        OnHitObject.Invoke();
                        
                        //Debug.Log("Grasp Selected");
                        /*if (scriptableListTransform.MyTransforms.Contains(_myTarget.transform))
                        {
                            OnHitObject.Invoke();
                            Debug.Log("Grasp Selected");
                        }
                        if (_myTarget.transform.GetComponent<Outline>() != null)
                        {
                            if (_myTarget.transform.GetComponent<InitializeGrab>() != null)
                            {
                                if (scriptableListTransform.MyTransforms.Contains(_myTarget.transform))
                                {
                                    OnHitObject.Invoke();
                                    Debug.Log("Grasp Selected");
                                }
                            }
                            
                            //
                            else
                            {
                                if (scriptableListTransform.MyTransforms.Contains(_myTarget.transform))
                                {
                                    OnHitNonGrasp.Invoke();
                                    Debug.Log("Non Grasp Selected");
                                }
                            }
                        }
                        /*else if (_myTarget.transform.name == "TeleportPoint")
                        {
                            //do something
                        }#1#*/
                    }
                    
                    _myTarget = _myRaycastHit.transform;
                }
            }
            else
            {
                OnNotHitObject.Invoke();
                
                ClearTargetObject();
                
                //Debug.Log("OUT");
            }
        }
    }
    
    public void ClearTargetObject()
    {
        Reset();
    }

    public void Reset()
    {
        /*if (_myTarget == null)
            return;*/
        
        _myTarget = null;
    }
}