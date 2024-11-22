using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerExpression(bool isCorrect, float expressionBlendValue)
    {
        if (isCorrect)
        {
            animator.SetBool("IsCorrect", true);
            animator.SetFloat("CorrectExpressionBlend", expressionBlendValue);
        }
        else
        {
            animator.SetBool("IsCorrect", false);
            animator.SetFloat("WrongExpressionBlend", expressionBlendValue);
        }
    }

    public void ResetToIdle()
    {
        animator.SetBool("IsCorrect", false);
    }
}
