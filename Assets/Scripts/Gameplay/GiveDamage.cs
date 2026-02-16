using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] int damage = 10;
    [SerializeField] LayerMask damageableLayers;

    [Header("Trigger Conditions")]
    [SerializeField] bool damageOnTrigger;
    [SerializeField] bool damageOnCollision;
    
    [Header("Continuous Damage")]
    [SerializeField] bool damageContinuously;
    [SerializeField] float damageInterval = 0.5f;
    
    Dictionary<int, float> nextDamageTimes = new();

    public void SetDamage (int damage)
    {
        this.damage = damage;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (damageOnCollision)
            TryDamage(other.gameObject, other.GetContact(0).point, true);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (damageOnCollision && damageContinuously)
            TryDamage(other.gameObject, other.GetContact(0).point, false);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        RemoveFromTracker(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!damageOnTrigger)
            return;
        Vector3 pointOnEnemy = other.ClosestPoint(transform.position);
        TryDamage(other.gameObject, pointOnEnemy, true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!damageOnTrigger)
            return;
        Vector3 pointOnEnemy = other.ClosestPoint(transform.position);
        TryDamage(other.gameObject, pointOnEnemy, false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        RemoveFromTracker(other.gameObject);
    }
    
    void TryDamage(GameObject other, Vector3 contactPoint, bool isEnter)
    {
        if (damage <= 0)
            return;
        
        if (damageableLayers != (damageableLayers | (1 << other.layer)))
            return;
        
        if (!other.TryGetComponent(out Health health))
            return;

        if (!damageContinuously)
        {
            if (!isEnter)
                return;
            health.ModifyHealth(-damage, contactPoint);
            return;
        }
        
        int id = other.GetInstanceID();

        if (nextDamageTimes.TryGetValue(id, out float nextTime))
        {
            if (Time.time < nextTime)
                return;
        }
        
        if (health.ModifyHealth(-damage, contactPoint))
            nextDamageTimes[id] = Time.time + damageInterval;
    }
    
    void RemoveFromTracker(GameObject other)
    {
        int id = other.GetInstanceID();
        nextDamageTimes.Remove(id);
    }
}