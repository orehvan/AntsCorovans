using System;
using Mono.Cecil;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class AbstractTurret : MonoBehaviour
    {
        [SerializeField] private int firePrice;
        [SerializeField] private int poisonPrice;
        [SerializeField] private int metalPrice;
        public Sprite turretImage;
        public GameObject turretBaseImage;
        
        public BoxCollider2D turretCollider;

        void Start()
        {
            turretCollider = gameObject.GetComponentInChildren<BoxCollider2D>();
        }

        public (int firePrice, int poisonPrice, int metalPrice) GetPrice()
        {
            return (firePrice, poisonPrice, metalPrice);
        }
    }
}