using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystemMain;
    [SerializeField] ParticleSystem particleSystemSecondary;

    void Update()
    {
        if(particleSystemMain == true && Input.GetKey(KeyCode.K))
        {
            particleSystemMain.Stop();
            particleSystemSecondary.Play();
        }
        else if(Input.GetKey(KeyCode.K)) 
        {
            particleSystemMain.Play();
            particleSystemSecondary.Stop();
        }
    }
}
