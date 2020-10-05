using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowPopOut()
    {
        animator.SetBool("Show", true);
        animator.SetBool("Hide", false);
    }

    public void HidePopOut()
    {
        animator.SetBool("Show", false);
        animator.SetBool("Hide", true);
    }

    public bool IsHiding()
    {
        if (animator != null)
        {
            return animator.GetBool("Hide") && !animator.GetBool("Show");
        }
        else
        {
            return false;
        }
    }

}
