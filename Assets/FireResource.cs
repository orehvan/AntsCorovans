using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireResource : Resource
{
    protected override void AddResource()
    {
        PlayerController.Instance.playerResources.FireResource += value;
    }
}
