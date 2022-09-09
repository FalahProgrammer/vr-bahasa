using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Scenario Location", menuName = "Editor/Scenario Location")]
public class ScenarioLocation : ScriptableObject
{
    public List<string> ScenarioLocationList = new List<string>();
    public List<string> ScenarioAreaList = new List<string>();
    public List<int> npcId = new List<int>();
}
