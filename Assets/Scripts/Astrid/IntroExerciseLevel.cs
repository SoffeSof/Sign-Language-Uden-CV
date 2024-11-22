using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroExerciseLevel : LevelTemplate // Ensure it inherits from LevelTemplate
{
    public string lettersToPractice = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ";  // Letters to practice
    
    // For this exercise, we overide GetWord as we make use of letterToPractice instead.
    protected override string GetWord()
    {
        return "";  // Return a string representing the word (or empty for this exercise)
    }

    // Track the current letter being practiced

    private void Awake()
    {
        lettersToPractice = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ";
    }

    void Start()
    {
        StartExercise();
    }

    public void StartExercise()
    {
        Debug.Log($"Letters to practice: {lettersToPractice}");
        if (lettersToPractice.Length > 0)
        {
            StartCoroutine(PracticeLetters());
        }
        else
        {
            Debug.LogWarning("No letters to practice!");
        }
    }

    // 

    private IEnumerator PracticeLetters()
    {
        Debug.Log("Starting practice letters...");
        while (currentLetterIndex < lettersToPractice.Length)
        {
            char currentLetter = lettersToPractice[currentLetterIndex];

            // Play the animation for the current letter
            StartCoroutine(animationManager.PlayLetterAnimation(currentLetter));

            // Wait for player input immediately after starting the animation
            bool letterConfirmed = false;

            while (!letterConfirmed)
            {
                // Check if the player pressed Enter
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    // Check if the input matches the current letter
                    if (inputField.text.ToUpper() == currentLetter.ToString())
                    {
                        feedbackText.text = $"Correct! {currentLetter}";
                        letterConfirmed = true;  // Move to the next letter
                        currentLetterIndex++;
                    }
                    else
                    {
                        feedbackText.text = $"Incorrect! Try again.";
                    }

                    inputField.text = "";  // Clear the input field
                }

                yield return null;  // Wait for the next frame
            }
        }


        // Exercise complete
        Debug.Log("Exercise complete! Returning to menu...");
        // Add transition back to menu or next scene here
    }
}
