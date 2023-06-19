using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wire : MonoBehaviour
{
    [SerializeField] private Minigame2 minigame2;
    public string color;
    private Image image;
    private LineRenderer lr;
    private Button button;
    private bool done;
    private Vector2 lastPos;
    private Transform fixedPos;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        image = GetComponent<Image>();
        lr = GetComponent<LineRenderer>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Input);
        // canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (minigame2.isWiring && !done)
        {
            var rect = canvas.transform as RectTransform;
            var screenToWorldPosition = Camera.main.ScreenToWorldPoint(transform.position);
            screenToWorldPosition.z = 100;
            lr.SetPosition(0, screenToWorldPosition);
            var screenToWorldPosition2 = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            screenToWorldPosition2.z = 100;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,
                UnityEngine.Input.mousePosition, canvas.worldCamera, out lastPos);
            
            lr.SetPosition(1, screenToWorldPosition2);
        }
        else if (done && fixedPos is not null)
        {
            var screenToWorldPosition = Camera.main.ScreenToWorldPoint(transform.position);
            screenToWorldPosition.z = 100;
            lr.SetPosition(0, screenToWorldPosition);
            var screenToWorldPosition2 = Camera.main.ScreenToWorldPoint(fixedPos.position);
            screenToWorldPosition2.z = 100;
            lr.SetPosition(1, screenToWorldPosition2);
        }
    }

    public void Input()
    {
        if (!minigame2.isWiring && !done)
        {
            minigame2.source = gameObject;
            minigame2.isWiring = true;
            minigame2.wiringColor = color;
            lr.enabled = true;
            lr.material.color = image.color;
        }
        else if (minigame2.isWiring)
        {
            if (color == minigame2.wiringColor && minigame2.source != gameObject && !done)
            {
                var result = minigame2.CheckInput(color);
                if (result)
                {
                    done = true;
                    minigame2.source.GetComponent<Wire>().fixedPos = transform;
                    minigame2.source.GetComponent<Wire>().done = true;
                }
                
            }
        }
    }
    public void ColorWire()
    {
        if (color == "red")
            image.color = Color.red;
        if (color == "blue")
            image.color = Color.blue;
        if (color == "yellow")
            image.color = Color.yellow;
        if (color == "purple")
            image.color = Color.magenta;
    }
}
