using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private Minigame1 minigame;

    public void Input(string color)
    {
        var result = minigame.CheckInput(color);
        if (result)
            gameObject.GetComponent<Image>().color = Color.green;
    }
}
