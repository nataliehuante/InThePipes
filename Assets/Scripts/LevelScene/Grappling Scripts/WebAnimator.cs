using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAnimator : MonoBehaviour
{
    [SerializeField] 
    public Transform target;

    [SerializeField]
    public int resolution;
    public int waveCount;
    public int wobbleCount;

    [SerializeField]
    public float waveSize;
    public float animSpeed;

    public LineRenderer lineRenderer;
    public bool isAnimating = false;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    public IEnumerator AnimateWeb(Vector3 targetPos) {
        isAnimating = true;
        lineRenderer.positionCount = resolution;

        float percent = 0; // will go from 0 to 1 over time
        while (percent <= 1f) {
            percent += Time.deltaTime * animSpeed;
            SetPoints(targetPos, percent);
            yield return null;
        }

        // makes sure that the line straightens out at the end of the animation
        SetPoints(targetPos, 1);
        isAnimating = false;
    }

    public void SetPoints(Vector3 targetPos, float percent) {
        // current end of the web at this frame
        Vector3 ropeEnd = Vector3.Lerp(transform.position, targetPos, percent);

        // current length of the web at this frame
        float length = Vector2.Distance(transform.position, ropeEnd);

        for (int i = 0; i < resolution; i++) {
            float xPos = (float) i / resolution * length;
            float reversePercent = (1 - percent);

            float amplitude = Mathf.Sin(reversePercent * wobbleCount * Mathf.PI);

            float yPos = Mathf.Sin((float) waveCount * i / resolution * 2 * Mathf.PI * reversePercent);

            Vector2 pos = new Vector2(xPos, yPos);
            lineRenderer.SetPosition(i, pos);
        }
    }



}
