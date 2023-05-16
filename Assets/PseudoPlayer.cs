using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoPlayer : MonoBehaviour
{
    public PlayerResources playerResources;
    
    public static PseudoPlayer Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
