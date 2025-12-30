/*
 * Copyright 2025 yiroth
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Purpose: A dynamic game object identifier which can be interacted, processed, saved/loaded from a slot
 */

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

            if (!saveManager.IsCurrentGameSlotActive())
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
            Vector3 lastLocation = transform.localPosition;
            saveManager.LoadVariable<Vector3>(levelID, "lastLocation", ref lastLocation);
            
            this.gameObject.transform.position = lastLocation;
            
            return false;
        }
    }
}