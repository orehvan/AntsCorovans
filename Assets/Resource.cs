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

    protected virtual void AddResource()
    {
        
    }
}
