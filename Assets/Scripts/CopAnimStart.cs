using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopAnimStart : MonoBehaviour
{
    Animator animator;
    FollowCop followCop;
    private void Awake()
    {
        transform.position = new Vector3(0, 0, -5.6f);
        followCop = GetComponent<FollowCop>();
    }
    void Start()
    {
        transform.Rotate(0, 0, 0);
        animator = GetComponent<Animator>();
        StartCoroutine(StartPlayAnim());

    }
    IEnumerator StartPlayAnim()
    {
       
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2);
        followCop.enabled = true;
        animator.SetBool("isDead", false);
        StartPlay();
    }

    void StartPlay()
    {
        animator.SetBool("isRun", true);
        transform.Rotate(0, 0, 0);
    }
}
