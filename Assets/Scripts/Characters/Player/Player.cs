using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : GunCharacter
{
    [Space(5)]
    [Header("Player Specific")]
    [SerializeField] private PlayerState playerState;

    private int ammoInGunNotBeingUsed = -1;

    private Vector2 moveInput;
    private Vector2 aimDirection;


    protected override void Awake()
    {
        base.Awake();

        if (playerState == null)
            playerState = GetComponent<PlayerState>();
    }

    void Update()
    {
        rigidBody.velocity = moveInput * speed;
        animator.SetBool("IsMoving", moveInput != Vector2.zero);
        if (moveInput.x != 0.0f)
            IsLookingLeft = moveInput.x < 0.0f;
    }

    public void Move(InputAction.CallbackContext context) 
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context) 
    {
        if (!context.performed)
            return;
        
        interactor.Interact();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            if (isReloading && gun.AmmoInClip > 0)
                CancelReload();
                
            gun.StartShooting();
        }
        else if (context.canceled)
            gun.StopShooting();
    }


    public void MouseAim(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 aimDirection = mouseWorldPosition - this.transform.position;

        UpdateAimDirection(aimDirection);
    }

    public void StickAim(InputAction.CallbackContext context)
    {
        Vector2 stickDirection = context.ReadValue<Vector2>();
        if (stickDirection != Vector2.zero)
            UpdateAimDirection(stickDirection);
        else if (moveInput != Vector2.zero)
            UpdateAimDirection(moveInput);
        else
            UpdateAimDirection(IsLookingLeft ? Vector2.left : Vector2.right);
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed)
            TryToReload();
    }

    protected override int GetRemainingAmmo(AmmoType ammoType)
    {
        return playerState.GetAmmo(ammoType);
    }

    protected override void UseAmmo(AmmoType ammoType, int value)
    {
        playerState.SetAmmo(ammoType, playerState.GetAmmo(ammoType) - value);
    }

    public void SwitchGun(InputAction.CallbackContext context)
    {
        if (playerState.GunNotBeingUsed == null)
            return;

        if (isReloading)
            CancelReload();

        int ammoInNextGun = ammoInGunNotBeingUsed;
        ammoInGunNotBeingUsed = gun.AmmoInClip;

        gun.Template = playerState.GunNotBeingUsed;
        gun.AmmoInClip = ammoInNextGun;

        playerState.SwitchGuns();
        gun.Template = playerState.CurrentGun;
    }

    public void UseSpecialAbility(InputAction.CallbackContext context)
    {
        
    }

    public void FocusAim(InputAction.CallbackContext context)
    {

    }

    private void UpdateAimDirection(Vector2 newDirection)
    {
        aimDirection = newDirection;
        UpdateGunRotation(aimDirection);
    }

    public override void PickUpDroppedGun(GunCollectable gunCollectable)
    {
        bool pickingSecondGun = playerState.CurrentGun != null && playerState.GunNotBeingUsed == null;

        if (!pickingSecondGun)
        {
            base.PickUpDroppedGun(gunCollectable);
            playerState.ReplaceCurrentGun(gunCollectable.GunTemplate);
            return;
        }

        playerState.AddGun(gunCollectable.GunTemplate);
        ammoInGunNotBeingUsed = gunCollectable.AmmoInClip;
        Destroy(gunCollectable.gameObject);
    }
}
