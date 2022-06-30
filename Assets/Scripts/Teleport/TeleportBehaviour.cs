using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TeleportBehaviour : MonoBehaviour, iResetable
{
    [SerializeField] private Transform _myPlayer;
    
    [SerializeField] private GraspBehaviour _graspBehaviour;

    //[SerializeField] private ToggleBehaviour _toggleBehaviour;
    
    [SerializeField] private Booleanbehaviour _booleanbehaviour;

    public Transform _teleportPointer;
    
    public GameObject LinePrefabs;
        
    public Color LineColor;

    public bool isActive;
    
    private bool startTeleport;

    [SerializeField, Range(0,10)] private float _speed;
    
    [Range(0,20)] public float Density = 12;
    
    [Range(0,10)] public float CurveHeight = 2;

    [SerializeField] private Vector3 _teleportPivot;
        
    //[SerializeField] private UnityEvent OnBeginPinchingTeleport;
    
    //[SerializeField] private UnityEvent OnCompleteTeleport;
    
    private LineRenderer _lineRenderer;
    
    private Transform Point2;
    
    public Transform _teleportPoint;

    private GameObject _teleportDirection;

    private Vector3 _savedTeleportPosition;

    private Transform _savedTeleportObject;

    private Teleport _teleport;

    public TeleportEvents teleportEvents;
    
    public delegate void TeleportEventHandler();
    
    private void Start()
    {
        Init();
    }

    void Init()
    {
        GameObject drawing = Instantiate(LinePrefabs);
        
        _lineRenderer = drawing.GetComponent<LineRenderer>();

        Point2 = _lineRenderer.transform.GetChild(0);
        
        _teleportPoint = _lineRenderer.transform.GetChild(1);
        
        _lineRenderer.startColor = LineColor;
        
        _teleportDirection = GameObject.Find("Teleport Direction");
    }

    public void ActivateTeleport()
    {
        if (!_booleanbehaviour.isBoolean)
        {
            if (!isActive)
            {
                //if (_graspBehaviour._myTarget)
                {
                    //if (_graspBehaviour.Active)
                    {
                        try
                        {
                            Debug.Log(_savedTeleportObject);
                            Debug.Log(_graspBehaviour._myTarget.GetComponent<InitializeTeleport>());
                            if (_graspBehaviour._myTarget.GetComponent<InitializeTeleport>())
                            {
                                ShowTeleport();
            
                                isActive = true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
        }
    }
    
    public void Reset()
    {
        isActive = false;
    }
    
    public void ShowTeleport()
    {
        _lineRenderer.enabled = true;
        
        _teleportPoint.gameObject.SetActive(true);
    }

    public void HideTelepfort()
    {
        _lineRenderer.enabled = false;
        _teleportPoint.gameObject.SetActive(false);
    }

    private void Update()
    {
        _teleport = new Teleport(
            lineRenderer: _lineRenderer, 
            teleportDirection: _teleportDirection,
            myPlayer: _myPlayer,
            teleportPoint: _teleportPoint,
            point2: Point2,
            teleportPointer: _teleportPointer,
            speed: _speed,
            curveHeight: CurveHeight,
            density: Density,
            savedTeleportPosition: _savedTeleportPosition,
            _teleportPivot: _teleportPivot,
            teleportEvent: teleportEvents.OnCompleteTeleport);
        
        if (_graspBehaviour._myTarget)
        {
            _savedTeleportPosition = new Vector3(_graspBehaviour._myRaycastHit.point.x,_graspBehaviour._myRaycastHit.point.y,_graspBehaviour._myRaycastHit.point.z);

            _savedTeleportObject = _graspBehaviour._myTarget;
            
            if (_graspBehaviour._myTarget.GetComponent<InitializeGrab>())
            {
                Reset();
            }
        }

        

        if (isActive)
        {
            if (_savedTeleportObject)
            {
                var initTeleport = _savedTeleportObject.GetComponent<InitializeTeleport>();
            
                if (initTeleport)
                {
                    if (initTeleport._canTeleport)
                    {
                        _teleportPoint.gameObject.SetActive(true);
                        
                        _lineRenderer.gameObject.SetActive(true);

                        teleportEvents.OnTeleportActivated.Invoke();
                        
                        _teleport.CalculateLine();
                    }
                }
                else
                {
                    Debug.Log("Please Select The Teleport Point !!!");
            
                    _lineRenderer.gameObject.SetActive(false);
                    
                    teleportEvents.OnTeleportDeactivated.Invoke();
                }
            }
            
        }
        else
        {
            _lineRenderer.gameObject.SetActive(false);
            
            teleportEvents.OnTeleportDeactivated.Invoke();
        }
        

    }

    public void BeginTeleport()
    {
        if (isActive)
        {
            var initTeleport = _savedTeleportObject.GetComponent<InitializeTeleport>();
        
            if (_savedTeleportObject)
            {
                if (initTeleport)
                {
                    if (initTeleport._canTeleport)
                    {
                        teleportEvents.OnBeginPinchingTeleport.Invoke();
                        
                        //TeleportPlayer();
                    }
                }
            }
            else
            {
                Debug.Log("Please Select The Teleport Point !!!");
            }
        }
    }

    public void TeleportPlayer()
    {
        _teleport.TeleportPlayer();
        /*if (isActive)
        {
            
        }*/
    }

    /// <summary>
    /// Emitted when teleport is began
    /// </summary>
    public event TeleportEventHandler BeginPinchingTeleport;
    
    /// <summary>
    /// Emitted when teleport is done
    /// </summary>
    public event TeleportEventHandler CompleteTeleport;
    
    /// <summary>
    /// Emitted when teleport is began
    /// </summary>
    public event TeleportEventHandler TeleportActivated;
    
    /// <summary>
    /// Emitted when teleport is done
    /// </summary>
    public event TeleportEventHandler TeleportDeactivated;

    private void OnEnable()
    {
        BeginPinchingTeleport += teleportEvents.OnBeginPinchingTeleport.Invoke;

        CompleteTeleport += teleportEvents.OnCompleteTeleport.Invoke;
        
        TeleportActivated += teleportEvents.OnTeleportActivated.Invoke;

        TeleportDeactivated += teleportEvents.OnTeleportDeactivated.Invoke;
    }

    private void OnDisable()
    {
        BeginPinchingTeleport -= teleportEvents.OnBeginPinchingTeleport.Invoke;

        CompleteTeleport -= teleportEvents.OnCompleteTeleport.Invoke;
        
        TeleportActivated -= teleportEvents.OnTeleportActivated.Invoke;

        TeleportDeactivated -= teleportEvents.OnTeleportDeactivated.Invoke;
    }
}


[Serializable] public class TeleportEvents
{
    [Serializable] public sealed class TeleportEvent : UnityEvent { }
    
    public TeleportEvent OnBeginPinchingTeleport = new TeleportEvent();
    
    public TeleportEvent OnCompleteTeleport = new TeleportEvent();
    
    public TeleportEvent OnTeleportActivated = new TeleportEvent();
    
    public TeleportEvent OnTeleportDeactivated = new TeleportEvent();
}