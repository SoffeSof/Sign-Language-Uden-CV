using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public UdpSocket Landmarks;
    public GameObject[] handPoints;

    // Optional: Adjust scaling and offset values
    private float scaleFactor = 0.01f;
    private Vector3 offset = new Vector3(0, 0, 1);

    void Update()
    {
        float[] landmarks = Landmarks.GetLandmarks();

        for (int i = 0; i < 21; i++)
        {
            // Extract x and y coordinates, negate only y for flipping on y-axis
            float x = landmarks[i * 2] * scaleFactor;
            float y = -landmarks[i * 2 + 1] * scaleFactor;

            // Set the position of the hand point
            handPoints[i].transform.localPosition = new Vector3(x, y, 0) + offset;
            handPoints[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
}