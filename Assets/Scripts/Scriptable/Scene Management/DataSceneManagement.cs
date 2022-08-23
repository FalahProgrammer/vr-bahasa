using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Scene", menuName = "Data Scene/Data Scene Management")]
public class DataSceneManagement : ScriptableObject
{
    public RepositoryLocation RepositoryLocation;
    public bool voskIsActive;

    private void Awake()
    {
        RepositoryLocation = Resources.Load<RepositoryLocation>("ScriptableObjects/Repository/Repository Location");
    }

    private void OnDisable()
    {
        voskIsActive = false;
    }
}
