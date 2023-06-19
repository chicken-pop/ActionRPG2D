using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;

    private void Start()
    {

        //length = GetComponent<SpriteRenderer>().bounds.size.x;
        length = 40;
        xPosition = transform.position.x;
    }

    private void Update()
    {
        float distanceMoved = Camera.main.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = Camera.main.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove,transform.position.y);

        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }
        
        
        
    }
}
