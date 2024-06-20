using UnityEngine;
using System;

public class Enemy : MonoBehaviour 
{
    [Header("Main Properties")]
    [SerializeField] public Transform target; 
    [SerializeField] protected float chaseRadius;
    [SerializeField] protected float attackRadius;
    [SerializeField] public Transform homePosition;

    [Header("Movement")]
    protected Vector3 movement;
    protected Animator animator;

    [Header("Statistics")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float moveSpeed;

    [Header("SFX")]
    public AudioClip hitSFX;
    public AudioClip dieSFX;

    protected Rigidbody2D rb;
    private BulletScript bulletScript;
    private GameMaster gameMaster;
    
    public static event Action<int> OnAddCoin;
    
    void Start() 
    {
        target = GameObject.FindWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
    
    private void TakeDamage() 
    {
        if (bulletScript == null)
        {
            return;
        }
        
        if (maxHealth > 0) 
        {
            maxHealth -= bulletScript.damage;
            SoundManager.Instance.PlaySound(hitSFX);
        }
        
        if (maxHealth <= 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameMaster != null)
        { 
            OnAddCoin?.Invoke(1);
            gameMaster.AddEnemiesKilled(1);
        }

        if (dieSFX != null)
        {
            SoundManager.Instance.PlaySound(dieSFX);
        }

        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("KunaiBullet")) 
        {
            bulletScript = collision.collider.GetComponent<BulletScript>();
            TakeDamage();
        }
        if(collision.collider.CompareTag("Player") && this.gameObject.CompareTag("Tree")) 
        {
            Destroy(this.gameObject);
        }
    }
}