using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalResource : Resource
{
    protected override void AddResource()
    {
        PseudoPlayer.Instance.playerResources.MetalResource += value;
    }
}
