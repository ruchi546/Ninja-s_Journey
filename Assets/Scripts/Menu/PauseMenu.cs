using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour 
{
    [SerializeField] private GameObject pausePanel;
    private bool isInPause = false;

    void Start() 
    {
        pausePanel.SetActive(false);
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Toggle();
        }
    }

    public void Toggle() 
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        isInPause = pausePanel.activeSelf;

        Time.timeScale = pausePanel.activeSelf ? 0f : 1f;
    }

    public void ToMainMenu() 
    {
        MusicManager musicManager = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
        musicManager.PlayMusic(0);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
    
    public bool GetIsInPause() 
    {
        return isInPause;
    }
}