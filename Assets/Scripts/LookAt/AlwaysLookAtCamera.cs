using UnityEngine;

public class AlwaysLookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform _cam;

    /*private void Update()
    {
        transform.LookAt(_cam.position - _cam.forward);
    }*/
    
    void Update()
    {
        Quaternion lookRotation = _cam.rotation;
        transform.rotation = lookRotation;
    }
}
