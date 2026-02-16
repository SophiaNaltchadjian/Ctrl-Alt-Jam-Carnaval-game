using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Color damageColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    [Header("Knockback Settings")]
    [SerializeField] float knockbackForce;
    
    Color _defaultColor;

    void Awake ()
    {
        _defaultColor = spriteRenderer.color;
    }

    void OnEnable ()
    {
        health.OnDeath += HandleDeath;
        health.OnInvincibilityStart += HandleInvincibilityStart;
        health.OnInvincibilityEnd += HandleInvincibilityEnd;
    }

    void OnDisable ()
    {
        health.OnDeath -= HandleDeath;
        health.OnInvincibilityStart -= HandleInvincibilityStart;
        health.OnInvincibilityEnd -= HandleInvincibilityEnd;
    }

    void HandleDeath ()
    {
        gameObject.SetActive(false);
    }

    void HandleInvincibilityStart (float duration)
    {
        spriteRenderer.color = damageColor;
    }

    void HandleInvincibilityEnd ()
    {
        spriteRenderer.color = _defaultColor;   
    }
}
