using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public float moveSpeed = 0.2f;

    public float health = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform; 
        _rb = GetComponent<Rigidbody2D>();     
        _animator = GetComponent<Animator>();  
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTransform != null) // Check if player exists
        {
            // Calculate direction towards player
            Vector2 direction = playerTransform.position - transform.position;
            direction.Normalize(); // Normalize the direction vector to have a magnitude of 1
            // Move the enemy towards the player
            _rb.MovePosition(_rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
            // Flip the enemy sprite based on the direction
            _spriteRenderer.flipX = direction.x < 0;
            _animator.SetBool("IsMoving", direction.magnitude > 0);
        }
    }

    public float Heatth
    {
        set
        {
            health = value;

            if (health <= 0) {
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
        Debug.Log("Slime defeated, playing animation");
        _animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Debug.Log("Removing enemy after defeated animation");
        Destroy(gameObject);
    }
}
