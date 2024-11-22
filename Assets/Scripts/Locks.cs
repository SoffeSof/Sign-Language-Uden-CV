using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Locks : MonoBehaviour
{
    public GameObject lockedIcon;        // Reference to the locked state icon
    public GameObject unlockedIcon;      // Reference to the unlocked state icon
    public TextMeshProUGUI lockIconText;
    public Button[] exercisesToUnlock;
    public GameObject[] lockedIcons;
    public GameObject[] unlockedIcons;
    public int completedExercises;
    public int requiredExercises;

    public void Start()
    {
        lockIconText.text = $"{completedExercises} / {requiredExercises}";
    }

    public void AddCompletedExercises()
    {
        completedExercises++;
        UpdateCompletionCount();
    }

    private void UpdateCompletionCount()
    {
        lockIconText.text = $"{completedExercises} / {requiredExercises}";

        if (completedExercises >= requiredExercises)
        {
            UnlockExercises();
        }
    }

    private void UnlockExercises()
    {
        lockedIcon.SetActive(false);
        unlockedIcon.SetActive(true);

        foreach (Button exerciseButton in exercisesToUnlock)
        {
            if (exerciseButton != null)
                exerciseButton.interactable = true;  // Make the button interactable
        }

        // Set all locked icons in the array to inactive
        foreach (GameObject icon in lockedIcons)
        {
                icon.SetActive(false);
        }

        // Set all unlocked icons in the array to active
        foreach (GameObject icon in unlockedIcons)
        {
                icon.SetActive(true);
        }
    }

    public void MarkIconAsCompleted(GameObject unlockedIcon, GameObject completedIcon)
    {
        unlockedIcon.SetActive(false);
        completedIcon.SetActive(true);
    }
}
