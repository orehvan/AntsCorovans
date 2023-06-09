using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        PlayerController.Instance.playerResources.FireChanged += FireChanged;
    }

    private void FireChanged(int obj)
    {
        _text.SetText("F" + obj.ToString());
    }
}
