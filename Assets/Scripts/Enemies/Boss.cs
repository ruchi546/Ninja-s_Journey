using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss : MonoBehaviour
{
    [Header("Main Properties")]
    [SerializeField] public Transform target; //public -> Health
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;

    [Header("Movement")]
    private Vector3 movement;

    [Header("Statistics")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 11;
    [SerializeField] private float moveSpeed;

    [Header("SFX")]
    public AudioClip hitSFX;

    [Header("Prefabs")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject skullPrefab;

    [SerializeField] HealthBar healthBar;
    [FormerlySerializedAs("Final")] [SerializeField] GameObject finalObject;

    private Rigidbody2D rb;
    private BulletScript bulletScript;

    private bool canDash = true; 
    private float dashCooldown = 5.0f;
    
    private GameMaster gameMaster;
    
    private void Awake() 
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    void Start() 
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        healthBar.UpdateHealthBar(health, maxHealth);
        StartCoroutine(SpawnSkullsCoroutine());
    }

    void FixedUpdate() 
    {
        PerformMovement();
    }

    void PerformMovement() 
    {
        if (target == null)
        {
            return;
        }
        
        // Move towards the target if within chase radius
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);

            // Perform dash attack if within attack radius
            if (Vector2.Distance(target.position, transform.position) <= attackRadius && canDash)
            {
                StartCoroutine(PerformDash());
            }
        }
    }
    
    private IEnumerator PerformDash() 
    {
        canDash = false;
        
        Vector2 dashDirection = (target.position - transform.position).normalized;
        float dashDuration = 0.5f; 
        float dashSpeed = moveSpeed * 4.0f;

        float dashTimer = 0f;

        while (dashTimer < dashDuration) 
        {
            transform.position += (Vector3)dashDirection * dashSpeed * Time.deltaTime;
            dashTimer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true; 
    }
    
    private void TakeDamage() 
    {
        if (health > 0 && bulletScript != null)
        {
            health -= bulletScript.damage;
            healthBar.UpdateHealthBar(health, maxHealth);
            SoundManager.Instance.PlaySound(hitSFX);
        }
        
        if (health <= 0) 
        {
            Die();
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);

        if (gameMaster != null)
        {
            gameMaster.AddEnemiesKilled(1);
        }

        SpawnCoins();
        SpawnFinalObject();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("KunaiBullet")) 
        {
            bulletScript = collision.collider.GetComponent<BulletScript>();
            TakeDamage();
        }
    }

    private void SpawnCoins() 
    {
        const int numCoins = 10;
        const float spawnRadius = 2.0f;

        for (int i = 0; i < numCoins; i++) 
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0.0f);

            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnSkullsCoroutine() 
    {
        const float spawnInterval = 3.0f;

        while (true) 
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnSkull();
        }
    }

    private void SpawnSkull() 
    {
        float spawnRadius = 3.0f;

        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0.0f);

        GameObject skull = Instantiate(skullPrefab, spawnPosition, Quaternion.identity);
        Enemy skullEnemy = skull.GetComponent<Enemy>();
        skullEnemy.homePosition = target;
    }

    private void SpawnFinalObject()
    {
        if (finalObject != null)
        {
            finalObject.SetActive(true);
        }
    }
}
