using UnityEngine;

public class BulletScript : MonoBehaviour 
{
    [SerializeField] public float damage;
    [SerializeField] private float fireForce;
    [SerializeField] private float timeBetweenShots;

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("KunaiBullet")) 
        {
            Destroy(gameObject);
        }
    }
}