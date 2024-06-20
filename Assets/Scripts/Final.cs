using UnityEngine;

public class Final : MonoBehaviour 
{
    [SerializeField] private GameObject finalPanel;
    private GameMasterHUD gameMasterHUD;

    private PauseMenu pauseMenu;

    void Start() 
    {
        finalPanel.SetActive(false);
        gameMasterHUD = finalPanel.GetComponent<GameMasterHUD>();
        pauseMenu = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseMenu>();
        pauseMenu.enabled = true;
    }

    public void Toggle() 
    {
        finalPanel.SetActive(!finalPanel.activeSelf);
        gameMasterHUD.ShowStatistics();
        Time.timeScale = finalPanel.activeSelf ? 0f : 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag(tag: "Player")) 
        {
            Toggle();
            pauseMenu.enabled = false;
        }
    }
}