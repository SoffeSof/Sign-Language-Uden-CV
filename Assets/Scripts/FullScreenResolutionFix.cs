using UnityEngine;

public class FullScreenResolutionFix : MonoBehaviour
{
    void Start()
    {
        // Set the resolution to the current screen resolution
        int width = Screen.currentResolution.width;
        int height = Screen.currentResolution.height;
        
        // Force fullscreen mode with the native resolution
        Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
        
        // Ensure the aspect ratio matches the display
        Camera.main.aspect = (float)width / height;
    }
}
