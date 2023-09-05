using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlternatingShotPattern", menuName = "HelikoRangers/ShotPatterns/Alternating", order = 0)]
public class AlternatingShotPattern : ShotPattern
{
    [SerializeField] private float offset;
    private int offsetDirection = 1;

    public override void SpawnBullets(BulletTemplate bulletTemplate, Vector3 spawnPosition, Quaternion rotation, AmmoType ammoType, LayerMask damageLayers)
    {
        for (int i = 0; i < Bullets; i++)
        {
            float directedOffset = offset * offsetDirection;
            offsetDirection *= -1;

            Vector3 offsetSpawnPosition = spawnPosition + rotation * new Vector2(0.0f, directedOffset);

            SpawnBulletAndInitialize(bulletTemplate, offsetSpawnPosition, rotation, ammoType, damageLayers);
        }

    }

}
