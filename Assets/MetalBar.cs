using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetalBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        PlayerController.Instance.playerResources.MetalChanged += MetalChanged;
    }

    private void MetalChanged(int obj)
    {
        _text.SetText("M" + obj.ToString());
    }
}
