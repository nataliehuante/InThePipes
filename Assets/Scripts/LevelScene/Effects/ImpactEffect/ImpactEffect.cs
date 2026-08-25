using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    public bool isAnimationDone = false;

    // Update is called once per frame
    void Update()
    {
        if (isAnimationDone) {
            Destroy(gameObject);
        }
    }
}
