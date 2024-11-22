using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class LevelTemplate : MonoBehaviour
{
    public AnimationManager animationManager;  // Reference to the AnimationManager
    public TMP_Text feedbackText;              // Text to show feedback (e.g., correct/incorrect)
    public TMP_InputField inputField;          // TMP InputField for player to type their guess
    public float levelCompleteDelay = 2.0f;    // Scene transition delay
    // Delay before returning to menu after completing a level

    protected abstract string GetWord();  // Abstract method: each derived class specifies its word
    protected int currentLetterIndex = 0; // Tracks the current letter being spelled

    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    // Function to start the level
    public void StartLevel()
    {
        string word = GetWord();  // Retrieve the word for this level
        StartCoroutine(SpellWordAndCompleteLevel(word));  // Start spelling the word using the AnimationManager
    }

    // Coroutine to spell the word and complete the level
    private IEnumerator SpellWordAndCompleteLevel(string word)
    {
        word = word.ToUpper();  // Convert the word to uppercase

        // Loop through each letter in the word
        while (currentLetterIndex < word.Length)
        {
            char currentLetter = word[currentLetterIndex];

            // Play the animation for the current letter
            yield return StartCoroutine(animationManager.PlayLetterAnimation(currentLetter));

            // Wait for player to type the correct letter
            bool letterConfirmed = false;

            while (!letterConfirmed)
            {
                // Check if the player pressed Enter after typing
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    // Validate input with the current letter
                    if (inputField.text.ToUpper() == currentLetter.ToString())
                    {
                        feedbackText.text = $"Correct! It was {currentLetter}";
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

        // After the word is spelled, wait for a delay and return to the menu or next level
        yield return new WaitForSeconds(levelCompleteDelay);
        ReturnToMenu();
    }

    // Function to return to the menu (or load the next level)
    private void ReturnToMenu()
    {
        Debug.Log("Level complete! Returning to menu...");
        // Add your scene transition logic here (e.g., SceneManager.LoadScene("MainMenu"));
    }
}




