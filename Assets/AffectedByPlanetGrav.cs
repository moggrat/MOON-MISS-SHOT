using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedByPlanetGrav : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject planet; 
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void LateUpdate()//If I used a different rotation method (set not time based )then change to start
    {
        planet.GetComponent<GravObject>().attractGPull(gameObject);    
    }
}
