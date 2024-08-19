using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    [SerializeField] public bool isActive = false;
    [SerializeField] protected Animator animator;

    [HideInInspector] public bool isFired = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObjectScale obj))
        {
            isActive = true;
            
            if (animator)
                TriggerEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObjectScale obj))
        {
            isActive = false;

            if (animator)
                TriggerExit();
        }
    }
    protected abstract void TriggerEnter();
    protected abstract void TriggerExit();
}
