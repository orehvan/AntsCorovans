using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonResource : Resource
{
    protected override void AddResource()
    {
        PseudoPlayer.Instance.playerResources.PoisonResource += value;
    }
}
