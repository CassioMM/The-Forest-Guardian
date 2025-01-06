using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelTransition(SceneManager.GetActiveScene().buildIndex + 1));


    }

    IEnumerator LoadLevelTransition(int levelIndex)
    {
        transition.SetTrigger("Star");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);


    }
}
