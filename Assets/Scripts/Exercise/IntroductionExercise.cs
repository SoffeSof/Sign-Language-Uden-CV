using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class IntroductionExercise : Exercise
{
    //Done for now
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
                UpdateProgress();
                PlayHandAnimation(); //Plays the animations for that word.
            }
            else
            {
                // No more words left, mark the exercise as complete
                GoToCompletionPanel();
                return; // Exit the function as there are no more words to process
            }
        }
    }

    public override void GoToCompletionPanel()
    {
        // Start the reset process
        //ResetExercise();

        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
    }

    public override void CompleteExercise()
    {
        StartCoroutine(CompleteExerciseCoroutine());
    }

    public IEnumerator CompleteExerciseCoroutine()
    {
        // Check if the exerciseObject is active
        if (completedPanel.activeSelf)
        {
            if (isCompleted == false)
            {
                isCompleted = true;
                associatedLock.AddCompletedExercises();
                associatedLock.MarkIconAsCompleted(unlockedIcon, completedIcon);
            }
            // Hide the completion panel and the exercise, and go back to the level menu
            ResetExercise();
            // Optionally, wait for a frame to ensure the reset is processed, or for the animator to reset.
            yield return new WaitForEndOfFrame();

            completedPanel.SetActive(false);
            exercisePanel.SetActive(false);
            startPanel.SetActive(true);
            exercise.SetActive(false);
            navManager.ToggleLevelMenu();
        }
    }


}