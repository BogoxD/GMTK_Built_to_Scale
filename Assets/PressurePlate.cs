using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Trigger
{
    protected override void TriggerEnter()
    {
        animator.SetBool("IsPressed", true);
    }

    protected override void TriggerExit()
    {
        animator.SetBool("IsPressed", false);
    }

}
