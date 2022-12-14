using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private bool isPaused;
    [SerializeField]
    private GameObject welcomeScreen;
    [SerializeField]
    private GameObject instructionScreen;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        menu = GameObject.Find("Pause Menu Manager/Pause Menu");
        welcomeScreen = GameObject.Find("Welcome Screen");
        instructionScreen = GameObject.Find("Pause Menu Manager/Help Menu");
        isPaused = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
        instructionScreen.SetActive(false);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            menu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            isPaused = false;
            menu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void LoadDemoScene()
    {
        SceneManager.LoadScene("Alina_Demo");
        Time.timeScale = 0;
    }

    public void EnemyCreationScene()
    {
        SceneManager.LoadScene("Enemy Creation Scene 1");
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseWelcomeScreen()
    {
        welcomeScreen.SetActive(false);
        //Time.timeScale = 1;
    }
    
    public void OpenInstructionScreen()
    {
        instructionScreen.SetActive(true);
        
    }
    
    public void CloseInstructionScreen()
    {
        instructionScreen.SetActive(false);
    }
}
