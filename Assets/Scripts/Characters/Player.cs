using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : GunCharacter
{
    private Vector2 moveInput;
    private Vector2 aimDirection;

    private Dictionary<AmmoType, GunTemplate> Guns;



    // protected override void Awake()
    // {
    //     base.Awake();
    // }

    // Update is called once per frame
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
        
        TryToInteract();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            gun.StartShooting();
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

    }

    public void SwitchGun(InputAction.CallbackContext context)
    {

    }

    public void UseSpecialAbility(InputAction.CallbackContext context)
    {
        
    }

    public void FocusAim(InputAction.CallbackContext context)
    {

    }


    private void TryToInteract()
    {
        
    }

    private void UpdateAimDirection(Vector2 newDirection)
    {
        aimDirection = newDirection;
        UpdateGunRotation(aimDirection);
    }
}
