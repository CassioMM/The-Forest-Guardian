using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject PausePainel;
    public GameObject cntrlObj;
    public GameObject cntrlObj2;



    public Text itemText;

    public AudioSource audioMenu;

    public GameObject[] itensPause;
    public GameObject[] itensConfig;
    public GameObject[] itensControls;

    bool invActive = false;
    bool pauseActive = false;
    bool cntrlActive = true;
    bool cntrlActive2 = true;


    void Start()
    {
        itemText.text = null;
        
    }


    void Update()
    {

        //Inventario
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            invActive =! invActive;
            inventoryPanel.SetActive(invActive);
            audioMenu.Play();

        }


        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseActive =! pauseActive;
            PausePainel.SetActive(pauseActive);
            audioMenu.Play();

        }


        if (pauseActive == true || invActive == true || cntrlActive == true || cntrlActive2 == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Resume3()
    {
        cntrlActive2 = !cntrlActive2;
        cntrlObj2.SetActive(cntrlActive2);
    }

    public void Resume2()
    {
        cntrlActive = !cntrlActive;
        cntrlObj.SetActive(cntrlActive);
    }

    public void Resume()
    {
        pauseActive =! pauseActive;
        PausePainel.SetActive(pauseActive);
    }

    public void Configuracoes()
    {
        for (int i = 0; i < itensConfig.Length; i++)
        {
            itensConfig[i].SetActive(true);
        }

        for (int i = 0; i < itensPause.Length; i++)
        {
            itensPause[i].SetActive(false);
        }

        for (int i = 0; i < itensControls.Length; i++)
        {
            itensControls[i].SetActive(false);
        }

    }

    public void ExitConfiguracoes()
    {
        for (int i = 0; i < itensPause.Length; i++)
        {
            itensPause[i].SetActive(true);
        }

        for (int i = 0; i < itensConfig.Length; i++)
        {
            itensConfig[i].SetActive(false);
        }

        for (int i = 0; i < itensControls.Length; i++)
        {
            itensControls[i].SetActive(false);
        }

    }

    public void Controls()
    {
        for (int i = 0; i < itensControls.Length; i++)
        {
            itensControls[i].SetActive(true);

        }

        for (int i = 0; i < itensPause.Length; i++)
        {
            itensPause[i].SetActive(false);
        }

        for (int i = 0; i < itensConfig.Length; i++)
        {
            itensConfig[i].SetActive(false);
        }

    }


    public void Menu()
    {
        SceneManager.LoadScene("Menu");

    }

}
