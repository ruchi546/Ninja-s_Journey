using UnityEngine;

public class FlameEnemy : Enemy 
{
    [Header("Attack Properties")]
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireForce;
    [SerializeField] private float timeBetweenShots;
    
    private float timeOfLastShot;
    private GameObject bullet;

    private void Update() 
    {
        if ( target == null)
        {
            return;
        }
        
        CheckDistance();
    }

    private void FixedUpdate() 
    {
        PerformMovement();
    }

    private void PerformMovement() 
    {
        target = GameObject.FindWithTag("Player").transform;
        if (target == null || homePosition == null || rb == null)
        {
            return;
        }
        
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) {
            movement = (target.position- transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("isWalking", true);

        }
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);
            animator.SetBool("isWalking", false);
        }
    }

    private void CheckDistance() 
    {
        if (Vector2.Distance(target.position, transform.position) <= attackRadius) 
        {
            if (Time.time - timeOfLastShot >= timeBetweenShots) 
            {
                Shoot();
                timeOfLastShot = Time.time;
            }
        }
    }

    private void Shoot() 
    {
        Vector2 direction = (target.position - firePoint.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle-90f);
        bullet = Instantiate(bulletPrefab, firePoint.transform.position, rotation);
        bullet.transform.rotation = rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * fireForce;

        Destroy(bullet, 5f);
    }
}