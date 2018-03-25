using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    [SerializeField] private GameObject thePlatform;
    [SerializeField] private Transform generationPoint;
    [SerializeField] private float distanceBetween;

    [SerializeField] private float platformWidth;

    void Start()
    {
        platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
    }


    void Update()
    {

        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);

            Instantiate(thePlatform, transform.position, transform.rotation);
        }

    }
}
 