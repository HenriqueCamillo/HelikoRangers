using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChest : Chest
{
    [SerializeField] private float healAmount;

    public override void Interact(Interactor interactor)
    {
        Entity entity = interactor.GetComponent<Entity>();
        if (entity == null)
            return;

        entity.Life += healAmount;

        base.Interact(interactor);
    }
}
