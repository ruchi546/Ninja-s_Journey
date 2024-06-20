using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    
    public int TotalPoints { get { return TotalPoints; } }
    public int points;
    public int health;
    public int currentWeapon;

    public int enemiesKilled;
    public float time;
    
    private void OnEnable()
    {
        Coin.OnCoinCollected += AddPoints;
        Enemy.OnAddCoin += AddPoints;
    }
    
    private void OnDisable()
    {
        Coin.OnCoinCollected -= AddPoints;
        Enemy.OnAddCoin -= AddPoints;
    }
    
    void Awake() 
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } 
        else 
        {
            Destroy(gameObject);
        }
        
        // Initialize default values
        this.health = 3;
        this.points = 0;
        this.currentWeapon = 0;
        this.time = 0;
        this.enemiesKilled = 0;
    }
    public void Update() 
    {
        time += Time.deltaTime;
    }

    private void AddPoints(int pointsToAdd) 
    {
        points += pointsToAdd;
    }

    public void AddEnemiesKilled(int number) 
    {
        enemiesKilled += number;
    }
}
