using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
        if (pauseScreen.activeInHierarchy)
            PauseGame(false);
        else PauseGame(true);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

        
    }

    public void Quitange ()
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
