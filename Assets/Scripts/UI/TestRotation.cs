using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestRotation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmpText;
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform _leftWrist;
    [SerializeField] private Transform _leftHand;


    // Update is called once per frame
    void Update()
    {
        Vector3 origin = _cam.transform.rotation.eulerAngles;
        Vector3 target = _leftWrist.transform.rotation.eulerAngles;

        float diff = origin.x - target.x;
        
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, origin - target);

        //_tmpText.text = "rotation origin: " + origin + ", rotation target: " + target + ", rotation result: " + rotation.eulerAngles.ToString() + "<br>difference: " + diff;

        _tmpText.text = Mathf.RoundToInt(_leftHand.transform.rotation.eulerAngles.z).ToString();
    }
}
