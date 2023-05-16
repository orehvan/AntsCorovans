using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfo : MonoBehaviour
{
    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI firePrice;
    [SerializeField] private TextMeshProUGUI poisonPrice;
    [SerializeField] private TextMeshProUGUI metalPrice;

    public void ShowTurretInfo(int firePrice, int poisonPrice, int metalPrice, Sprite turretImage)
    {
        this.firePrice.text = "F" + firePrice.ToString();
        this.poisonPrice.text = "P" + poisonPrice.ToString();
        this.metalPrice.text = "M" + metalPrice.ToString();
        Debug.Log(this.turretImage.sprite);
        Debug.Log(turretImage);
        this.turretImage.sprite = turretImage;
        
    }
}
