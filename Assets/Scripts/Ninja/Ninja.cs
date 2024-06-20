using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ninja : MonoBehaviour 
{
    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 movement;
    private Vector2 mousePosition;

    private Rigidbody2D rigidBody;
    private Animator animator;

    [Header("Scripts")]
    [SerializeField] private InputScript inputScript;

    [Header("Attack")]
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireForce;
    [SerializeField] private GameObject weaponGameObject;
    [SerializeField] private GameObject weaponSpriteObject;
    [SerializeField] private float timeBetweenShots;
    private float timeOfLastShot;
    private GameObject bullet;

    [Header("Bullet prefabs")]
    [SerializeField] private Sprite shurikenSprite;
    [SerializeField] private GameObject shurikenPrefab;
    [SerializeField] private Sprite bowSprite;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Sprite kunaiSprite;
    [SerializeField] private GameObject kunaiPrefab;

    [Header("SFX")]
    public AudioClip pickWeaponSFX;
    public AudioClip healSFX;
    public AudioClip hitSFX;
    public AudioClip bulletSFX;

    private GameMaster gameMaster;
    private PauseMenu pauseMenu;
    
    private void Start() 
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        pauseMenu = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseMenu>();
    }

    private void FixedUpdate() 
    {
        speed = ChangeSpeedOnHealth();
        MovementProcess();
    }

    private void Update() 
    {
        Weapon();

        if (Input.GetMouseButtonDown(0) && pauseMenu.GetIsInPause() == false)
        {
            if (Time.time - timeOfLastShot >= timeBetweenShots) 
            {
                if (gameMaster.currentWeapon == 2) 
                {
                    ShootKunai();
                }
                else 
                {
                    Shoot();
                }
                
                timeOfLastShot = Time.time;
            }
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - rigidBody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        weaponGameObject.GetComponent<Rigidbody2D>().rotation = aimAngle;
        firePoint.GetComponent<Transform>().position = weaponGameObject.GetComponent<Transform>().position + (weaponGameObject.GetComponent<UnityEngine.Transform>().up * 0.3f);
    }

    private void MovementProcess() 
    {
        movement = inputScript.DetectInput();

        if (movement != Vector3.zero) 
        {
            rigidBody.MovePosition(transform.position + movement * speed * Time.deltaTime);

            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("isWalking", true);

            weaponGameObject.GetComponent<Transform>().position = rigidBody.position;
            firePoint.GetComponent<Transform>().position = weaponGameObject.GetComponent<Transform>().position + (weaponGameObject.GetComponent<Transform>().up * 0.3f);
        }
        else 
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void Shoot() 
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePosition.y - rigidBody.position.y, mousePosition.x - rigidBody.position.x) * Mathf.Rad2Deg);
        
        bullet = Instantiate(bulletPrefab, firePoint.GetComponent<Transform>().position, rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.GetComponent<Transform>().up * fireForce, ForceMode2D.Impulse);

        SoundManager.Instance.PlaySound(bulletSFX);

        Destroy(bullet, 5f);
    }

    private void ShootKunai() 
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePosition.y - rigidBody.position.y, mousePosition.x - rigidBody.position.x) * Mathf.Rad2Deg);
        StartCoroutine(ShootBulletsWithDelay(rotation));
    }
    

    private IEnumerator ShootBulletsWithDelay(Quaternion rotation) {
        for (int i = 0; i < 3; i++) {
            bullet = Instantiate(bulletPrefab, firePoint.GetComponent<UnityEngine.Transform>().position, rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.GetComponent<UnityEngine.Transform>().up * fireForce, ForceMode2D.Impulse);
            SoundManager.Instance.PlaySound(bulletSFX);
            Destroy(bullet, 5f);

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Die() 
    {
        animator.SetBool("isDead", true);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        Invoke("Respawn", 2f);
    }

    private void Respawn() 
    {
        Destroy(this.gameObject);
        gameMaster.health = 3;
        gameMaster.points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private float ChangeSpeedOnHealth() 
    {
        switch (gameMaster.health) 
        {
            case 2:
                return 4f;
            case 1:
                return 3f;
            default:
                return 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Shuriken")) 
        {
            gameMaster.currentWeapon = 1;
            Destroy(collision.gameObject);
            SoundManager.Instance.PlaySound(pickWeaponSFX);
        }
        if (collision.CompareTag("Bow")) 
        {
            gameMaster.currentWeapon = 0;
            Destroy(collision.gameObject);
            SoundManager.Instance.PlaySound(pickWeaponSFX);
        }
        if (collision.CompareTag("Kunai")) 
        {
            gameMaster.currentWeapon = 2;
            Destroy(collision.gameObject);
            SoundManager.Instance.PlaySound(pickWeaponSFX);
        }
    }

    private void Weapon()
    {
        switch (gameMaster.currentWeapon)
        {
            case 2:
                weaponSpriteObject.GetComponent<SpriteRenderer>().sprite = kunaiSprite;
                bulletPrefab = kunaiPrefab;
                break;
            case 1:
                weaponSpriteObject.GetComponent<SpriteRenderer>().sprite = shurikenSprite;
                bulletPrefab = shurikenPrefab;
                break;
            case 0:
                weaponSpriteObject.GetComponent<SpriteRenderer>().sprite = bowSprite;
                bulletPrefab = arrowPrefab;
                break;
        }
    }
}