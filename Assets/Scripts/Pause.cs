using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseText;
    public GameObject mainMenuButton;
    public GameObject exitButton;
    public GameObject resetButton;
    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnPause()
    {
       if(!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseText.SetActive(true); mainMenuButton.SetActive(true); resetButton.SetActive(true); exitButton.SetActive(true);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseText.SetActive(false); mainMenuButton.SetActive(false); exitButton.SetActive(false); resetButton.SetActive(false);
        }
    }
}
