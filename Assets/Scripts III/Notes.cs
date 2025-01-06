using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notes : MonoBehaviour
{
    [SerializeField]
    public GameObject _noteImage;
    [SerializeField]
    private TextMeshProUGUI text;

    public GameObject MessagePanel;

    private bool pegouDocumento = false;
    private bool playerColidindo = false;


    void Start()
    {
        MessagePanel.SetActive(false);
        

    }

    
    void Update()
    {
        if (!pegouDocumento && playerColidindo && Input.GetKeyDown(KeyCode.E))
        {

            pegouDocumento = true;
            _noteImage.SetActive(true);
            text.enabled = true;
    
        }


        if (pegouDocumento && playerColidindo && Input.GetKeyDown(KeyCode.X))
        {

            pegouDocumento = false;
            _noteImage.SetActive(false);
            text.enabled = false;


        }



    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TripaSeca"))
        {
            MessagePanel.SetActive(true);
            playerColidindo = true;
            
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TripaSeca"))
        {

            MessagePanel.SetActive(false);
            playerColidindo = false;


        }
    }


}
