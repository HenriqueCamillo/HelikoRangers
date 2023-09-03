using UnityEngine;


[CreateAssetMenu(fileName = "GunVisuals", menuName = "HelikoRangers/GunVisuals", order = 0)]
public class GunVisuals : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Vector2 muzzleOffset;

    public Sprite Sprite => sprite;
    public Vector2 MuzzleOffset => muzzleOffset;
}
