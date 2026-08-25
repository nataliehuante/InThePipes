using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationColliderUpdates : MonoBehaviour
{
    public PolygonCollider2D[] colliders; 
    private PolygonCollider2D currentCollider;

    void Start()
    {
        ActivateCollider(0); 
    }

    public void ActivateCollider(int index)
    {
        if (currentCollider != null)
        {
            currentCollider.enabled = false;
        }

        currentCollider = colliders[index];
        currentCollider.enabled = true;
    }
}
