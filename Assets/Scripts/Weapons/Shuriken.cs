using UnityEngine;

public class Shuriken : Weapons 
{
    private GameObject shurikenPrefab;

    void Start() 
    {
        damage = 1;
        fireForce = 10f;
        timeBetweenShots = 0.3f;
        shurikenPrefab = Resources.Load<GameObject>("Assets/Prefabs/Shuriken.prefab");
        
        bulletPrefab = shurikenPrefab;
        weaponSprite = Resources.Load<Sprite>("Assets/Sprites/Shuriken.png");
    }
}