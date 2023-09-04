using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using Unity.VisualScripting;

public class Player : GunCharacter
{
    private Vector2 moveInput;
    private Vector2 aimDirection;

    private Dictionary<IInteractable, GameObject> possibleinteractables = new Dictionary<IInteractable, GameObject>();
    private List<IInteractable> interactablePriorityQueue = new List<IInteractable>();

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
        
        if (interactablePriorityQueue.Count == 0) 
            return;

        interactablePriorityQueue[0].Interact(this.gameObject);
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

    public void AddPossibleInteractable(IInteractable interactable, GameObject interactableObject)
    {
        if (possibleinteractables.ContainsKey(interactable))
            return;

        possibleinteractables.Add(interactable, interactableObject);
        RefreshInteractablePriorityQueue(interactable, true);
    }

    public void RemovePossibleInteractable(IInteractable interactable)
    {
        if (!possibleinteractables.ContainsKey(interactable))
            return;

        possibleinteractables.Remove(interactable);
        RefreshInteractablePriorityQueue(interactable, false);
    }

    // TODO: Make this better
    private void RefreshInteractablePriorityQueue(IInteractable interactable, bool wasAdded)
    {
        IInteractable activeInteractable = null;
        if (interactablePriorityQueue.Count > 0)
            activeInteractable = interactablePriorityQueue[0];

        if (wasAdded)
        {
            interactablePriorityQueue.Add(interactable);
        }
        else
        {
            for (int i = 0; i < interactablePriorityQueue.Count; i++)
            {
                if (interactablePriorityQueue[i] == interactable)
                {
                    interactablePriorityQueue.RemoveAt(i);
                    break;
                }
            }
        }

        if (interactablePriorityQueue.Count == 0)
            return;
        
        interactablePriorityQueue.OrderByDescending(interactable => (
            interactable.GetPriority() * 100000.0f -                      // Priority multiplied by big value so that it will always define the order when values are difference
            Mathf.Abs((possibleinteractables[interactable].transform.position - this.transform.position).sqrMagnitude)  // Decreases priority according to distance
        ));

        if (activeInteractable != null)
            activeInteractable.SetHintVisible(false);

        
        activeInteractable = interactablePriorityQueue[0];
        activeInteractable.SetHintVisible(true);
    }

    public void PickUpDroppedGun(GunCollectable gunCollectable)
    {
        GunTemplate gunToBeReplaced = gun.Template;        
        int currentAmmoInClip = gun.AmmoInClip;

        gun.Template = gunCollectable.GunTemplate;
        gun.AmmoInClip = gunCollectable.AmmoInClip;

        gunCollectable.GunTemplate = gunToBeReplaced;
        gunCollectable.AmmoInClip = currentAmmoInClip;
    }
}
