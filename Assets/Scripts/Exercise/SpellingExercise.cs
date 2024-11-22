using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro UI components

public class SpellingExercise : Exercise
{
    [SerializeField] private TextMeshProUGUI currentWordText;

    public void Start()
    {
        OrderPanels();
    }
    
    public override void CheckInput()
    {
        //Logic for checking input
        DisplayLetter();
        if (wordCompleted == true)
        {
            UpdateWord(); //Updates index to go to the next word in the word list

            if (currentWordIndex < words.Count)
            {
                // Display the letter for the next word
                DisplayLetter();
                DisplayWord();
                UpdateProgress();
            }
            else
            {
                // No more words left, mark the exercise as complete
                GoToCompletionPanel();
                return; // Exit the function as there are no more words to process
            }
        }
    }

    public override void ResetExercise()
    {
        currentWordIndex = 0; // Reset to the first word
        currentCharIndex = 0; // Reset to the first character
        UpdateProgress();
    }

    // Display the full current word in currentWordText
    private void DisplayWord()
    {
        if (currentWordIndex < words.Count)
        {
            currentWordText.text = words[currentWordIndex];
        }
        else
        {
            currentWordText.text = ""; // Clear text if no more words
        }
    }

    public override void GoToCompletionPanel()
    {
        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
    }

    public override void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);

        // Reset the exercise to the initial state
        ResetExercise();
        DisplayLetter();
        DisplayWord();
        UpdateProgress();
    }
    
    public override void Exit()
    {
        // Start the reset process
        ResetExercise();

        // Now hide the exercise panel and reset the UI.
        exercisePanel.SetActive(false);
        startPanel.SetActive(true);
        exercise.SetActive(false);

        // Navigate back to the level menu
        navManager.ToggleLevelMenu();
    }
}
