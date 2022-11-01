using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LoadMenu;
    public GameObject SettingsMenu;

    public void OnNewGame()
    {
        SceneManager.LoadScene("Zach's Test Scene");
    }

    public void OnLoadGame()
    {
        MainMenu.SetActive(false);
        LoadMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void OnSettings()
    {

    }

}
