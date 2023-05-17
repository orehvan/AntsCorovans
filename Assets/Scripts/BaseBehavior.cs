using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseBehavior : MonoBehaviour
{
    public float totalHealth = 100f;
    public float currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        Debug.Log(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Under Attack");
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0) 
            gameObject.SetActive(false);
    }
}
