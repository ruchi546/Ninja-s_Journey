using UnityEngine;

public class Weapons : MonoBehaviour 
{
    protected float damage;
    protected float fireForce;
    protected float timeBetweenShots;

    public string type;
    
    public GameObject bulletPrefab;
    public Sprite weaponSprite;
}