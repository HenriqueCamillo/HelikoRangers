using UnityEngine;


[CreateAssetMenu(fileName = "GunVisuals", menuName = "HelikoRangers/Guns/Visuals", order = 0)]
public class GunVisuals : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Vector2 muzzleOffset;

    public Sprite Sprite => sprite;
    public Vector2 MuzzleOffset => muzzleOffset;
}
