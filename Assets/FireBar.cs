using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        PseudoPlayer.Instance.playerResources.FireChanged += FireChanged;
    }

    private void FireChanged(int obj)
    {
        _text.SetText(obj.ToString());
    }
}
