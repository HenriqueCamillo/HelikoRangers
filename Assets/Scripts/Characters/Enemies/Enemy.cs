using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GunCharacter
{
    private GameObject target;    

    void Update()
    {
        if (target != null)
            UpdateGunRotation(target.transform.position);
        else
            UpdateGunRotation(this.transform.position + this.transform.forward);

    }
}
