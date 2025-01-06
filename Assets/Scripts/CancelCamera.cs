using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelCamera : MonoBehaviour
{
    private Mouse_Look mouse_Look;
    private PlayerMovimnt playerMovimnt;
    private Monster monster;


    public void OnEnable()
    {
        mouse_Look = FindObjectOfType<Mouse_Look>();
        playerMovimnt = FindObjectOfType<PlayerMovimnt>();
        monster = FindObjectOfType<Monster>();

        mouse_Look.CancelCamera(value: false);
        playerMovimnt.CancelControler(value: false);
        monster.CancelMControler(value: false);
    }


    public void OnDisable()
    {
        /*mouse_Look = FindObjectOfType<Mouse_Look>();
        playerMovimnt = FindObjectOfType<PlayerMovimnt>();
        monster = FindObjectOfType<Monster>();*/

        mouse_Look.CancelCamera(value: true);
        playerMovimnt.CancelControler(value: true);
        monster.CancelMControler(value: true);
    }
}
