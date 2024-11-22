using UnityEngine;
using System.Collections.Generic; // Required for using Dictionary
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class NavigationManager : MonoBehaviour
{
    // Reference to the level menu UI
    [SerializeField] private GameObject levelMenu;
    public GameObject currentLock;

    
    // Function to toggle the visibility of the level menu and its children
    public void ToggleLevelMenu()
    {
        // Check if levelMenu is currently active
        if (levelMenu.activeInHierarchy)
        {
            // If active, deactivate the levelMenu and its children
            levelMenu.SetActive(false);
            foreach (Transform child in levelMenu.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            // If inactive, activate the levelMenu and its children
            levelMenu.SetActive(true);
            foreach (Transform child in levelMenu.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
