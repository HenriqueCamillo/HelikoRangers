using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float maxLife;

    public float Life 
    {
        get => life;
        set
        {
            if (life == value)
                return;

            life = Mathf.Clamp(value, 0.0f, maxLife);

            UpdateLifeBar();

            if (life == 0.0f)
                Die();
        }

    }

    private void Awake()
    {
        Life = maxLife;
    }
    protected virtual void UpdateLifeBar()
    {

    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
}