using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalResource : Resource
{
    protected override void AddResource()
    {
        PlayerController.Instance.playerResources.MetalResource += value;
    }
}
