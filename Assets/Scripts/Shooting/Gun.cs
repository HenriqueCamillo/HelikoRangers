using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunTemplate template;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isInCooldown;
    private bool isTriggerPressed;

    public int AmmoInClip;

    public LayerMask DamageLayers;


    public GunTemplate Template
    {
        get => template;
        set 
        {
            template = value;
            spriteRenderer.sprite = template ? template.Sprite : null;
        }
    }

    public bool IsInCooldown => isInCooldown;

    private void Awake()
    {
        Template = Template;
        AmmoInClip = Template.AmmoClipCapacity;
    }

    public void StartShooting()
    {
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

        if (AmmoInClip < Template.ShotPattern.Bullets)
    	{
            // TODO: Invoke reload event
            return;
        }

        Template.ShotPattern.SpawnBullets(Template.BulletTemplate, GetMuzzlePosition(), this.transform.rotation, Template.AmmoType, DamageLayers);
        AmmoInClip -= Template.ShotPattern.Bullets;

        float cooldown = Template.ShotPattern.Cooldown;
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
        if (isTriggerPressed && Template.AutomaticFiringMode)
            Shoot();
    }

    public void FlipSprite(bool aimingLeft)
    {
        spriteRenderer.flipY = aimingLeft;
    }
}