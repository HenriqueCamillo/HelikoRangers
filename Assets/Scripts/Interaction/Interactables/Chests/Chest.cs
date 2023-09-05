using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ProximityInteractable
{
    public override void Interact(Interactor interactor)
    {
        Destroy(this.gameObject);
    }
}