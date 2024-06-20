using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour 
{
    [SerializeField] private int sceneBuildIndex;

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelsPanel;
    private GameMaster gameMaster;
    
    public static event Action<int> OnPLayMusic;

    void Start() 
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        settingsPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }
    
    public void Play(int index) 
    {
        sceneBuildIndex = index;
        gameMaster.points = 0;
        gameMaster.health = 3;
        gameMaster.currentWeapon = 0;

        gameMaster.time = 0;
        gameMaster.enemiesKilled = 0;
        
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

    public void Levels() 
    {
        mainMenuPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void Settings() 
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Quit() 
    {
        Application.Quit();
    }

    public void Back(GameObject panel) 
    {
        mainMenuPanel.SetActive(true);
        panel.SetActive(false);
    }
    
    public void PlayMusic(int index) 
    {
        OnPLayMusic?.Invoke(index);
    }
}