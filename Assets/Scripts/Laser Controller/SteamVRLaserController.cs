using System;
using UnityEngine;
using Valve.VR;

public class SteamVRLaserController : MonoBehaviour
{
    [SerializeField] private GraspBehaviour _graspBehaviour;
    
    [SerializeField] private Booleanbehaviour _booleanbehaviour;

    [SerializeField] private Transform _laserPrefab;
    
    [SerializeField] private Transform _laserParent;

    [SerializeField] private Transform _dotCursor;
    
    [SerializeField] private bool _isActive = true;
    
    [SerializeField] private Vector3 _offset;

    private Transform _laserTransform;
    
    private Transform _dotCursorTransform;
    
    private Vector3 HitPoint;
    private void Start()
    {
        //_graspBehaviour = FindObjectOfType<GraspBehaviour>();
        
        _laserTransform = Instantiate(_laserPrefab,_laserParent);
        
        _dotCursorTransform = Instantiate(_dotCursor,_laserParent);
    }

    public void ShowLaser()
    {
        if (_isActive)
        {
            /*if (_graspBehaviour._myTarget.GetComponent<InitializeNpcInteraction>())
            {
                HitPoint = _graspBehaviour._myRaycastHit.point;
        
                _dotCursor.position = _graspBehaviour._myRaycastHit.point;
            
                _dotCursor.gameObject.SetActive(true);
            
                _laserPrefab.gameObject.SetActive(true);

                _laserPrefab.position = Vector3.Lerp(new Vector3(_laserParent.transform.position.x + _offset.x, _laserParent.transform.position.y + _offset.y ,_laserParent.transform.position.z + _offset.z), HitPoint, .5f);

                _laserPrefab.LookAt(HitPoint);

                _laserPrefab.localScale = new Vector3(_laserPrefab.localScale.x,
                    _laserPrefab.localScale.y,
                    _graspBehaviour._myRaycastHit.distance);
            }*/

            if (_graspBehaviour._myTarget)
            {
                if (_graspBehaviour._myTarget.GetComponent<InitializeGrab>())
                {
                    HitPoint = _graspBehaviour._myRaycastHit.point;
        
                    _dotCursorTransform.position = _graspBehaviour._myRaycastHit.point;
            
                    _dotCursorTransform.gameObject.SetActive(true);
            
                    _laserPrefab.gameObject.SetActive(true);

                    //_laserPrefab.position = Vector3.Lerp(new Vector3(_laserParent.transform.position.x + _offset.x, _laserParent.transform.position.y + _offset.y ,_laserParent.transform.position.z + _offset.z), HitPoint, .5f);

                    //_laserPrefab.LookAt(HitPoint);

                    //_laserPrefab.localScale = new Vector3(_laserPrefab.localScale.x, _laserPrefab.localScale.y, _graspBehaviour._myRaycastHit.distance);
                }
            }
            else
            {
                //_laserPrefab.localPosition = new Vector3(0, 0, 0.0375f);
                //_laserPrefab.localScale = new Vector3(_laserPrefab.localScale.x,_laserPrefab.localScale.y,0.1f);
                _dotCursorTransform.gameObject.SetActive(false);
            }
        }
        else
        {
            //HideLaser();
            _laserPrefab.gameObject.SetActive(true);
            //_laserPrefab.localPosition = new Vector3(0, 0, 0.0375f);
            //_laserPrefab.localScale = new Vector3(_laserPrefab.localScale.x,_laserPrefab.localScale.y,0.1f);
            _dotCursorTransform.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        ShowLaser();
        
        if (_graspBehaviour.Active)
        {
            ActivateLaser();
            
        }
        else
        {
            HideLaser();
        }
    }

    public void ActivateLaser()
    {
        if (!_booleanbehaviour.isBoolean)
        {
            if (!_isActive)
            {
                _isActive = true;
        
                _dotCursorTransform.gameObject.SetActive(true);
            
                _laserTransform.gameObject.SetActive(true);
            }
        }
    }

    public void HideLaser()
    {
        _isActive = false;

        _laserTransform.gameObject.SetActive(false);
        
        _dotCursorTransform.gameObject.SetActive(false);
    }
}
