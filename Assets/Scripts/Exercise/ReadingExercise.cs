using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class ReadingExercise : Exercise
{
     // Placeholder text for the input field
    private string placeholderText;

    // UI element references (set in the inspector)
    [SerializeField] private TMP_InputField inputField; // Text input field
    [SerializeField] private Button checkAnswerButton; // Button to submit the input
    [SerializeField] private TextMeshProUGUI placeholder; // UI element for placeholder text

    //Correct wrong logic
    [SerializeField] private GameObject checkMark;
    [SerializeField] private GameObject wrongMark;
    [SerializeField] private AudioClip correctSound; // Sound for correct answer
    [SerializeField] private AudioClip wrongSound; // Sound for incorrect answer
    [SerializeField] private float feedbackDuration = 1.5f; // Duration for showing feedback
    [SerializeField] private AudioSource audioSource;

    //Loading mechanics
    [SerializeField] private GameObject loadingIcon;



    // Property for the player's input
    public string PlayerInput { get; private set; } // Public getter, private setter

    //Bool
    private bool isCorrect;

    public void Start()
    {
        OrderPanels();
        checkMark.SetActive(false);
        wrongMark.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Code to execute when Enter key is pressed
            CheckInput();
        }
    }

    // Function to check the player's input when they click the submit button
    public override void CheckInput()
    { 
        // Get the current input from the input field
        PlayerInput = inputField.text;

        // Compare the player's input with the current letter
        if (PlayerInput.ToLower() == words[currentWordIndex].ToLower())
        {
            isCorrect = true;
            ShowFeedback(isCorrect);
            // If correct, move to the next letter
            currentWordIndex++;

            // Check if there are more letters to display
            if (currentWordIndex < words.Count)
            {
                // Reset the input field and show the next letter
                ResetInputField();
                PlayHandAnimation();
                UpdateProgress();
            }
            else
            {
                // If all letters are completed, show the completion panel and reset the exercise
                GoToCompletionPanel();
            }
        }
        else //Show wrong
        {
            isCorrect = false;
            ShowFeedback(isCorrect);
        }
    }

    // Function to reset the input field
    private void ResetInputField()
    {
        inputField.text = placeholderText;
    }

    // Function to reset the exercise to the initial state
    public override void ResetExercise()
    {
        currentWordIndex = 0; // Reset to the first letter
        currentCharIndex = 0; // Reset to the first letter
        ResetInputField(); // Clear the input field
        UpdateProgress();
        animationManager.StopLetterAnimation();
        checkMark.SetActive(false);
        wrongMark.SetActive(false);
    }

    public override void GoToCompletionPanel()
    {
        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
        ResetExercise();
    }

    public override void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);
        ResetInputField();
        //animationManager.ResetAnimationState();
        PlayHandAnimation();
        UpdateProgress();

    }

    public void ShowFeedback(bool isCorrect)
    {
        StartCoroutine(ShowFeedbackCoroutine(isCorrect));
    }

    private IEnumerator ShowFeedbackCoroutine(bool isCorrect)
    {
        // First, deactivate both marks to reset any previous feedback
        checkMark.SetActive(false);
        wrongMark.SetActive(false);


        if (isCorrect == true)
        {
            checkMark.SetActive(true);
            audioSource.clip = correctSound;
            audioSource.Play();
        }
        else if (isCorrect == false)
        {
            wrongMark.SetActive(true);
            audioSource.clip = wrongSound;
            audioSource.Play();
        }

        yield return new WaitForSeconds(feedbackDuration);

        checkMark.SetActive(false);
        wrongMark.SetActive(false);
        isCorrect = false;
    }

    protected override IEnumerator PlayLetterSequence()
    {
        Debug.Log($"Starting PlayLetterSequence for word index {currentWordIndex}");

        if (isWordAChar == false)
        {
            animationManager.GoToIdle();
        }

        // Check if there are words left to animate
        if (currentWordIndex < words.Count)
        {
            string currentWord = words[currentWordIndex]; //Finds the word
            int currentCharIndexAni = 0;

            // Iterate using currentCharIndexAni to track each character in the word
            while (currentCharIndexAni < currentWord.Length)
            {
                loadingIcon.SetActive(true);
                replayButton.interactable = false;
                char currentChar = currentWord[currentCharIndexAni];  // Corrected to use currentCharIndexAni

                // Play the animation for the current character using the index
                yield return StartCoroutine(animationManager.PlayLetterAnimation(currentChar));

                // Increment the character index to move to the next character
                currentCharIndexAni++;

            }
            float delayTime = 1.5f;
            yield return new WaitForSeconds(delayTime);
            loadingIcon.SetActive(false);
            replayButton.interactable = true;

            if (words[currentWordIndex].Length > 1)
            {
                isWordAChar = false;
            }
            else
            {
                isWordAChar = true;
            }
        }
        else
        {
            Debug.LogError($"currentWordIndex {currentWordIndex} is out of bounds for the words list (count: {words.Count}).");
        }
    }

}

