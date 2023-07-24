using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    public static NextLevelLoader instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    public void GoToNextLevel()
    {
        StartCoroutine(LoadSceneWithDelay());
    }

    public IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }

}
