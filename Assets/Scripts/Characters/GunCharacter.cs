using System;
using System.Collections.Generic;
using UnityEngine;

public class GunCharacter : Entity
{
    [Header("Internal Component References")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Interactor interactor;

    [Space(5)]
    [Header("External Component References")]
    [SerializeField] protected Gun gun;

    [Space(5)]
    [Header("General Settings")]
    [SerializeField] protected float speed;
    [SerializeField] private bool isLookingLeft;
    [SerializeField] private LayerMask damageLayers;

    private Coroutine reloadCoroutine;
    protected bool isReloading;

    protected bool IsLookingLeft
    {
        get => isLookingLeft;
        set 
        {
            isLookingLeft = value;
            spriteRenderer.flipX = isLookingLeft;
        }
    }

    public int GetAmmoInCurrentGun => gun != null ? gun.AmmoInClip : 0;
    public int GetCurrentGunAmmoCapacity => gun != null ? gun.Template.AmmoClipCapacity : 0;

    protected virtual void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (interactor == null)
            interactor.GetComponent<Interactor>();

        IsLookingLeft = isLookingLeft;

        gun.DamageLayers = damageLayers;

        gun.OnEmptyAmmoClip += TryToReload;
    }

    protected virtual void UpdateGunRotation(Vector2 direction)
    {
        gun.transform.right = direction;
        gun.FlipSprite(direction.x < 0.0f);
    }

    public virtual void PickUpDroppedGun(GunCollectable gunCollectable)
    {
        GunTemplate gunToBeReplaced = gun.Template;        
        int currentAmmoInClip = gun.AmmoInClip;

        gun.Template = gunCollectable.GunTemplate;
        gun.AmmoInClip = gunCollectable.AmmoInClip;

        if (gunToBeReplaced != null)
        {
            gunCollectable.GunTemplate = gunToBeReplaced;
            gunCollectable.AmmoInClip = currentAmmoInClip;
        }
        else
        {
            Destroy(gunCollectable.gameObject);
        }
    }
    
    protected virtual void TryToReload()
    {
        if (isReloading || gun.IsInCooldown)
            return;

        if (gun.Template == null)
            return;

        if (gun.AmmoInClip >= gun.Template.AmmoClipCapacity)
            return;

        if (GetRemainingAmmo(gun.Template.AmmoType) <= 0)
            return;

        // TODO: Play animation and use event
        reloadCoroutine = StartCoroutine(ApplyReload());
    }

    protected virtual int GetRemainingAmmo(AmmoType ammoType)
    {
        return int.MaxValue;
    }

    protected virtual void UseAmmo(AmmoType ammoType, int value)
    {

    }

    protected virtual IEnumerator<WaitForSeconds> ApplyReload()
    {
        gun.StopShooting();

        isReloading = true;

        // TODO: Animations
        gun.SetColor(Color.red);
        yield return new WaitForSeconds(gun.Template.ReloadTime);
        gun.SetColor(Color.white);

        int ammoUntilFull = gun.Template.AmmoClipCapacity - gun.AmmoInClip;
        int ammoToAdd = Mathf.Min(ammoUntilFull, GetRemainingAmmo(gun.Template.AmmoType));

        UseAmmo(gun.Template.AmmoType, ammoToAdd);
        gun.AmmoInClip += ammoToAdd;

        reloadCoroutine = null;
        isReloading = false;
    }

    protected void CancelReload()
    {
        if (reloadCoroutine == null)
            return;

        StopCoroutine(reloadCoroutine);
        gun.SetColor(Color.white);
        isReloading = false;
    }
}
