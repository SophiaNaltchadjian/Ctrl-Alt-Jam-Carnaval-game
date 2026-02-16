using System;
using System.Collections;
using UnityEngine;

public class CollapsingRocks : MonoBehaviour
{
    [SerializeField] GiveDamage damageObject;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color activeColor;
    [SerializeField] float activationTimer;
    [SerializeField] float damageTimer;

    bool _isActive;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerObject player))
            return;

        if (!_isActive)
            StartCoroutine(ActivationCoroutine());
    }


    IEnumerator ActivationCoroutine ()
    {
        _isActive = true;
        yield return new WaitForSeconds(activationTimer);
        
        damageObject.gameObject.SetActive(true);
        spriteRenderer.color = activeColor;
        yield return new WaitForSeconds(damageTimer);
        
        damageObject.SetDamage(0);
    }
}