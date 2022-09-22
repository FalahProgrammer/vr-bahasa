using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AreaPrefab : MonoBehaviour
{
   [SerializeField] private bool _debugMode;
   
   [Space(10)]
   public Transform characterContainer;
   [SerializeField] private ListInteractor _listInteractor;
   [SerializeField] private Transform _playerInitialPosition;

   [Space(10)]
   [SerializeField] private Vector3 playerInitialPositionAdjustment;
   
   [Space(10)]
   public SkyboxType skyboxType;
   [HideInInspector] public Material skybox;
   
   [SerializeField] private RepositorySkybox _repositorySkybox;

   public void ClearData()
   {
      if (_debugMode) Debug.Log("Data CLeared!");
      _listInteractor.listInteractors.Clear();
      _listInteractor.ListUIPosition.Clear();
      _listInteractor.listCharacterPosition.Clear();
      _listInteractor.listCharacterRotation.Clear();
   }

   private void Awake()
   {
      _repositorySkybox = Resources.Load<RepositorySkybox>("ScriptableObjects/Repository/Repository Skybox");
      SetSkyBox();
      
      if (_listInteractor == null) _listInteractor = Resources.Load<ListInteractor>("ScriptableObjects/List Interactor");
      
      GetNewData();
   }
   
   private void SetSkyBox()
   {
      switch (skyboxType)
      {
         case SkyboxType.defaultSkybox:
            if (RenderSettings.skybox != _repositorySkybox.defaultSkybox)
            {
               RenderSettings.skybox = _repositorySkybox.defaultSkybox;
            }
            break;
         case SkyboxType.customSkybox:
            if (RenderSettings.skybox != skybox)
            {
               RenderSettings.skybox = skybox;
            }
            break;
      }
   }

   public void GetNewData()
   {
      ClearData();
      
      _listInteractor.homeTransform = _playerInitialPosition;

      if (playerInitialPositionAdjustment != Vector3.zero)
      {
         Vector3 originPos = _listInteractor.homeTransform.position;
         _listInteractor.homeTransform.position = new Vector3(originPos.x + playerInitialPositionAdjustment.x, originPos.y + playerInitialPositionAdjustment.y, originPos.z + playerInitialPositionAdjustment.z);
      }

      

      foreach (Transform c in characterContainer)
      {
         if (!c.gameObject.activeSelf) continue;
         if (c.name.Remove(3, c.name.Length - 3) != "NPC") continue;
         
         var pos = c.position;
         //c.GetChild(1).GetChild(0).position;
         var charAdjustment = c.GetComponent<CharacterAdjustment>();
         
         // Interactor's transform position
         _listInteractor.listInteractors.Add(c);
         
         // UI position
         var uiPosition = charAdjustment.uiPivot.position;
         var adjustmentUIPosition = charAdjustment.adjustmentUIPosition;
         switch (charAdjustment.playerIsSitting)
         {
            case true:
               _listInteractor.ListUIPosition.Add(new Vector3(
                  uiPosition.x + adjustmentUIPosition.x,
                  uiPosition.y + adjustmentUIPosition.y, 
                  uiPosition.z + adjustmentUIPosition.z));
               break;
            
            case false:
               _listInteractor.ListUIPosition.Add(new Vector3(
                  uiPosition.x + adjustmentUIPosition.x,
                  uiPosition.y + 0.7f + adjustmentUIPosition.y, 
                  uiPosition.z + adjustmentUIPosition.z));
               break;
         }
         
         
         
         
         // Character warp position
         var adjustmentPlayerPosition = charAdjustment.adjustmentPlayerPosition;
         var position = charAdjustment.characterPivot.position;
         switch (charAdjustment.playerIsSitting)
         {
            case true:
               _listInteractor.listCharacterPosition.Add(new Vector3(
                  position.x + adjustmentPlayerPosition.x, 
                  pos.y + 0.2f + adjustmentPlayerPosition.y, 
                  position.z + adjustmentPlayerPosition.z));
               break;
                  
            case false:
              
               _listInteractor.listCharacterPosition.Add(new Vector3(
                  position.x + adjustmentPlayerPosition.x, 
                  pos.y + 0.65f + adjustmentPlayerPosition.y, 
                  position.z + adjustmentPlayerPosition.z));
               break;
         }

         Debug.Log("Char Y: " + pos.y);
         
         // character warp rotation
         var adjustmentRotation = charAdjustment.adjustmentPlayerRotation;
               
         _listInteractor.listCharacterRotation.Add(new Vector3(
            0 + adjustmentRotation.x, 
            180 + c.localRotation.eulerAngles.y  + adjustmentRotation.y, 
            0 + adjustmentRotation.z));
         
      }
      
      //add player initial position to the bottom of the list
      var playerPos = _playerInitialPosition.position;
      _listInteractor.homeVector = new Vector3(playerPos.x, 0.46f, playerPos.z);
      
      
      // _listInteractor.ListUIPosition.Add(new Vector3(playerPos.x - 0.5f, playerPos.y + 1.5f, playerPos.z));
      
      // add player initial position to the bottom of the list
      // _listInteractor.listCharacterPosition.Add(_playerInitialPosition.position);
     
      if (_debugMode) Debug.Log("Data Generated!");
   }
}

public enum SkyboxType
{
   defaultSkybox, customSkybox
}