using UnityEngine;

/// <summary>
/// Holds the basic movement of the player.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameManager.SaySomething();
        }
    }
}
