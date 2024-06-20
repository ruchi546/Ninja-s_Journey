using System;
using TMPro;
using UnityEngine;

public class GameMasterHUD : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI enemiesKilledHUD;
    [SerializeField] private TextMeshProUGUI timeHUD;
    [SerializeField] private TextMeshProUGUI pointsHUDFinal;
    private GameMaster gameMaster;

    private int enemiesKilled;
    private float time;
    private int points;

    private void Awake() 
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    public void ShowStatistics() 
    {
        this.enemiesKilled = gameMaster.enemiesKilled;
        this.time = gameMaster.time;
        this.time = (float) (Math.Round(time, 1));

        enemiesKilledHUD.text = this.enemiesKilled.ToString();
        this.points = gameMaster.points;
        pointsHUDFinal.text = this.points.ToString();
        timeHUD.text = this.time.ToString();
    }
}