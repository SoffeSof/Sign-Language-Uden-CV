using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher_Dictionary : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject currentPanel; // The active panel (e.g., Dictionary_Canvas)
    public GameObject nextPanel; // The panel to activate (e.g., InfoPage_Canvas)

    public void SwitchPanel()
    {
        Debug.Log("Button clicked!");
        if (currentPanel != null && nextPanel != null)
        {
            currentPanel.SetActive(false); // Deactivate the current panel
            nextPanel.SetActive(true);    // Activate the next panel
        }
        else
        {
            Debug.LogError("Panel references are missing! Please assign them in the Inspector.");
        }
    }
}
