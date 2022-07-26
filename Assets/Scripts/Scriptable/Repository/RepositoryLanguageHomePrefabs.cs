using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Repository Language Home Prefabs", menuName = "Repository/Repository Language Home Prefabs")]
public class RepositoryLanguageHomePrefabs : ScriptableObject
{
    [SerializeField] private DataVariable stringVariable;
    [SerializeField] private ScriptableTransform prefabArea;
    
    public List<LanguageHomeArea> LanguageHomeAreas = new List<LanguageHomeArea>();

    public void AssignLanguageHomePrefab()
    {
        foreach (var v in LanguageHomeAreas)
        {
            if (stringVariable.materi_id != v.id)
            {
                continue;;
            }

            prefabArea.LanguageHomeArea = v.arePrefab;
        }
        
    }
}

[Serializable]
public class LanguageHomeArea
{
    public string name;
    public string id;
    public Transform arePrefab;
}
