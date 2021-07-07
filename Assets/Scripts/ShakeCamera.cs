using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public Animator animator;

    public void camShake()
    {
        animator.SetTrigger("Shake"); 
    }
}
