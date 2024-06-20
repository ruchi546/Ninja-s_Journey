using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
    [Header("UI Elements")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    
    [Header("References")]
    [SerializeField] private Ninja ninjaScript;

    [Header("Camera Shake")]
    private float shakeIntensity = 5;
    private float shakeFrequency = 5;
    private float shakeTime = 0.5f;
    
    private GameMaster gameMaster;
    private const int MaxHealth = 3;
    
    void Start () 
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    private void Update() 
    {
        foreach (Image heart in hearts) 
        {
            heart.sprite = emptyHeart;
        }
        for (int i = 0; i < gameMaster.health; i++) 
        {
            hearts[i].sprite = fullHeart;
        }
    }

    private void TakeDamage() 
    {
        gameMaster.health -= 1;
        SoundManager.Instance.PlaySound(ninjaScript.hitSFX);
        CineMachineShake.Instance.ShakeCamera(shakeIntensity, shakeFrequency, shakeTime);


        if (gameMaster.health <= 0) 
        {
            foreach (Image heart in hearts) 
            {
                heart.sprite = emptyHeart;
            }
            ninjaScript.Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("FlameBullet") || collision.collider.CompareTag("Tree"))
        {
            TakeDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Heal")) 
        {
            AddHealth(1);
            Destroy(collision.gameObject);
            SoundManager.Instance.PlaySound(ninjaScript.healSFX);
        }
        else if (collision.CompareTag("AllHeal")) 
        {
            AddAllHealth();
            Destroy(collision.gameObject);
            SoundManager.Instance.PlaySound(ninjaScript.healSFX);
        }
    }

    private void AddHealth(int value) 
    {
        if(gameMaster.health < 3) 
        {
            gameMaster.health += value;
        }
    }
    private void AddAllHealth() 
    {
        if (HealthRemaining() == 1) 
        {
            gameMaster.health += 1;
        }
        else if (HealthRemaining() == 2) 
        {
            gameMaster.health += 2;
        }
    }

    private int HealthRemaining() 
    {
        return MaxHealth - gameMaster.health;
    }
}