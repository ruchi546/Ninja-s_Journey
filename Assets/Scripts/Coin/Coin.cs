using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private AudioClip coinSFX;
    
    public static event Action<int> OnCoinCollected;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player")) 
        {
            OnCoinCollected?.Invoke(value);

            // Play the coin sound effect
            if (coinSFX != null)
            {
                SoundManager.Instance.PlaySound(coinSFX);
            }
            
            Destroy(gameObject);
        }
    }
}
