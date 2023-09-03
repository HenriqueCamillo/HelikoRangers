using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Entity entity;

    public void ApplyDamange(float damage)
    {
        entity.Life -= damage;
    }
}
