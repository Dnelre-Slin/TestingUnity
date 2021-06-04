using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    protected Animator animator;

    [SerializeField]
    protected string playAnimation;
    [SerializeField]
    protected string playReverseAnimation;

    // Start is called before the first frame update
    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Play()
    {
        float startTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (startTime > 1.0f)
        {
            startTime = 1.0f;
        }
        startTime = 1.0f - startTime;
        animator.Play(playAnimation, 0, startTime);
    }

    public void PlayReverse()
    {
        float startTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (startTime > 1.0f)
        {
            startTime = 1.0f;
        }
        startTime = 1.0f - startTime;
        animator.Play(playReverseAnimation, 0, startTime);
    }
}
