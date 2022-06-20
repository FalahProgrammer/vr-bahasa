using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceHolderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _placeHolderText;

    private void Awake()
    {
        _placeHolderText.text = transform.parent.name;
    }
}
