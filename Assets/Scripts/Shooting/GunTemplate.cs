using UnityEngine;
using UnityEngine.Video;

public enum AmmoType
{
    Light,
    Medium,
    Heavy
}

[CreateAssetMenu(fileName = "GunTemplate", menuName = "HelikoRangers/GunTemplate", order = 0)]
public class GunTemplate : ScriptableObject
{
    [SerializeField] private GunVisuals visuals;
    [SerializeField] private BulletTemplate bulletTemplate;
    [SerializeField] private ShotPattern shotPattern;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int ammoClipCapacity;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool automaticFiringMode;

    public Vector2 MuzzleOffset => visuals.MuzzleOffset;
    public Sprite Sprite => visuals.Sprite;
    public BulletTemplate BulletTemplate => bulletTemplate;
    public ShotPattern ShotPattern => shotPattern;
    public AmmoType AmmoType => ammoType;
    public int AmmoClipCapacity => ammoClipCapacity;
    public float ReloadTime => reloadTime;
    public bool AutomaticFiringMode => automaticFiringMode;
}
