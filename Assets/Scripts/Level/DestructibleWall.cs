using System;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    [SerializeField] Health health;

    void OnEnable ()
    {
        health.OnDeath += HandleDeath;
    }

    void OnDisable ()
    {
        health.OnDeath -= HandleDeath;
    }

    void HandleDeath ()
    {
        gameObject.SetActive(false);
    }
}
