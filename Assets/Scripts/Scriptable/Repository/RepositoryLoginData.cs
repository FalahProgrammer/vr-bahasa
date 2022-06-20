using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Repository Login Data", menuName = "Repository/Repository Login Data")]
public class RepositoryLoginData : ScriptableObject
{
    public int status_code;

    public string token;
    
    public List<RequestHeader> Header = new List<RequestHeader>();

    public List<DataLogin> data;
}
