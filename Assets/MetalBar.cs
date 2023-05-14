using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetalBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        PseudoPlayer.Instance.playerResources.MetalChanged += MetalChanged;
    }

    private void MetalChanged(int obj)
    {
        _text.SetText(obj.ToString());
    }
}
