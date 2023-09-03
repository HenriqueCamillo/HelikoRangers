using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletTemplate template;
    [SerializeField] private Rigidbody2D rigidBody;
   private LayerMask damageLayers;

    private void Awake()
    {
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>(); 
    }

    public void Initalize(BulletTemplate template, AmmoType ammoType, LayerMask damageLayers)
    {
        this.template = template;
        this.damageLayers = damageLayers;
        
        rigidBody.velocity = this.transform.right * template.Speed;
        
        // TODO: Change shader outline according to ammoType

        Invoke(nameof(Disappear), template.Lifetime);
    }

    private void Disappear()
    {
        // TODO: Animation
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int otherLayerMaskValue= 1 << other.gameObject.layer;

        if ((otherLayerMaskValue & damageLayers.value) != 0)
        {
            Hitbox hitbox = other.gameObject.GetComponent<Hitbox>();
            hitbox.ApplyDamange(template.Damage);
        
            Destroy(this.gameObject);
        }
    }
}
