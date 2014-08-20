using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour 
{   
    public static float range = 158;
    
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void FixedUpdate () 
    {
        Collider[] cols  = Physics.OverlapSphere(transform.position, range); 
        List <Rigidbody> rbs = new List<Rigidbody>();
        
        foreach(Collider c in cols)
        {
            Rigidbody rb = c.attachedRigidbody;
            if(rb != null && rb != rigidbody)
            {
                
                if(rbs.Contains(rb))
                {
                    continue;
                }
                
                rbs.Add(rb);
                Vector3 offset = transform.position - c.transform.position;
                rb.AddForce( offset / offset.sqrMagnitude * rigidbody.mass);
            }
        }
    }
}