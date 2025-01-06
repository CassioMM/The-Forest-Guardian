using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volumes : MonoBehaviour
{
    public int VolumeSlider = 1;
    public AudioSource[] audios;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        audios.GetValue(VolumeSlider);


    }
}
