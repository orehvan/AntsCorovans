using System.Collections;
using System.Collections.Generic;
using DTerrain;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Vector3 TargetPosition;
    private bool isMoving;
    
    [SerializeField] protected BasicPaintableLayer primaryLayer;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        if (isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, TargetPosition - transform.position);
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, speed * Time.deltaTime);
        Debug.Log($"Mouse pos: {TargetPosition}");

        if (transform.position == TargetPosition) isMoving = false;
    }

    private void SetTargetPosition()
    {
        TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TargetPosition.z = transform.position.z;

        isMoving = true;
    }
}
