using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlimeController : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public float moveSpeed = 0.2f;


    [SerializeField]
    public float health, maxHealth = 120f;
    [SerializeField]
    FloatingHealthBar healthBar;

    private Vector3 initialPosition; // Store the initial position of the Slime
    private bool isDefeated = false; 

    public LayerMask obstacleLayer; // Layer mask to specify the obstacle layers


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform; 
        _rb = GetComponent<Rigidbody2D>();     
        _animator = GetComponent<Animator>();  
        _spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
        initialPosition = transform.position; // Store the initial position
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTransform != null) // Check if player exists
        {
            // Calculate direction towards player
            Vector2 direction = playerTransform.position - transform.position;
            direction.Normalize(); // Normalize the direction vector to have a magnitude of 1


            // Check if there is an obstacle between slime and player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position, playerTransform.position), obstacleLayer);
            if (hit.collider != null)
            {
                // Obstacle detected, go to initial position
                Vector2 returnDirection = initialPosition - transform.position;
                returnDirection.Normalize();
                _rb.MovePosition(_rb.position + returnDirection * (moveSpeed * Time.fixedDeltaTime));
                _animator.SetBool("IsMoving", returnDirection.magnitude > 0);
            }
            else
            {
                // No obstacle, move towards player
                // Move the enemy towards the player
                _rb.MovePosition(_rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
                // Flip the enemy sprite based on the direction
                _spriteRenderer.flipX = direction.x < 0;
                _animator.SetBool("IsMoving", direction.magnitude > 0);
            }
        }
    }

    public float Heatth
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Defeated();
            }

        }
        get
        {
            return health;
        }
    }

    public void Defeated()
    {
        if (!isDefeated)
        {
            isDefeated = true; 
            Debug.Log("Slime defeated, playing animation");
            _animator.SetTrigger("Defeated");
            ScoreController.slimeCount++; 
        }

    }

    public void RemoveEnemy()
    {
        Debug.Log("Removing enemy after defeated animation");
        Destroy(gameObject);
    }

    public void TakeDamage(float damgeAmount)
    {
        health -= damgeAmount;  
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Defeated();
        }
    }

}
