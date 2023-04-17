using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private float flySpeed = 10f;
    [SerializeField] private float maxFlyDistance = 10f;
    private float damage;
    private Vector3 startPos;
    private Vector2 direction;

    void Start()
    {
        startPos = transform.position;
        direction = transform.up * 10;
    }
    
    void Update()
    {
        BulletFly();
    }
    
    private void BulletFly()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, flySpeed * Time.deltaTime);
        if ((transform.position - startPos).magnitude > maxFlyDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null) return;
        enemy.GetDamage(damage);
        Destroy(gameObject);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
