using UnityEngine;
using System;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunTemplate template;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isInCooldown;
    private bool isTriggerPressed;

    private int ammoInClip;
    public LayerMask DamageLayers;

    public event Action OnEmptyAmmoClip;
    public delegate void OnAmmoClipValuesChangedHandler(int ammoInClip, int capacity);
    public event OnAmmoClipValuesChangedHandler OnAmmoClipValuesChanged;

    public int AmmoInClip
    {
        get => ammoInClip;
        set
        {
            if (Template == null)
            {
                ammoInClip = value;
                OnAmmoClipValuesChanged?.Invoke(ammoInClip, -1);
            }
            else
            {
                ammoInClip = Mathf.Clamp(value, 0, Template.AmmoClipCapacity);
                OnAmmoClipValuesChanged?.Invoke(ammoInClip, Template.AmmoClipCapacity);
            }
        }
    }

    public GunTemplate Template
    {
        get => template;
        set 
        {
            template = value;
            spriteRenderer.sprite = template ? template.Sprite : null;
            OnAmmoClipValuesChanged?.Invoke(ammoInClip, template ? template.AmmoClipCapacity : -1);
        }
    }

    public bool IsInCooldown => isInCooldown;

    private void Awake()
    {
        Template = template;

        if (Template != null)
            AmmoInClip = Template.AmmoClipCapacity;
    }

    public void StartShooting()
    {
        if (Template == null)
            return;
        
        if (isTriggerPressed)
            return;

        isTriggerPressed = true;
        Shoot();
    }

    public void StopShooting()
    {
        isTriggerPressed = false;
    }

    public void Shoot()
    {
        if (IsInCooldown)
            return;

        if (AmmoInClip < Template.ShotPattern.ResourceCost)
    	    return;

        Template.ShotPattern.SpawnBullets(Template.BulletTemplate, GetMuzzlePosition(), this.transform.rotation, Template.AmmoType, DamageLayers);
        AmmoInClip -= Template.ShotPattern.ResourceCost;

        float cooldown = Template.Cooldown;
        if (cooldown > 0.0f)
        {
            isInCooldown = true;
            Invoke(nameof(FinishCooldown), cooldown);
        }
    }

    private Vector3 GetMuzzlePosition()
    {
        Vector2 fixedMuzzleOffset = new Vector2(template.MuzzleOffset.x, template.MuzzleOffset.y * (spriteRenderer.flipY ? -1 : 1));
        Vector3 rotatedMuzzleOffset = this.transform.rotation * (Vector3)fixedMuzzleOffset;
        return this.transform.position + rotatedMuzzleOffset;
    }

    private void FinishCooldown()
    {
        isInCooldown = false;

        if (AmmoInClip < Template.ShotPattern.ResourceCost)
        {
            OnEmptyAmmoClip?.Invoke();
            return;
        }

        if (isTriggerPressed && Template.AutomaticFiringMode)
            Shoot();
    }

    public void FlipSprite(bool aimingLeft)
    {
        spriteRenderer.flipY = aimingLeft;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}