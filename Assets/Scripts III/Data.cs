using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public GameObject[] Datas;
    public static float posX, posY, posZ;


    void Awake()
    {
        Datas = GameObject.FindGameObjectsWithTag("Data");
        if(Datas.Length >= 10)
        {
            Destroy(Datas[0]);

        }
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("PosX")){
            posX = PlayerPrefs.GetFloat("PosX");
        }
        else
        {
            PlayerPrefs.SetFloat("PosX", posX);
        }
        //
        if (PlayerPrefs.HasKey("PosY"))
        {
            posY = PlayerPrefs.GetFloat("PosY");
        }
        else
        {
            PlayerPrefs.SetFloat("PosY", posY);
        }
        //
        if (PlayerPrefs.HasKey("PosZ"))
        {
            posZ = PlayerPrefs.GetFloat("PosZ");
        }
        else
        {
            PlayerPrefs.SetFloat("PosZ", posZ);
        }
    }


    /*void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            Application.LoadLevel("MainGame");
        }
    }*/

}
