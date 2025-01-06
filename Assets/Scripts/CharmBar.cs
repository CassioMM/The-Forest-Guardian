using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharmBar : MonoBehaviour
{

    public Slider sliderH;
    public Slider sliderQ;

    PlayerMovimnt2 player;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovimnt2>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderH.value = player.heath;
        sliderQ.value = player.QuickValue;
    }
}
