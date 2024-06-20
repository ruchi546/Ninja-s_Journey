using UnityEngine;

public class Kunai : Weapons 
{
    private GameObject kunaiPrefab;

    void Start() 
    {
        damage = 10;
        fireForce = 10f;
        timeBetweenShots = 0.3f;
        kunaiPrefab = Resources.Load<GameObject>("Assets/Prefabs/Kunai.prefab");

        bulletPrefab = kunaiPrefab;
        weaponSprite = Resources.Load<Sprite>("Assets/Sprites/Kunai.png");
    }
}