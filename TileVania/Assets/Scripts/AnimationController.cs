using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void RunAnimation(bool condition)
    {
        playerAnimator.SetBool("IsRunning",condition);
    }

    public void ClimbingAnimation(bool condition)
    {
        playerAnimator.SetBool("IsClimbing",condition);
    }

    public void DeathAnimation()
    {
        playerAnimator.SetTrigger("Dying");
    }
}
