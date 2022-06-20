using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;
    
    [SerializeField] private float _rotateSpeed = 2f;
    
    void Moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            {
                transform.Translate(Vector3.forward * _movementSpeed *Time.deltaTime);
            }
        }
            
        if (Input.GetKey(KeyCode.A))
        {
            {
                transform.Translate(-Vector3.right * _movementSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            {
                transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
            }
        }
            
        if (Input.GetKey(KeyCode.S))
        {
            {
                transform.Translate(-Vector3.forward * _movementSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            {
                transform.Rotate(0,_rotateSpeed,0 * _movementSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            {
                transform.Rotate(0,-_rotateSpeed,0 * _movementSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.C))
        {
            {
                transform.Rotate(Vector3.right * _rotateSpeed * Time.deltaTime);
            }
        }
            
        if (Input.GetKey(KeyCode.Z))
        {
            {
                transform.Rotate(-Vector3.right * _rotateSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            {
                transform.Translate(Vector3.down * _rotateSpeed * Time.deltaTime);
            }
        }
            
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            {
                transform.Translate(-Vector3.down * _rotateSpeed * Time.deltaTime);
            }
        }
    }
    
    private void Update()
    {
        Moving();
    }
}


