using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UdpSocket : MonoBehaviour
{
    [HideInInspector] public bool isTxStarted = false;

    [SerializeField] string IP = "127.0.0.1"; // local host
    [SerializeField] int rxPort = 8000; // port to receive data from Python on
    [SerializeField] int txPort = 8001; // port to send data to Python on

    // Create necessary UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; // Receiving Thread

    float[] landmarks;
    int handSignID;
    public float[] GetLandmarks()
    {
        return landmarks;
    }
    void Awake()
    {
        // Create remote endpoint (to Matlab)
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

        // Create local client
        client = new UdpClient(rxPort);

        // Create a new thread for reception of incoming messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // Initialize (seen in comments window)
        print("UDP Comms Initialised");
    }

    // Receive data, update packets received
    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string receivedData = Encoding.UTF8.GetString(data);

                // Split the data based on the '|' delimiter
                string[] parts = receivedData.Split('|');

                // Extract landmarks and hand sign ID
                string landmarksString = parts[0].Substring(parts[0].IndexOf(": ") + 2); // Remove "from Python: "
                handSignID = int.Parse(parts[1].Trim());

                // Parse landmarks from the string
                string[] landmarkPairs = landmarksString.Split(';');
                landmarks = new float[landmarkPairs.Length * 2];
                for (int i = 0; i < landmarkPairs.Length; i++)
                {
                    string[] xy = landmarkPairs[i].Split(',');
                    landmarks[2 * i] = float.Parse(xy[0]);
                    landmarks[2 * i + 1] = float.Parse(xy[1]);
                }

                // Process the received data
                ProcessInput(landmarks, handSignID);
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void ProcessInput(float[] landmarks, int handSignID)
    {
        // Print the landmarks and hand sign ID to the console
        Debug.Log("Received Landmarks:");
        for (int i = 0; i < landmarks.Length; i += 2)
        {
            Debug.Log($"Landmark {i / 2}: X={landmarks[i]}, Y={landmarks[i + 1]}");
        }
        Debug.Log("Hand Sign ID: " + handSignID);

        // You can now use these values for further processing, such as:
        // - Visualizing landmarks in Unity
        // - Recognizing hand gestures using machine learning
        // - Triggering actions based on hand sign ID
    }

    //Prevent crashes - close clients and threads properly!
    void OnDisable()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }
}