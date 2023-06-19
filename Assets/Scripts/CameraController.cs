using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        var position = playerTransform.position;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}