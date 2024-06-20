using TMPro;
using UnityEngine;

public class CoinHUD : MonoBehaviour
{
    public TextMeshProUGUI pointsHUD;
    private GameMaster gameMaster;
    private int points;

    private void OnEnable()
    {
        Coin.OnCoinCollected += UpdatePoints;
        Enemy.OnAddCoin += UpdatePoints;
    }
    
    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdatePoints;
        Enemy.OnAddCoin -= UpdatePoints;
    }
    
    void Start() 
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        points = gameMaster.points;
        pointsHUD.text = points.ToString();
    }
    
    private void UpdatePoints(int pointsToAdd) 
    {
        this.points = gameMaster.points;
        pointsHUD.text = this.points.ToString();
    }
}
