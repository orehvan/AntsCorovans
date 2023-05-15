using System;
using System.Collections;
using System.Collections.Generic;
using DTerrain;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [FormerlySerializedAs("DestroyCircleSize")] public int destroyCircleSize;

    private Shape DestroyCircle;
    private Vector3 TargetPosition;
    private bool isMoving;
    
    [SerializeField] protected BasicPaintableLayer primaryLayer;
    [SerializeField] protected BasicPaintableLayer secondaryLayer;

    private void Start()
    {
        // DestroyCircleSize = (int)Math.Ceiling(GetComponent<SpriteRenderer>().bounds.size.x);
        // destroyCircleSize = 40;
        DestroyCircle = Shape.GenerateShapeCircle(destroyCircleSize);
        
        Debug.Log($"Circle size: {destroyCircleSize}");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        if (!isMoving) return;
        Move();
        DestroyTerrain();
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

    private void DestroyTerrain()
    {
        primaryLayer?.Paint(new PaintingParameters()
        {
            Color = Color.clear,
            Position = new Vector2Int((int)(transform.position.x * primaryLayer.PPU) - destroyCircleSize,
                (int)(transform.position.y * primaryLayer.PPU) - destroyCircleSize),
            Shape = DestroyCircle,
            PaintingMode = PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.DESTROY
        });
        secondaryLayer?.Paint(new PaintingParameters()
        {
            Color = Color.clear,
            Position = new Vector2Int((int)(transform.position.x * secondaryLayer.PPU) - destroyCircleSize,
                (int)(transform.position.y * secondaryLayer.PPU) - destroyCircleSize),
            Shape = DestroyCircle,
            PaintingMode = PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.DESTROY
        });
    }
}
