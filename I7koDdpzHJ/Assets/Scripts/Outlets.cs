using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlets : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Plug")
        {
            Debug.Log("Heck ya");
        }
        
        throw new NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Getting there");
        throw new NotImplementedException();
    }
}
