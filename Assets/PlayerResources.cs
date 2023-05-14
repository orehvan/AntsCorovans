using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    private int fireResource;
    private int poisonResource;
    private int metalResource;

    public int FireResource
    {
        get => fireResource;
        set
        {
            fireResource = value;
            FireChanged?.Invoke(fireResource);
        }
    }

    public int PoisonResource
    {
        get => poisonResource;
        set
        {
            poisonResource = value;
            PoisonChanged?.Invoke(poisonResource);
        }
    }

    public int MetalResource
    {
        get => metalResource;
        set
        {
            metalResource = value;
            MetalChanged?.Invoke(metalResource);
        }
    }
    
    public event Action<int> PoisonChanged;

    public event Action<int> FireChanged;

    public event Action<int> MetalChanged;

    public bool HasEnoughResources((int firePrice, int poisonPrice, int metalPrice) price)
    {
        return FireResource >= price.firePrice && PoisonResource >= price.poisonPrice &&
                MetalResource >= price.metalPrice;
    }

    public void Pay((int firePrice, int poisonPrice, int metalPrice) price)
    {
        FireResource -= price.firePrice;
        PoisonResource -= price.poisonPrice;
        MetalResource -= price.metalPrice;
    }
}
