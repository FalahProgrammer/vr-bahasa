using UnityEngine;

public class AreaPrefab : MonoBehaviour
{
   [SerializeField] private bool _debugMode;
   [SerializeField] private ListInteractor _listInteractor;
   [SerializeField] private Transform _characterContainer;
   [SerializeField] private Transform _playerInitialPosition;

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
      GetNewData();
   }

   public void GetNewData()
   {
      ClearData();

      foreach (Transform c in _characterContainer)
      {
         var tempUI = c.GetChild(0).position;
         var charAdjustment = c.GetComponent<CharacterAdjustment>();
         
         // Interactor's transform position
         _listInteractor.listInteractors.Add(c);
         
         // UI position
         switch (charAdjustment.adjustUIPosition)
         {
            case true:
               var adjustmentUIPosition = charAdjustment.adjustmentUIPosition;
               
               _listInteractor.ListUIPosition.Add(new Vector3(
                  tempUI.x + 0.5f + adjustmentUIPosition.x,
                  tempUI.y + 2.5f + adjustmentUIPosition.y, 
                  tempUI.z + adjustmentUIPosition.z));
               
               break;
            case false:
               _listInteractor.ListUIPosition.Add(new Vector3(
                  tempUI.x + 0.5f,
                  tempUI.y + 2.5f, 
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
                  tempUI.y + 1.4f + adjustmentPlayerPosition.y, 
                  tempUI.z + adjustmentPlayerPosition.z));
               break;
            
            case false:
               _listInteractor.listCharacterPosition.Add(new Vector3(
                  tempUI.x - 0.5f, 
                  tempUI.y + 1.4f, 
                  tempUI.z));
               break;
         }
         
         // character warp rotation
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
      
      if (_debugMode) Debug.Log("Data Generated!");
   }
}
