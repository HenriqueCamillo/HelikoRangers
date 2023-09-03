using UnityEngine;

[CreateAssetMenu(fileName = "BulletTemplate", menuName = "HelikoRangers/BulletTemplate", order = 0)]
public class BulletTemplate : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float size;
    [SerializeField] private float lifetime; // TODO: use distance instead of time
    [SerializeField] private Bullet prefab;

    public float Damage => damage;
    public float Speed => speed;
    public float Size => size;
    public float Lifetime => lifetime;
    public Bullet Prefab => prefab;
}