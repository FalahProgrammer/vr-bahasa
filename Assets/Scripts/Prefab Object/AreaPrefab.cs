using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPrefab : MonoBehaviour
{
   [SerializeField] private ListInteractor _listInteractor;
   [SerializeField] private Transform _characterContainer;
   [SerializeField] private Transform _playerInitialPosition;

   private void OnDestroy()
   {
      ClearData();
   }

   public void ClearData()
   {
      _listInteractor.listInteractors.Clear();
      _listInteractor.ListUIPosition.Clear();
      _listInteractor.listCharacterPosition.Clear();
      _listInteractor.listCharacterRotation.Clear();
   }

   private void Awake()
   {
      ClearData();

      foreach (Transform c in _characterContainer)
      {
         var tempUI = c.GetChild(0).position;
         CharacterAdjustment charAdjustment = c.GetComponent<CharacterAdjustment>();
         
        
         
         
         // Interactor's transform position
         _listInteractor.listInteractors.Add(c);
         
         // UI position
         switch (charAdjustment.adjustUIPosition)
         {
            case true:
               var adjustmentUIPosition = charAdjustment.adjustmentUIPosition;
               
               _listInteractor.ListUIPosition.Add(new Vector3(
                  tempUI.x - 0.5f + adjustmentUIPosition.x,
                  tempUI.y + 3 + adjustmentUIPosition.y, 
                  tempUI.z + adjustmentUIPosition.z));
               
               break;
            case false:
               _listInteractor.ListUIPosition.Add(new Vector3(
                  tempUI.x - 0.5f,
                  tempUI.y + 3, 
                  tempUI.z));
               break;
         }
         
         // Character warp position
         switch (charAdjustment.adjustPlayerPosition)
         {
            case true:
               var adjustmentPlayerPosition = charAdjustment.adjustmentPlayerPosition;
               
               _listInteractor.listCharacterPosition.Add(new Vector3(
                  tempUI.x - 0.5f + adjustmentPlayerPosition.x, 
                  tempUI.y + 1.5f + adjustmentPlayerPosition.y, 
                  tempUI.z + adjustmentPlayerPosition.z));
               break;
            
            case false:
               _listInteractor.listCharacterPosition.Add(new Vector3(
                  tempUI.x - 0.5f, 
                  tempUI.y + 1.5f, 
                  tempUI.z));
               break;
         }
         
         // character war rotation
         
         switch (charAdjustment.adjustPlayerRotation)
         {
            case true:
               var adjustmentRotation = charAdjustment.adjustmentPlayerRotation;
               
               _listInteractor.listCharacterRotation.Add(new Vector3(
                  0 + adjustmentRotation.x, 
                  90  + adjustmentRotation.y, 
                  0 + adjustmentRotation.z));
               break;
            
            case false:
               _listInteractor.listCharacterRotation.Add(new Vector3(
                  0, 
                  90, 
                  0));
               break;
         }
         
      }
      
      //add player initial position to the bottom of the list
      var playerPos = _playerInitialPosition.position;
      _listInteractor.ListUIPosition.Add(new Vector3(playerPos.x - 0.5f, playerPos.y + 1.5f, playerPos.z));
      
      //add player initial position to the bottom of the list
      _listInteractor.listCharacterPosition.Add(_playerInitialPosition.position);
   }
}
