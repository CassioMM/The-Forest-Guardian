using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string proximaFase;

    public GameObject[] itensMenu;
    public GameObject[] itensConfig;

    public LevelLoader LL;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        StartCoroutine(LoadLevelTransition(SceneManager.GetActiveScene().buildIndex + 1)); //temporario
        //LL.LoadNextLevel();

    }


    public void StartGame2()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
    

    IEnumerator LoadLevelTransition(int levelIndex)
    {
        yield return null;

        SceneManager.LoadScene(levelIndex);


    }



    public void Configuracoes()
    {
        for(int i = 0 ; i < itensMenu.Length ; i++)
        {
            itensMenu[i].SetActive(false);
        }

        for (int i = 0; i < itensConfig.Length; i++)
        {
            itensConfig[i].SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Menu Configurações

    public void ExitConfiguracoes()
    {
        for (int i = 0; i < itensConfig.Length; i++)
        {
            itensConfig[i].SetActive(false);
        }

        for (int i = 0; i < itensMenu.Length; i++)
        {
            itensMenu[i].SetActive(true);
        }
    }

    
    public void LoadScene()
    {
        SceneManager.LoadScene("MainGame");

    }

    
    public void Menu()
    {
        SceneManager.LoadScene("Menu");

    }

}
