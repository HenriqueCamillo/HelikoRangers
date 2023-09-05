using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpreadShotPattern", menuName = "HelikoRangers/ShotPatterns/Spread", order = 0)]
public class BurstShot : ShotPattern
{
    [SerializeField] private float spreadAngle;

    public override void SpawnBullets(BulletTemplate bulletTemplate, Vector3 spawnPosition, Quaternion rotation, AmmoType ammoType, LayerMask damageLayers)
    {
        // Vector3 direction = rotation.eulerAngles;
        bool hasEvenBullets = bullets % 2 == 0;

        float firstAngleOffset = 0;
        float angleStep = 0;

        if (hasEvenBullets)
        {
            angleStep = spreadAngle / bullets;
            firstAngleOffset = angleStep / 2.0f;
        }
        else if (bullets > 1)
        {
            angleStep = spreadAngle / (bullets - 1);
        }


        int alternatingFactor = 1;
        Vector3 defaultRotationVector = rotation.eulerAngles;

        for (int i = 0; i < Bullets; i++)
        {
            int pairIndex = (i + 1) / 2;
            float angleOffset = firstAngleOffset + (pairIndex * alternatingFactor * angleStep);

            Vector3 updatedRotationVector = defaultRotationVector + new Vector3(0.0f, 0.0f, angleOffset);
            Quaternion updatedRotation = Quaternion.Euler(updatedRotationVector);

            SpawnBulletAndInitialize(bulletTemplate, spawnPosition, updatedRotation, ammoType, damageLayers);          
            alternatingFactor *= -1;
        }
    }
}
