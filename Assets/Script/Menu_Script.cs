using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu_Script : MonoBehaviour
{
    private void Update()
    {
        Screen.SetResolution(694, 1080, FullScreenMode.Windowed);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MenuGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
