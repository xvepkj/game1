using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR

using UnityEditor.SearchService;

#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField]private GameObject gameOverScreen;
    [SerializeField]private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false); 
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
            PauseGame(!pauseScreen.activeInHierarchy);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void StartGame()
    { 
        int level = PlayerPrefs.GetInt("level", 1);
        SceneManager.LoadScene(level);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

        
    }

    public void QuitGame ()
    {
        Application.Quit();

    }

    // Pausing 

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);


        if (status)
            Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
}
