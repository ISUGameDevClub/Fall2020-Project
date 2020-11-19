using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{
    public static float VolumeLevel = 1;
    private Slider myValue;
    // Start is called before the first frame update
    void Start()
    {
        myValue = GetComponent<Slider>();
        myValue.value = VolumeLevel;
    }

    // Update is called once per frame
    void Update()
    {
       VolumeLevel = myValue.value;
       AudioListener.volume = VolumeLevel;
    }

   
}
