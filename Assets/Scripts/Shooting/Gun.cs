using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunTemplate template;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isInCooldown;
    private bool isTriggerPressed;

    public LayerMask DamageLayers;


    public GunTemplate Template
    {
        get => template;
        set 
        {
            if (template == value)
                return;
            
            template = value;
            spriteRenderer.sprite = template.Sprite;
        }
    }

    public bool IsInCooldown => isInCooldown;

    private void Start()
    {
        Template = Template;
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

        Template.ShotPattern.SpawnBullets(Template.BulletTemplate, GetMuzzlePosition(), this.transform.rotation, Template.AmmoType, DamageLayers);

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