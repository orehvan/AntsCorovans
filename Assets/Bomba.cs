using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    [SerializeField] private float flySpeed = 5f;
    [SerializeField] private float maxFlyDistance = 5f;
    [SerializeField] private float radius = 5f;
    private ParticleSystem boom;
    private SpriteRenderer sr;
    [SerializeField] private float damage = 5f;
    private Vector3 startPos;
    private Vector2 direction;

    void Start()
    {
        startPos = transform.position;
        direction = startPos + transform.up * maxFlyDistance;
        boom = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        BulletFly();
    }
    
    private void BulletFly()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, flySpeed * Time.deltaTime);
        if (maxFlyDistance - (transform.position - startPos).magnitude <= 0.01f)
            StartCoroutine(DestroyAfterBoom());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        flySpeed = 0f;
        var enemies = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var enemyCol in enemies)
        { 
            var enemy = enemyCol.GetComponent<AbstractEnemy>();
            if (enemy != null)
                enemy.GetDamage(damage);
        }

        StartCoroutine(DestroyAfterBoom());
    }

    IEnumerator DestroyAfterBoom()
    {
        boom.Play();
        sr.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
