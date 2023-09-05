using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float maxLife;

    // TODO: better way to pass maxValue to life bars
    public delegate void OnLifeUpdatedHandler(float value, float maxValue);
    public event OnLifeUpdatedHandler OnLifeUpdated;

    public float Life 
    {
        get => life;
        set
        {
            if (life == value)
                return;

            life = Mathf.Clamp(value, 0.0f, maxLife);
            OnLifeUpdated?.Invoke(life, maxLife);

            if (life == 0.0f)
                Die();
        }
    }

    private void Awake()
    {
        Life = maxLife;
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
}