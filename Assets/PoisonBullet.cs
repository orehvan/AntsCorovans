using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PoisonBullet : MonoBehaviour
{
    [SerializeField] private float flySpeed = 10f;
    [SerializeField] private float maxFlyDistance = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float poisonDamage = 3f;
    [SerializeField] private int poisonTicks = 3;
    [SerializeField] private float slownessCoef = 0.8f;
    private bool hit;
    private SpriteRenderer sr;
    private Vector3 startPos;
    private Vector2 direction;

    void Start()
    {
        startPos = transform.position;
        direction = startPos + transform.up * maxFlyDistance;
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        BulletFly();
    }
    
    private void BulletFly()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, flySpeed * Time.deltaTime);
        if (maxFlyDistance - (transform.position - startPos).magnitude <= 0.01f && !hit)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hit = true;
        var enemy = other.gameObject.GetComponent<AbstractEnemy>();
        if (enemy == null) return;
        enemy.GetDamage(damage);
        StartCoroutine(PoisonDamage(enemy));
    }

    IEnumerator PoisonDamage(AbstractEnemy enemy)
    {
        sr.enabled = false;
        for (var i = 0; i < poisonTicks; i++)
        {
            if (enemy == null)
            {
                Destroy(gameObject);
                yield break;
            }

            enemy.GetDamage(poisonDamage);
            enemy.SetSlowness(slownessCoef);
            yield return new WaitForSeconds(1f);
        }
        if (enemy == null)
        {
            Destroy(gameObject);
            yield break;
        }
        enemy.SetSlowness(1/slownessCoef);
        Destroy(gameObject);
    }
}
