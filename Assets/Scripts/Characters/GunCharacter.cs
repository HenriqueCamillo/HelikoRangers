using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCharacter : Entity
{
    [Header("Internal Component References")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected Animator animator;
    [Space(5)]

    [Header("External Component References")]
    [SerializeField] protected Gun gun;
    [Space(5)]

    [Header("General Settings")]
    [SerializeField] protected float speed;
    [SerializeField] private bool isLookingLeft;
    [SerializeField] private LayerMask damageLayers;

    protected bool IsLookingLeft
    {
        get => isLookingLeft;
        set 
        {
            isLookingLeft = value;
            spriteRenderer.flipX = isLookingLeft;
        }
    }

    protected virtual void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        gun.DamageLayers = damageLayers;
    }

    protected virtual void UpdateGunRotation(Vector2 direction)
    {
        gun.transform.right = direction;
        gun.FlipSprite(direction.x < 0.0f);
    }
}
