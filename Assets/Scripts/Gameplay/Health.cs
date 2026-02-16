using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    //TODO pedro: evaluate all these events
    public event Action<HealthStateChange> OnHealthChanged;
    public event Action OnDeath;
    public event Action OnRevive;
    public event Action<float> OnInvincibilityStart;
    public event Action OnInvincibilityEnd;
    
    //TODO pedro: maybe change to float
    [SerializeField] int maxHealth;
    
    [Header("Invincibility Settings")]
    [SerializeField] bool hasInvincibility;
    [SerializeField] float invincibilityDuration;

    public int CurrentHealth { get; private set; }
    
    bool _isInvincible;
    float _invincibilityTimer;
    
    void Start()
    {
        ResetHealth(false);
    }

    public void ResetHealth (bool revived = true)
    {
        if (revived)
            OnRevive?.Invoke();
        
        CurrentHealth = maxHealth;
        _isInvincible = false;
        _invincibilityTimer = 0f;
        
        HealthStateChange change = new()
        {
            Current = CurrentHealth,
            Max = maxHealth,
            Delta = 0
        };
        OnHealthChanged?.Invoke(change);
    }

    public bool ModifyHealth (int amount, Vector3? originatorPosition = null, float knockbackMultiplier = 0)
    {
        if (amount < 0 && _isInvincible)
            return false;

        if (amount < 0 && hasInvincibility && !_isInvincible)
            StartCoroutine(InvincibilityCoroutine());
        
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        
        if (CurrentHealth <= 0)
            Die();
        else
        {
            HealthStateChange change = new()
            {
                Current = CurrentHealth,
                Max = maxHealth,
                Delta = amount,
                OriginatorPosition = originatorPosition,
                KnockbackMultiplier = knockbackMultiplier
            };
            OnHealthChanged?.Invoke(change);
        }
        return true;
    }

    void Die ()
    {
        CurrentHealth = 0;
        OnDeath?.Invoke();
    }

    IEnumerator InvincibilityCoroutine ()
    {
        _isInvincible = true;
        _invincibilityTimer = invincibilityDuration;
        OnInvincibilityStart?.Invoke(_invincibilityTimer);
        
        while (_invincibilityTimer > 0f)
        {
            _invincibilityTimer -= Time.deltaTime;
            yield return null;
        }
        
        _isInvincible = false;
        OnInvincibilityEnd?.Invoke();
    }
}
