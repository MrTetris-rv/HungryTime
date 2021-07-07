using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public AudioSource buttonsAudio;
    public AudioClip clickAudio;

    public void ClickSound()
    {
        buttonsAudio.PlayOneShot(clickAudio);
    }
}
