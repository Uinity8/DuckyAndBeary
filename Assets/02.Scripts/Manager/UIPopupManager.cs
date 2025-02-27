using System.Collections.Generic;
using UI;
using UI.Popup;
using UnityEngine;

namespace Manager
{
    public class UIPopupManager
    {
        private Dictionary<string, UIPopup> popupPool;
        //private readonly Transform canvasTransform;

        public UIPopupManager()
        {
            popupPool = new Dictionary<string, UIPopup>();
        }

        public void ShowPopup(string popupName)
        {
            if (popupPool.TryGetValue(popupName, out UIPopup existingPopup))
            {
                if (!existingPopup.gameObject.activeSelf)
                {
                    existingPopup.gameObject.SetActive(true);
                    existingPopup.Initialize();
                }
                return;
            }

            GameObject popupPrefab = Resources.Load<GameObject>($"Popups/{popupName}");
            if (popupPrefab == null)
            {
                Debug.LogError($"Popup prefab {popupName} not found in Resources/Popups");
                return;
            }
   
            GameObject popupObj = Object.Instantiate(popupPrefab);
            popupObj.name = popupName;
            Transform canvasTransform = GameObject.FindObjectOfType<Canvas>().transform;
            popupObj.transform.SetParent(canvasTransform,false);   

            UIPopup popup = popupObj.GetComponent<UIPopup>();
            if (popup != null)
            {
                popup.Initialize();
                popupPool.Add(popupName, popup);
            }
            else
            {
                Debug.LogError($"Popup {popupName} does not contain UIPopup component.");
            }
        }

        public void ClosePopup(string popupName)
        {
            if (popupPool.TryGetValue(popupName, out UIPopup popup))
            {
                popup.Close();
            }
            else
            {
                Debug.LogWarning($"Popup {popupName} not found in popup pool.");
            }
        }

        public void TogglePopup(string popupName)
        {
            if (popupPool.TryGetValue(popupName, out UIPopup popup) && popup.gameObject.activeSelf)
            {
                ClosePopup(popupName);
            }
            else
            {
                ShowPopup(popupName);
            }
        }
    }
}