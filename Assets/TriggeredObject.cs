using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggeredObject : MonoBehaviour
{
    [SerializeField] Trigger[] triggers;
    public bool isTriggered = false;

    private int index = 0;
    private void LateUpdate()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i].isActive && !triggers[i].isFired)
            {
                triggers[i].isFired = true;
                index++;
            }
            if(!triggers[i].isActive && triggers[i].isFired)
            {
                ExecuteTriggerExit();
                index--;
                triggers[i].isFired = false;
            }
        }
        if (index == triggers.Length)
        {
            isTriggered = true;
            ExecuteTriggerEnter();
        }
    }
    protected void SetIndex(int ammount)
    {
        index = ammount;
    }
    protected abstract void ExecuteTriggerEnter();
    protected abstract void ExecuteTriggerExit();
}
