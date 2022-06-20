using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateDirection : MonoBehaviour
{
    [SerializeField] private GameObject _handleMesh;

    [SerializeField] private float _turnSmoothTime = 0.1f;

    [SerializeField] private float _turnSMoothVelocity;

    private void Start()
    {
        _handleMesh = GameObject.Find("Handle Mesh");
        
        _handleMesh.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        
        float horizontal = _handleMesh.transform.localPosition.x;
        
        //Debug.Log("Horizontal : " + horizontal);
        
        //float vertical = Input.GetAxisRaw("Vertical");
        
        float vertical = _handleMesh.transform   .localPosition.z;
        
        //Debug.Log("Vertical : " + vertical);
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle
                (transform.eulerAngles.y, targetAngle, ref _turnSMoothVelocity, _turnSmoothTime);
            
            transform.rotation = Quaternion.Euler(0f,targetAngle,0f);
        }
    }
}
