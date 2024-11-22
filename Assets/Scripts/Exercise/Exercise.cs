using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class Exercise : MonoBehaviour
{
    //Managers
    [SerializeField] protected NavigationManager navManager;
    [SerializeField] protected AnimationManager animationManager;

    //Word variables
    [SerializeField] protected List<string> words; // List of letters
    [SerializeField] protected int currentWordIndex = 0; // Index of the current word in the list
    [SerializeField] protected int currentCharIndex = 0; // Index of the current character in the current word
    [SerializeField] protected TextMeshProUGUI letterDisplay; // UI element to display the current letter
    [SerializeField] protected TextMeshProUGUI progressText; // UI element to display the current letter


    // GameObject references for the exercise and the completion panel and the introduction panel
    protected GameObject exercisePanel;
    protected GameObject completedPanel;
    protected GameObject startPanel;
    [SerializeField] protected GameObject exercise;

    //
    //Lock reference
    public Locks associatedLock;
    [SerializeField] protected bool isCompleted = false;

    //Icons
    [SerializeField] protected GameObject unlockedIcon;
    [SerializeField] protected GameObject completedIcon;
    [SerializeField] protected Button replayButton;

    //Bool
    protected bool isWordAChar = true;
    protected bool wordCompleted = false;

    public void OrderPanels()
    {
        // Assign UI elements by finding them in the scene
        exercisePanel = transform.Find("Canvas/Exercise").gameObject;
        completedPanel = transform.Find("Canvas/Completed").gameObject;
        startPanel = transform.Find("Canvas/Start").gameObject;

        // Ensure the exercise UI starts with the correct panels visible
        if (exercise != null) exercise.SetActive(false);
        if (completedPanel != null) completedPanel.SetActive(false);
        if (exercisePanel != null) exercisePanel.SetActive(false);
        if (startPanel != null) startPanel.SetActive(true);
    }

    public virtual void DisplayLetter()
    {
        // Get the current word
        string currentWord = words[currentWordIndex];

        // Display the current character in the word
        if (currentCharIndex < currentWord.Length)
        {
            char currentChar = currentWord[currentCharIndex];
            letterDisplay.text = currentChar.ToString();
            currentCharIndex++; // Move to the next character
        }
        else
        {
            wordCompleted = true;
            currentCharIndex = 0;
        }

    }

    public void PlayHandAnimation()
    {
        StartCoroutine(PlayLetterSequence());
    }

    public void ReplayAnimation()
    {
        animationManager.StopLetterAnimation();
        //animationManager.ResetAnimationState();
        StartCoroutine(PlayLetterSequence());
    }
    
   protected virtual IEnumerator PlayLetterSequence()
   {
       Debug.Log($"Starting PlayLetterSequence for word index {currentWordIndex}");

       if (isWordAChar == false)
       {
           animationManager.GoToIdle();
            Debug.Log("Hello");
       }

       // Check if there are words left to animate
       if (currentWordIndex < words.Count)
       {
           string currentWord = words[currentWordIndex]; //Finds the word
           int currentCharIndexAni = 0;

           // Iterate using currentCharIndexAni to track each character in the word
           while (currentCharIndexAni < currentWord.Length)
           {
                replayButton.interactable = false;
                char currentChar = currentWord[currentCharIndexAni];  // Corrected to use currentCharIndexAni

                // Play the animation for the current character using the index
                yield return StartCoroutine(animationManager.PlayLetterAnimation(currentChar));

                // Increment the character index to move to the next character
                currentCharIndexAni++;

           }
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
    

    public virtual void CompleteExercise()
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
            //ResetExercise();
            completedPanel.SetActive(false);
            exercisePanel.SetActive(false);
            startPanel.SetActive(true);
            exercise.SetActive(false);
            navManager.ToggleLevelMenu();
        }
    }

    public void UpdateWord()
    {
        currentWordIndex++;
        wordCompleted = false;
    }

    public virtual void CheckInput()
    {
    }

    public virtual void ResetExercise()
    {
        currentWordIndex = 0; // Index of the current word in the list
        currentCharIndex = 0;
        UpdateProgress();
        animationManager.StopLetterAnimation();
  

    }

    public virtual void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);
        //animationManager.ResetAnimationState();
        PlayHandAnimation();
        DisplayLetter();
        UpdateProgress();
    }

    public virtual void GoToCompletionPanel()
    {

    }

    public virtual void Exit()
    {
        StartCoroutine(ExitExercise());
    }

    public IEnumerator ExitExercise()
    {
        // Start the reset process
        ResetExercise();

        // Optionally, wait for a frame to ensure the reset is processed, or for the animator to reset.
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForEndOfFrame();

        // Now hide the exercise panel and reset the UI.
        exercisePanel.SetActive(false);
        startPanel.SetActive(true);
        exercise.SetActive(false);

        // Navigate back to the level menu
        navManager.ToggleLevelMenu();
    }

    public void UpdateProgress()
    {
        progressText.text = $"Opgave {currentWordIndex + 1} / {words.Count}";
    }


}
