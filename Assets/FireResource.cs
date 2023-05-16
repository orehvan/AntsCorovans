using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireResource : Resource
{
    protected override void AddResource()
    {
        PseudoPlayer.Instance.playerResources.FireResource += value;
    }
}
