using cakeslice;
using UnityEngine;
using UnityEngine.Events;


public class GraspWithEvent
{
    private Transform _dotcursor { get; }
    private bool _active { set; get; }
    public Transform _myTarget { set; get; }
    private Transform _myTransform { get; }
    private bool _collideWithMyLayerOnly { get; }
    private float _raycastLeght { set; get; }
    private ScriptableListTransform ScriptableListTransform { get; }
    private UnityEvent _onHitObject { get; }
    private UnityEvent _onNothitGrasp { get; }
    private UnityEvent _onNotHitObject { get; }
    private UnityEvent _onHitTeleport { get; }
    private Ray myRay { set; get; }

    private RaycastHit _myRaycastHit;

    public GraspWithEvent(Transform myTarget, Transform myTransform, bool collideWithMyLayerOnly, float raycastLeght,
        ScriptableListTransform scriptableListTransform, UnityEvent onHitObject,
        UnityEvent onNothitGrasp, UnityEvent onNotHitObject, UnityEvent onHitTeleport, bool active, Transform dotcursor)
    {
        _dotcursor = dotcursor;
        
        _active = active;

        _myTarget = myTarget;

        _myTransform = myTransform;

        _collideWithMyLayerOnly = collideWithMyLayerOnly;

        _raycastLeght = raycastLeght;

        ScriptableListTransform = scriptableListTransform;

        _onHitObject = onHitObject;

        _onNothitGrasp = onNothitGrasp;

        _onNotHitObject = onNotHitObject;

        _onHitTeleport = onHitTeleport;
    }

    public void BeginRaycasting(float raycastlenght)
    {
        myRay = new Ray(_myTransform.position, _myTransform.forward);

        
        
        if (_active)
        {
            _raycastLeght = raycastlenght;
            
            int myLayerMask = -1;

            if (_collideWithMyLayerOnly)
            {
                myLayerMask = 1 << _myTransform.gameObject.layer;
            }

            if (Physics.Raycast(myRay, out _myRaycastHit, _raycastLeght, myLayerMask))
            {
                Debug.Log("IN");

                _raycastLeght = Vector3.Distance(_myRaycastHit.point, _myTransform.position);

                //didalam class tidak memanggil get component lagi, taruh di monobehaviour
                
                if (_myRaycastHit.collider)
                {
                    if (_myTarget != null && _myTarget.transform)
                    {
                        if (_myTarget.transform.GetComponent<Outline>() != null)
                        {
                            if (_myTarget.transform.GetComponent<InitializeGrab>() != null)
                            {
                                if (ScriptableListTransform.MyTransforms.Contains(_myTarget.transform))
                                {
                                    _onHitObject.Invoke();
                                    Debug.Log("Grasp Selected");
                                }
                            }
                            
                            //
                            else
                            {
                                if (ScriptableListTransform.MyTransforms.Contains(_myTarget.transform))
                                {
                                    _onNothitGrasp.Invoke();
                                    Debug.Log("Non Grasp Selected");
                                }
                            }
                        }
                        /*else if (_myTarget.transform.name == "TeleportPoint")
                        {
                            //do something
                        }*/
                    }
                    
                    _myTarget = _myRaycastHit.transform;
                    
                    _dotcursor.position = _myRaycastHit.point;
                }
            }
            else
            {
                _onNotHitObject.Invoke();
                
                ClearTargetObject();
                
                Debug.Log("OUT");
            }
        }
    }

    void ClearTargetObject()
    {
        if (_myTarget == null)
            return;
        
        _myTarget = null;

        _dotcursor.localPosition = new Vector3(0f,0f,1f);
    }

    public bool ActivateRaycast()
    {
        _active = true;

        return _active;
    }

    public bool DeactivateRaycast()
    {
        _active = false;

        return _active;
    }
}