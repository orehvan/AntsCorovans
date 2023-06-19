using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private Minigame1 minigame;
    [SerializeField] private Color nativeColor;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Input(string color)
    {
        var result = minigame.CheckInput(color);
        if (result)
            image.color = Color.green;
    }

    public void Reset()
    {
        image.color = nativeColor;
    }
}
