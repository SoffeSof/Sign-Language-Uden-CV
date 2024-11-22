using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance to ensure there is only one GameManager instance
    public static GameManager Instance { get; private set; }

    // Tracks the current exercise collection the player is on
    private int currentExerciseCollection = 1; 

    // Property for accessing the current exercise collection (get and set)
    public int CurrentExerciseCollection 
    { 
        get => currentExerciseCollection; 
        set => currentExerciseCollection = value; // Set the current collection value
    }

    // Dictionary to track the completion status of different exercises
    public Dictionary<string, bool> CurrentExercises { get; private set; }

    // Called when the GameManager object is created
    private void Awake()
    {
        // Singleton pattern: ensures only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent GameManager from being destroyed when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances of GameManager
        }

        // Initialize the dictionary to track completed exercises
        CurrentExercises = new Dictionary<string, bool> 
        { 
            { "IntroductionCompleted", false }, 
            { "Spelling1Completed", false }, 
            { "Spelling2Completed", false }, 
            { "Reading1Completed", false }, 
            { "Reading2Completed", false } 
        };
    }
}
