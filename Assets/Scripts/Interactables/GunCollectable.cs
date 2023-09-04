using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunCollectable : ProximityInteractable 
{
    [SerializeField] private SpriteRenderer spriteRenderer;
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

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        GunTemplate = gunTemplate;
        AmmoInClip = GunTemplate.AmmoClipCapacity;
    }

    public override void Interact(GameObject interactor)
    {
        Player player = interactor.GetComponent<Player>();
        player.PickUpDroppedGun(this);
    }

    public override void SetHintVisible(bool visible)
    {
        spriteRenderer.color = visible ? Color.cyan : Color.white;
    }
}
