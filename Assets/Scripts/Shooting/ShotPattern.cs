using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "ShotPattern", menuName = "HelikoRangers/ShotPattern", order = 0)]
public class ShotPattern : ScriptableObject
{
    [SerializeField] private int bullets = 1;
    [SerializeField] private float maxDispersionAngle = 0.0f;
    [SerializeField] private float cooldown = 0.0f;

    public int Bullets => bullets;
    public float MaxDispersionAngle => maxDispersionAngle;
    public float Cooldown => cooldown;

    public virtual void SpawnBullets(BulletTemplate bulletTemplate, Vector3 spawnPosition, Quaternion rotation, AmmoType ammoType, LayerMask damageLayers)
    {
        for (int i = 0; i < Bullets; i++)
        {
            SpawnBulletAndInitialize(bulletTemplate, spawnPosition, rotation, ammoType, damageLayers);
        }
    }

    protected Bullet SpawnBulletAndInitialize(BulletTemplate bulletTemplate, Vector3 spawnPosition, Quaternion rotation, AmmoType ammoType, LayerMask damageLayers)
    {
        Bullet bullet = Instantiate(bulletTemplate.Prefab, spawnPosition, rotation).GetComponent<Bullet>();
        bullet.Initalize(bulletTemplate, ammoType, damageLayers);

        return bullet;
    }
}
