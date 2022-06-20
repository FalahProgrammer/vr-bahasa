using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Teleport
{
    private LineRenderer _lineRenderer { get; }
    private GameObject _teleportDirection { get; }
    private Transform _myPlayer { get; }
    private Transform _teleportPoint { get; }
    private Transform _point2 { get; }
    private Transform _teleportPointer { get; }
    private float _speed { get; }
    private float _curveHeight { get; }
    private float _density { get; }
    private Vector3 _savedTeleportPosition { get; }
    private Vector3 _teleportPivot { get; }
    private UnityEvent _teleportEvent { get; }


    public Teleport(LineRenderer lineRenderer,GameObject teleportDirection, 
        Transform myPlayer,Transform teleportPoint, Transform point2, Transform teleportPointer,float speed, float curveHeight,float density, 
        Vector3 savedTeleportPosition, Vector3 _teleportPivot, UnityEvent teleportEvent)
    {
        _lineRenderer = lineRenderer;
        
        _teleportDirection = teleportDirection;
        
        _myPlayer = myPlayer;
        
        _teleportPoint = teleportPoint;
        
        _point2 = point2;
        
        _teleportPointer = teleportPointer;
        
        _speed = speed;
        
        _curveHeight = curveHeight;
        
        _density = density;
        
        _savedTeleportPosition = savedTeleportPosition;
        
        this._teleportPivot = _teleportPivot;
        
        _teleportEvent = teleportEvent;
    }

    public void CalculateLine()
    {
        _teleportPoint.position = new Vector3(_savedTeleportPosition.x,_savedTeleportPosition.y + 0.1f,_savedTeleportPosition.z);;

        _point2.transform.position = new Vector3((_savedTeleportPosition.x + _teleportPointer.transform.position.x) / 2
            , _curveHeight,(_savedTeleportPosition.z + _teleportPointer.transform.position.z) / 2);
        
        var pointList = new List<Vector3>();

        for (float i = 0; i <= 1; i += 1 / _density)
        {
            var tangent1 = Vector3.Lerp(_savedTeleportPosition, _point2.position, i);
                        
            var tangent2 = Vector3.Lerp(_point2.position, _teleportPointer.position, i);
                        
            var curve = Vector3.Lerp(tangent1,tangent2, i);
            
            pointList.Add(curve);
        }
        
        _lineRenderer.positionCount = pointList.Count;
        
        _lineRenderer.SetPositions(pointList.ToArray());
    }

    public void TeleportPlayer()
    {
        _myPlayer.localRotation = _teleportDirection.transform.localRotation;
        
        _myPlayer.DOLocalMove(new Vector3(
                _savedTeleportPosition.x + _teleportPivot.x
                ,_savedTeleportPosition.y + _teleportPivot.y
                , _savedTeleportPosition.z + _teleportPivot.z)
            , _speed).SetId("BeginTeleport")
            .OnComplete(_teleportEvent.Invoke);
    }
}
