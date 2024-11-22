using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMenu : MonoBehaviour
{
    public GameObject levelUI; // The object to move
    public GameObject leftClampGO; // The right clamp point (GameObject to reference maxX)
    public float scrollSpeed = 20f; // Speed of horizontal movement
    public float rightClamp; // Minimum X position (left clamp point)
    public float leftClamp; // Maximum X position (right clamp point)

    private void Start()
    {
        // Set minX to the current position of levelUI when the game starts
        rightClamp = levelUI.transform.position.x;

        // Set maxX to the position of the rightClamp GameObject
        leftClamp = leftClampGO.transform.position.x;
    }

    void Update()
    {
        // Detect horizontal scroll using trackpad gestures
        float horizontalScroll = Input.GetAxis("Mouse ScrollWheel");

        if (horizontalScroll != 0)
        {
            // Adjust the position of levelUI horizontally
            Vector3 newPosition = levelUI.transform.position;
            newPosition.x += horizontalScroll * scrollSpeed;
            newPosition.x = Mathf.Clamp(newPosition.x, leftClamp, rightClamp);
            levelUI.transform.position = newPosition;
        }
    }
}
