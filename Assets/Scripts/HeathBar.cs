using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{

    public Slider sliderH;

    PlayerMovimnt player;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovimnt>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderH.value = player.heath;
    }
}
