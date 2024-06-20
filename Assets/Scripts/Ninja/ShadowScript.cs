using UnityEngine;

public class ShadowScript : MonoBehaviour 
{
    [SerializeField] private GameObject ninja;

    void Update() 
    {
        if (ninja != null) 
        {
            Vector3 position = transform.position;
            position.x = ninja.transform.position.x;
            position.y = ninja.transform.position.y - 0.37f;
            transform.position = position;
        }
    }
}