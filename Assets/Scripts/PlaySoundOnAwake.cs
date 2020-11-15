using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour
{
    public AudioSource mySound;

    void Start()
    {
        AudioSource.PlayClipAtPoint(mySound.clip, transform.position);
    }

}
