using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public PlayerMovimnt player = new();
    public InventoryController inventory = new();


    void Start()
    {
        transform.position = new Vector3(Data.posX, Data.posY, Data.posZ);

        //LoadFromJson();
    }


    void Update()
    {
        Data.posX = transform.position.x;
        Data.posY = transform.position.y;
        Data.posZ = transform.position.z;

        //LoadFromJson();

        if (Input.GetKeyDown("h"))
        {

            LoadFromJson();
            /*PlayerPrefs.SetFloat("PosX", Data.posX);
            PlayerPrefs.SetFloat("PosY", Data.posY);
            PlayerPrefs.SetFloat("PosZ", Data.posZ);*/

            print("foi");
        }


        /*if (Input.GetKeyDown("g"))
        {
            Application.LoadLevel("Menu");
        }*/
    }

    public void Save()
    {
        string InventoryData = JsonUtility.ToJson(inventory);
        string filePathI = Application.persistentDataPath + "/InventoryData.json";
        Debug.Log(filePathI);
        System.IO.File.WriteAllText(filePathI, InventoryData);

        string playerData = JsonUtility.ToJson(player);
        string filePathP = Application.persistentDataPath + "/playerData.json";
        Debug.Log(filePathP);
        System.IO.File.WriteAllText(filePathP, playerData);


        Debug.Log("Salvo caraio");




        PlayerPrefs.SetFloat("PosX", Data.posX);
        PlayerPrefs.SetFloat("PosY", Data.posY);
        PlayerPrefs.SetFloat("PosZ", Data.posZ);

        print("foi");
    }


    public void LoadFromJson()
    {
        string filePathI = Application.persistentDataPath + "/InventoryData.json";
        string InventoryData = System.IO.File.ReadAllText(filePathI);


        string filePathP = Application.persistentDataPath + "/playerData.json";
        string playerData = System.IO.File.ReadAllText(filePathP);


        inventory = JsonUtility.FromJson<InventoryController>(InventoryData);
        player = JsonUtility.FromJson<PlayerMovimnt>(playerData);

        Debug.Log("Puxou caraio");

    }

}
