/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeveButton : MonoBehaviour
{
    private GameObject buttonLocked;
    private GameObject buttonUnlocked;
    private GameObject buttonCompleted;

    [SerializeField] private int CollectionNumber;

    public void Start()
    {
        GameObject buttonLocked = transform.Find("Locked").gameObject;
        GameObject buttonUnlocked = transform.Find("Unlocked").gameObject;
        GameObject buttonCompleted = transform.Find("Completed").gameObject;

        if (CollectionNumber == 1 && gameObject.name == "Intro button")
        {
            if (buttonUnlocked != null) buttonUnlocked.SetActive(true);
            if (buttonCompleted != null) buttonCompleted.SetActive(false);
        }
        else
        {
            if (buttonLocked != null) buttonLocked.SetActive(true);
            if (buttonUnlocked != null) buttonUnlocked.SetActive(false);
            if (buttonCompleted != null) buttonCompleted.SetActive(false);
        }
    }

    public void UpdateButtonLock()
    {

    }


}*/
