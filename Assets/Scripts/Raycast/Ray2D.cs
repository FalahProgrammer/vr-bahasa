using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ray2D : MonoBehaviour
{
    [Header("The Target Object")] 
    public Transform _myTarget;

    [Header("Settings")] 
    [SerializeField] private Transform _myPlayer;
    
    [SerializeField] private ScriptableTransform _scriptableTransform;
    [Tooltip("Activate This Function?")] 
    [SerializeField] private bool Active;
    [SerializeField] private float _raycastLenght;
    [SerializeField] private bool _collideWithMyLayerOnly;


    [Header("Event")] 
    [SerializeField] private UnityEvent OnHitObject;
    //[SerializeField] private UnityEvent OnHitNonGrasp;
    [SerializeField] private UnityEvent OnNotHitObject;
    //[SerializeField] private UnityEvent OnHitTeleport;

    //private GraspWithEvent _graspWithEvent;
    
    private Ray myRay { set; get; }
    //public RaycastHit _myRaycastHit;
    private RaycastHit2D[] _raycastHitArray;
    
    private List<Collider2D> _colliderList = new List<Collider2D>();
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
    }
    
    public void DeacitvateMyRaycast()
    {
        Active = false;
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

            //_raycastHitArray = Physics2D.RaycastAll(transform.position, _myPlayer.forward);


            //List<RaycastResult> raycastResult = new List<RaycastResult>();

            //RaycastHit[] raycastHitArray = Physics.RaycastAll(_myPlayer.position,_myPlayer.forward);
            
            List<RaycastResult> results = new List<RaycastResult>();
            /*EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            
            foreach (RaycastHit result in raycastHitArray)
            {
                Debug.Log("Button Name : " + result.transform.name);
                _myTarget = result.transform;
            }*/

            /*List<RaycastResult> results = new List<RaycastResult>();
            Physics.RaycastAll(pointerData, results);

            foreach (RaycastResult raycastResult in results)
            {
               Debug.Log(raycastResult.gameObject.name); 
            }*/
            /*foreach (RaycastHit2D raycastHit in _raycastHitArray)
            {
               
                if (raycastHit.collider != null)
                {
                    //Hit Something
                    if (!_colliderList.Contains(raycastHit.collider))
                    {
                        Debug.Log("RAY UI");
                        _myTarget = raycastHit.transform;
                    }
                }
                else
                {
                    ClearTargetObject();
                }
            }*/
        }
    }
    
    void ClearTargetObject()
    {
        if (_myTarget == null)
            return;
        
        _myTarget = null;

        
    }
}
