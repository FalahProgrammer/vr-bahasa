using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Repository Log", menuName = "Repository/Repository Log")]
public class LogRepository : ScriptableObject
{
    public string content_id;
    public Log logs;
    
}

[Serializable]
public class Log
{
    public List<DataLog> Items;
}
