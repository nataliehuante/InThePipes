using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public GameObject gameObject;
    private void Start()
    {
  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Entered trigger with: " + other.name);
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(true); // Show the GameObject when the player enters the collider
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Exited trigger with: " + other.name);
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false); // Hide the GameObject when the player exits the collider
        }
    }
}