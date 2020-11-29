using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOrient : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject planet; 
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    void LateUpdate()//If I used a different rotation method (set not time based )then change to start
    {
        planet.GetComponent<GravObject>().OrientPlayer(rb);
    }
}
