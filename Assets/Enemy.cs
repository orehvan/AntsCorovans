using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Vector2 directionForward = Vector2.right * 7;
    private Vector2 directionBackward = Vector2.left * 7;
    private Vector2 currentDirection;
    private float distance;
    void Start()
    {
        currentDirection = directionForward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, currentDirection) < 0.01f)
            currentDirection = currentDirection == directionForward ? directionBackward : directionForward;
        transform.position = Vector2.MoveTowards(transform.position, currentDirection, speed * Time.deltaTime);
    }

    public void GetDamage(float damage)
    {
        Debug.Log($"Got {damage} damage");
    }
}
