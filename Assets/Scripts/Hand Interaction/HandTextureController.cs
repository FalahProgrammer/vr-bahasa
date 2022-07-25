using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTextureController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRendererLeft;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRendererRight;

    [SerializeField] private Material[] _handMaterials;
    [SerializeField] private int _currentHandMaterialIndex;
    
    private void Awake()
    {
        Material[] tempMaterials = _skinnedMeshRendererRight.materials;
        string materialName = tempMaterials[0].name;

        if (materialName.Contains("(Instance)"))
        {
            materialName = materialName.Replace(" (Instance)", "");
        }

        for (int i = 0; i < _handMaterials.Length; i++)
        {
            
            if (materialName != _handMaterials[i].name) continue;
            
            _currentHandMaterialIndex = i;
            break;;
        }
        
        Debug.Log("Material Index Assigned");
    }

    public void ChangeHandTexture()
    {
        if (_currentHandMaterialIndex >= _handMaterials.Length - 1)
        {
            _currentHandMaterialIndex = 0;
        }
        else
        {
            _currentHandMaterialIndex += 1;
        }

        ApplyHandTexture();
    }

    public void ResetHandTexture()
    {
        _currentHandMaterialIndex = 0;
        
        ApplyHandTexture();
    }

    private void ApplyHandTexture()
    {
        Material[] newMaterials = new Material[1]{_handMaterials[_currentHandMaterialIndex]};

        _skinnedMeshRendererLeft.materials = newMaterials;
        _skinnedMeshRendererRight.materials = newMaterials;
    }
}
