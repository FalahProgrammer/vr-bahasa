using UnityEngine;

public class AlwaysLookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform _cam;

    void Update()
    {
        /*Quaternion lookRotation = _cam.rotation;
        lookRotation.z = 0;
        
        transform.rotation = lookRotation;*/
        
        transform.LookAt(new Vector3(_cam.position.x,_cam.position.y,_cam.position.z));
    }
}
