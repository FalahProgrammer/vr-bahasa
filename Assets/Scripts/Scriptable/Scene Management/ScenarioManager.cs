using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario Manager", menuName = "Data Scene/Scenario Manager")]
public class ScenarioManager : ScriptableObject
{
   [field: SerializeField]
   public bool ScenarioIsActive { get; set; }

   public void ScenarioIsFinished()
   {
      ScenarioIsActive = false;
   }

   private void OnDestroy()
   {
      ScenarioIsActive = false;
   }
}
