using LibYiroth;
using UnityEngine;

namespace LibYiroth.Scene
{
    public enum EStates
    {
        None,
        Activated,
        Deactivated,
    }
    
    public class SceneObject : MonoBehaviour, Save.ISaveGame
    {
        public Data.Identification levelID = new(Data.IdentificationCategories.Game, Data.IdentificationTypes.Level, "");
        public Data.Node node;
        public EStates state = EStates.None;
        
        private Save.SaveManager _saveManager;

        private void Start()
        {
            _saveManager = FindFirstObjectByType<Save.SaveManager>();

            if (_saveManager == null)
                return;
        }

        public bool OnSaving(Save.SaveManager saveManager)
        {
            if (saveManager == null)
            {
                Debug.LogError("Save Manager not found!");
                return false;
            }

            if (saveManager.GetActiveGameSlot() == null)
            {
                Debug.LogError("Game Slot not found!");
                return false;
            }

            // NOTE: None of this save/load stuff works right now. IT IS INCOMPLETE.
            saveManager.SaveVariable(levelID, "lastLocation", transform.localPosition);
            
            return true;
        }

        public bool OnLoading(Save.SaveManager saveManager)
        {
            return false;
        }
    }
}