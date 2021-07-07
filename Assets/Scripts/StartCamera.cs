using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    Animator cameraAnim;
    CameraFollow go;
    public TimeCount timeCount;
    private void Awake()
    {
       go = GetComponent<CameraFollow>();
    }
    private void Start()
    {
        transform.position = new Vector3(1.236f, 1.79f, -7.354f);
        cameraAnim = GetComponent<Animator>();
        cameraAnim.SetBool("isStart", true);
        timeCount.enabled = false;
        StartCoroutine(StartCameraFollow());
    
    }

    IEnumerator StartCameraFollow()
    {
        yield return new WaitForSeconds(2);
        timeCount.enabled = true;
        cameraAnim.SetBool("isStart", false);
        CameraFollow go = GetComponent<CameraFollow>();
        go.enabled = true;
        transform.Rotate(20.42f, 0, 0);
    }
    private void Update()
    {
        
    }
}
