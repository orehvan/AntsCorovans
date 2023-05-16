using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    [SerializeField] protected int value;

    private void OnMouseDown(){
        AddResource();
        Destroy(gameObject);
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<PlayerController>()) return;
        AddResource();
        Destroy(gameObject);
    }

    protected virtual void AddResource()
    {
        
    }
}
