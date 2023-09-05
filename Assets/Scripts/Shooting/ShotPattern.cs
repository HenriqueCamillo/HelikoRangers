using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "DefaultShotPattern", menuName = "HelikoRangers/ShotPatterns/Default", order = 0)]
public class ShotPattern : ScriptableObject
{
    [SerializeField] protected int resourceCost = 1;
    [SerializeField] protected int bullets = 1;
    [SerializeField] protected float maxDispersionAngle = 0.0f;

    public int ResourceCost => resourceCost;
    public int Bullets => bullets;
    public float MaxDispersionAngle => maxDispersionAngle;

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
