using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public GameObject[] gameObjectsToAnimate;
    public Sprite[] animationSprites;
    public float cooldown = 25f;
    public float animationSpeed = 0.05f;
    private bool isCooldown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Trigger Entered");
        if(collision.CompareTag("Player") && !isCooldown)
        {
            StartCoroutine(ActivateObjects());
            isCooldown = true;
            Invoke(nameof(ResetCooldown), cooldown);
        }
    }

    IEnumerator ActivateObjects()
    {
        foreach (var obj in gameObjectsToAnimate)
        {
            obj.SetActive(true);
            StartCoroutine(AnimateObject(obj));
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f)); // Delay to desynchronize animations
        }
    }

    IEnumerator AnimateObject(GameObject obj)
    {
        // Debug.Log("Animating Object: " + obj.name);
        foreach (var sprite in animationSprites)
        {
            obj.GetComponent<SpriteRenderer>().sprite = sprite;
            yield return new WaitForSeconds(animationSpeed); // Adjust as needed for animation speed
        }
        obj.SetActive(false);
    }

    void ResetCooldown()
    {
        isCooldown = false;
    }
}
