using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoisonBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        PlayerController.Instance.playerResources.PoisonChanged += PoisonChanged;
    }

    private void PoisonChanged(int obj)
    {
        _text.SetText("P" + obj.ToString());
    }
}
