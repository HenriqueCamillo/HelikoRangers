using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunCollectable : ProximityInteractable 
{
    [SerializeField] private GunTemplate gunTemplate;
    public int AmmoInClip;
    public GunTemplate GunTemplate
    {
        get => gunTemplate;
        set
        {
            gunTemplate = value;
            spriteRenderer.sprite = gunTemplate ? gunTemplate.Sprite : null;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        GunTemplate = gunTemplate;
        AmmoInClip = GunTemplate.AmmoClipCapacity;
    }

    public override void Interact(Interactor interactor)
    {
        GunCharacter gunCharacter = interactor.GetComponent<GunCharacter>();
        if (gunCharacter != null)
            gunCharacter.PickUpDroppedGun(this);
    }
}
