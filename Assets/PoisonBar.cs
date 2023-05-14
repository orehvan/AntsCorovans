using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoisonBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        PseudoPlayer.Instance.playerResources.PoisonChanged += PoisonChanged;
    }

    private void PoisonChanged(int obj)
    {
        _text.SetText(obj.ToString());
    }
}
