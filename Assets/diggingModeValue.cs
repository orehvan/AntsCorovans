using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class diggingModeValue : MonoBehaviour
{
    private TextMeshProUGUI text;
    private bool digMode;
    private const string NotDigging = "Not Digging";
    private const string Digging = "Digging";

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        PlayerController.Instance.DiggingModeChanged += ChangeDigText;
    }

    private void ChangeDigText()
    {
        digMode = !digMode;
        if (digMode)
            text.text = Digging;
        else
            text.text = NotDigging;
    }
    
}
