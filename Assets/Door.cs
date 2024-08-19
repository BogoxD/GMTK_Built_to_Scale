using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TriggeredObject
{
    public float OpenDelay = 3f;
    [SerializeField] Animator animator;

    protected override void ExecuteTriggerEnter()
    {
        Debug.Log("Open");
        animator.SetBool("isOpened", true);
    }

    protected override void ExecuteTriggerExit()
    {
        Debug.Log("Close");
        animator.SetBool("isOpened", false);
    }
}
