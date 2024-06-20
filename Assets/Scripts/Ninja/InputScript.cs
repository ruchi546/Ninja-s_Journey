using UnityEngine;

public class InputScript : MonoBehaviour 
{
    /// <summary>
    /// Detects user input for movement and returns a normalized movement vector.
    /// </summary>
    /// <returns>A Vector3 representing the movement direction.</returns>
    public Vector3 DetectInput() 
    {
        // Read input values
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Create a movement vector based on input
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        // Normalize the movement vector to ensure consistent speed in all directions
        return movement.normalized;
    }
}