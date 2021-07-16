using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterWalkingCycle : MonoBehaviour
{
    public TaskBehaviour task;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PauseWalkingAnimation()
    {
        animator.enabled = false;
    }

    public void ResumeWalkingAnimation()
    {
        animator.enabled = true;
    }

    public void FlipCharacter()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
