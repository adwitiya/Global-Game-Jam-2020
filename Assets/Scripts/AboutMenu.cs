using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutMenu : MonoBehaviour
{
    public Transform MenuCanvas;

    public Transform pausedMenu;
    public Transform controlMenu;

    
    void Awake()
    {
        if (pausedMenu != null){
            pausedMenu.gameObject.SetActive(true);
            controlMenu.gameObject.SetActive(false);
        }
    }

   public void load_about()
    {
        SceneManager.LoadScene("AboutMenu");
    }

    public void load_controls()
    {
        SceneManager.LoadScene("ControlMenu");
    }

    public void load_game()
    {
        SceneManager.LoadScene("StefScene_copy_workfile");
    }

    public void resume_game()
    {
        MenuCanvas.gameObject.SetActive(false);
        pausedMenu.gameObject.SetActive(true);
        controlMenu.gameObject.SetActive(false);

    }

    public void load_main()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void show_controls()
    {
        pausedMenu.gameObject.SetActive(false);
        controlMenu.gameObject.SetActive(true);
    }

    public void quit_game()
    {
        Application.Quit();
    }
}
