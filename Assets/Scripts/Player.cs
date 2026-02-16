using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;
using Unity.VisualScripting;
public class Player : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;
    private Vector2 input;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 2f;
    private float currentSpeed;
    private bool isDashing = false;
    private int dashCount = 0;
    

 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Dash();
    }
    void FixedUpdate()
    {
        rb.linearVelocity = input * currentSpeed;
    }
    private void Move()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();
    }
    private void Dash()
    {
        if (Input.GetKeyDown("space") && input != Vector2.zero && isDashing == false && dashCount < 3)
        {
            isDashing = true;
            currentSpeed = dashSpeed;
            dashCount++;
            StartCoroutine(dashDuration());
           
        }

    }
    IEnumerator dashDuration()
    {
        yield return new WaitForSeconds(0.4f);
        currentSpeed = speed;
        StartCoroutine(dashTiming());
    }
    IEnumerator dashTiming()
    {
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }

}