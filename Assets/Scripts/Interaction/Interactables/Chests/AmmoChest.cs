using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoChest : Chest
{
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int ammoQuantity;

    public override void Interact(Interactor interactor)
    {
        PlayerState playerState = interactor.GetComponent<PlayerState>();
        if (playerState == null)
            return;

        playerState.SetAmmo(ammoType, playerState.GetAmmo(ammoType) + ammoQuantity);


        base.Interact(interactor);
    }
}

