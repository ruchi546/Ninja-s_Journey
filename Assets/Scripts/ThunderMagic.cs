using UnityEngine;

public class ThunderMagic : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        gameObject.SetActive(false);
    }
}
